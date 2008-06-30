using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiftIO.Parsing;
using Palaso.Text;
using WeSay.Data;
using WeSay.Foundation;

namespace WeSay.LexicalModel
{

	public class LexEntryRepository: IRepository<LexEntry>
	{
		private readonly IRepository<LexEntry> _decoratedRepository;
		public LexEntryRepository(string path)
		{
			//use default of Db4oRepository for now
			//todo: eventually use synchronicRepository with Db4o and Lift
			_decoratedRepository = new Db4oRepository<LexEntry>(path);
		}

		public LexEntryRepository(IRepository<LexEntry> decoratedRepository)
		{
			if (decoratedRepository == null)
			{
				throw new ArgumentNullException("decoratedRepository");
			}

			_decoratedRepository = decoratedRepository;
		}

		public DateTime LastModified
		{
			get
			{
				return _decoratedRepository.LastModified;
			}
		}

		public LexEntry CreateItem()
		{
			LexEntry item = this._decoratedRepository.CreateItem();
			return item;
		}

		// todo:remove
		public LexEntry CreateItem(Extensible eInfo)
		{
			LexEntry item = this._decoratedRepository.CreateItem();
			item.Guid = eInfo.Guid;
			item.Id = eInfo.Id;
			item.ModificationTime = eInfo.ModificationTime;
			item.CreationTime = eInfo.CreationTime;
			return item;
		}

		public RepositoryId[] GetItemsModifiedSince(DateTime last)
		{
			return _decoratedRepository.GetItemsModifiedSince(last);
		}

		public RepositoryId[] GetAllItems()
		{
			return _decoratedRepository.GetAllItems();
		}

		public int CountAllItems()
		{
			return _decoratedRepository.CountAllItems();
		}

		public RepositoryId GetId(LexEntry item)
		{
			return _decoratedRepository.GetId(item);
		}

		public LexEntry GetItem(RepositoryId id)
		{
			return _decoratedRepository.GetItem(id);
		}

		public void SaveItems(IEnumerable<LexEntry> items)
		{
			_decoratedRepository.SaveItems(items);
		}

		public ResultSet<LexEntry> GetItemsMatching(Query query)
		{
			return _decoratedRepository.GetItemsMatching(query);
		}

		public void SaveItem(LexEntry item)
		{
			_decoratedRepository.SaveItem(item);
		}

		public bool CanQuery()
		{
			throw new NotImplementedException();
		}

		public bool CanPersist()
		{
			throw new NotImplementedException();
		}

		public RepositoryId[] GetItemsMatchingQuery()
		{
			throw new NotImplementedException();
		}

		public void DeleteItem(LexEntry item)
		{
			_decoratedRepository.DeleteItem(item);
		}

		public void DeleteItem(RepositoryId repositoryId)
		{
			_decoratedRepository.DeleteItem(repositoryId);
		}

		public RepositoryId[] GetAllItems()
		{
			throw new NotImplementedException();
		}

		public RepositoryId[] ItemsModifiedSince(DateTime dateTime)
		{
			throw new NotImplementedException();
		}
		public int GetHomographNumber(LexEntry entry, WritingSystem headwordWritingSystem)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (headwordWritingSystem == null)
			{
				throw new ArgumentNullException("headwordWritingSystem");
			}

			ResultSet<LexEntry> resultSet = GetAllEntriesSortedByHeadword(headwordWritingSystem);
			RecordToken<LexEntry> first = resultSet.FindFirst(entry);
			if(first == null)
			{
				throw new ArgumentOutOfRangeException("entry", entry, "Entry not in repository");
			}
			if ((bool)first.Results["HasHomograph"])
			{
				return (int) first.Results["HomographNumber"];
			}
			else
			{
				return 0;
			}
		}

		//todo: look at @order and the planned-for order-in-lift field on LexEntry
		public ResultSet<LexEntry> GetAllEntriesSortedByHeadword(WritingSystem writingSystem)
		{
			if (writingSystem == null)
			{
				throw new ArgumentNullException("writingSystem");
			}
			Query query = GetAllLexEntriesQuery();
			query.In("VirtualHeadWord").ForEach("Forms").Show("Form").Show("WritingSystemId");
			query.Show("OrderForRoundTripping");
			query.Show("CreationTime");

			ResultSet<LexEntry> itemsMatching = GetItemsMatchingQueryFilteredAndSortedByWritingSystem(query, "Form", "WritingSystemId", writingSystem);
			itemsMatching.Sort(new SortDefinition("Form",writingSystem),
				new SortDefinition("OrderForRoundTripping", Comparer<int>.Default),
				new SortDefinition("CreationTime", Comparer<DateTime>.Default));

			string previousHeadWord = null;
			int homographNumber = 1;
			IDictionary<string, object> previousResults = null;
			foreach (RecordToken<LexEntry> token in itemsMatching)
			{
				IDictionary<string, object> results = token.Results;
				string currentHeadWord = (string) results["Form"];
				if(currentHeadWord == previousHeadWord)
				{
					homographNumber++;
				}
				else
				{
					previousHeadWord = currentHeadWord;
					homographNumber = 1;
				}
				// only used to get our sort correct
				results.Remove("OrderForRoundTripping");
				results.Remove("CreationTime");
				results.Add("HomographNumber", homographNumber);
				switch(homographNumber)
				{
					case 1:
						results.Add("HasHomograph", false);
						break;
					case 2:
						Debug.Assert(previousResults != null);
						previousResults["HasHomograph"] = true;
						results.Add("HasHomograph", true);
						break;
					default:
						results.Add("HasHomograph", true);
						break;
				}
				previousResults = results;
			}
			return itemsMatching;
		}


		public ResultSet<LexEntry> GetAllEntriesSortedByLexicalForm(WritingSystem writingSystem)
		{
			if (writingSystem == null)
			{
				throw new ArgumentNullException("writingSystem");
			}
			Query query = GetAllLexEntriesQuery().In("LexicalForm")
						.ForEach("Forms").Show("Form").Show("WritingSystemId");

			return GetItemsMatchingQueryFilteredAndSortedByWritingSystem(query, "Form", "WritingSystemId", writingSystem);
		}

		public ResultSet<LexEntry> GetAllEntriesSortedByGloss(WritingSystem writingSystem)
		{
			if (writingSystem == null)
			{
				throw new ArgumentNullException("writingSystem");
			}
			Query query = GetAllLexEntriesQuery().ForEach("Senses");
			query.In("Definition").ForEach("Forms").Show("Form", "Definition/Form").Show("WritingSystemId", "Definition/WritingSystemId");
			query.In("Gloss").ForEach("Forms").Show("Form", "Gloss/Form").Show("WritingSystemId","Gloss/WritingSystemId");

			return GetItemsMatchingQueryFilteredAndSortedByWritingSystem(query, "Definition/Form", "Definition/WritingSystemId", writingSystem);
		}

		private ResultSet<LexEntry> GetItemsMatchingQueryFilteredAndSortedByWritingSystem(
			Query query,
			string formField,
			string writingSystemIdField,
			WritingSystem writingSystem)
		{
			ResultSet<LexEntry> allEntriesMatchingQuery = GetItemsMatching(query);
			ResultSet<LexEntry> result = FilterEntriesToOnlyThoseWithWritingSystemId(
											allEntriesMatchingQuery,
											formField,
											writingSystemIdField,
											writingSystem.Id);

			result.Sort(new SortDefinition(formField, writingSystem));
			return result;
		}

		public ResultSet<LexEntry> GetAllEntriesSortedBySemanticDomain(
			string fieldName)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			Query allSenseProperties = GetAllLexEntriesQuery().ForEach("Senses")
						.ForEach("Properties").Show("Key").Show("Value","SemanticDomain");

			ResultSet<LexEntry> results = GetItemsMatching(allSenseProperties);
			results.RemoveAll(delegate (RecordToken<LexEntry> token)
				{
					return (string)token.Results["Key"] != fieldName;
				});
			return results;
		}

		public ResultSet<LexEntry> GetEntriesWithMatchingGlossSortedByLexicalForm(
				LanguageForm glossForm, WritingSystem lexicalUnitWritingSystem)
		{
			if (glossForm == null)
			{
				throw new ArgumentNullException("glossForm");
			}
			if (lexicalUnitWritingSystem == null)
			{
				throw new ArgumentNullException("lexicalUnitWritingSystem");
			}
			Query query = GetAllLexEntriesQuery();
			query.In("LexicalForm").ForEach("Forms").Show("Form").Show("WritingSystemId");
			query.ForEach("Senses").In("Gloss").ForEach("Forms").Show("Form", "Gloss/Form").Show("WritingSystemId", "Gloss/WritingSystemId");

			ResultSet<LexEntry> resultSet = GetItemsMatchingQueryFilteredAndSortedByWritingSystem(query, "Form", "WritingSystemId", lexicalUnitWritingSystem);
			resultSet.RemoveAll(delegate(RecordToken<LexEntry> token)
				{
					return (string)token.Results["Gloss/WritingSystemId"] != glossForm.WritingSystemId
						|| (string)token.Results["Gloss/Form"] != glossForm.Form;
				});
			return resultSet;
		}

		public LexEntry GetLexEntryWithMatchingId(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Query idOfEntries = GetAllLexEntriesQuery().Show("Id");
			ResultSet<LexEntry> items = GetItemsMatching(idOfEntries);
			RecordToken<LexEntry> first = items.FindFirst(delegate(RecordToken<LexEntry> token)
												{
													return (string)token.Results["Id"] == id;
												});
			if (first == null)
			{
				return null;
			}
			return first.RealObject;
		}

		public LexEntry GetLexEntryWithMatchingGuid(Guid guid)
		{
			Query query = GetAllLexEntriesQuery().Show("Guid");
			ResultSet<LexEntry> items = GetItemsMatching(query);
			int index = items.FindFirstIndex(delegate(RecordToken<LexEntry> token)
												{
													return (Guid)token.Results["Guid"] == guid;
												});
			if (index < 0)
			{
				return null;
			}
			if (index + 1 < items.Count)
			{
				int nextIndex = items.FindFirstIndex(index,
									delegate(RecordToken<LexEntry> token)
									{
										return (Guid)token.Results["Guid"] == guid;
									});

				if(nextIndex >= 0)
				{
					throw new ApplicationException("More than one entry exists with the guid " +
												   guid);
				}
			}
			RecordToken<LexEntry> first = items[index];
			return first.RealObject;
		}

		public ResultSet<LexEntry> GetEntriesWithSimilarLexicalForm(string lexicalForm,
															WritingSystem writingSystem,
															ApproximateMatcherOptions
																	matcherOptions)
		{
			if (lexicalForm == null)
			{
				throw new ArgumentNullException("lexicalForm");
			}
			if (writingSystem == null)
			{
				throw new ArgumentNullException("writingSystem");
			}
			return new ResultSet<LexEntry>(this,
										   ApproximateMatcher.FindClosestForms
													<RecordToken<LexEntry>>(GetAllEntriesSortedByLexicalForm(writingSystem),
																			GetFormForMatchingStrategy,
																			lexicalForm,
																			matcherOptions));
		}

		private static string GetFormForMatchingStrategy(object item)
		{
			return (string)((RecordToken<LexEntry>)item).Results["Form"];
		}

		public ResultSet<LexEntry> GetEntriesWithMatchingLexicalForm(string lexicalForm,
																	 WritingSystem writingSystem)
		{
			if (lexicalForm == null)
			{
				throw new ArgumentNullException("lexicalForm");
			}
			if (writingSystem == null)
			{
				throw new ArgumentNullException("writingSystem");
			}
			ResultSet<LexEntry> resultSet = GetAllEntriesSortedByLexicalForm(writingSystem);
			resultSet.RemoveAll(delegate(RecordToken<LexEntry> token)
								{
									return (string)token.Results["Form"] != lexicalForm;
								});
			return resultSet;
		}

		private static Query GetAllLexEntriesQuery()
		{
			return new Query(typeof(LexEntry));
		}

		private ResultSet<LexEntry> FilterEntriesToOnlyThoseWithWritingSystemId(ResultSet<LexEntry> entriesWithAllHeadwords, string formField, string writingSystemIdField, string headwordWritingSystemId)
		{
			if (entriesWithAllHeadwords.Count == 0)
			{
				return entriesWithAllHeadwords;
			}

			entriesWithAllHeadwords.Sort(new SortDefinition("RepositoryId", Comparer<RepositoryId>.Default));

			// remove all entries with writing system != headwordWritingSystem
			//            make sure always have one entry though
			// walk list of entries, removing duplicate entries which have same repository id
			//     if there is no entry for a repository id that has
			//     writingSystemId == headwordWritingSystem then
			//     insert an empty form with writingSystemId to headwordId

			Dictionary<string, object> emptyResults = new Dictionary<string, object>();
			emptyResults.Add(formField, string.Empty);
			emptyResults.Add(writingSystemIdField, headwordWritingSystemId);

			List<RecordToken<LexEntry>> entriesWithHeadword = new List<RecordToken<LexEntry>>();
			RepositoryId previousRepositoryId = null;
			bool headWordRepositoryIdFound = false;

			foreach (RecordToken<LexEntry> token in entriesWithAllHeadwords)
			{
				if (previousRepositoryId != null && token.Id != previousRepositoryId)
				{
					if (!headWordRepositoryIdFound)
					{
						entriesWithHeadword.Add(
								new RecordToken<LexEntry>(this, emptyResults, previousRepositoryId));
					}
					headWordRepositoryIdFound = false;
				}
				previousRepositoryId = token.Id;

				object writingSystemId;
				if (token.Results.TryGetValue(writingSystemIdField, out writingSystemId)
					&& (string) writingSystemId == headwordWritingSystemId)
				{
					entriesWithHeadword.Add(token);
					headWordRepositoryIdFound = true;
				}
			}
			if (!headWordRepositoryIdFound)
			{
				entriesWithHeadword.Add(new RecordToken<LexEntry>(this, emptyResults, previousRepositoryId));
			}
			return new ResultSet<LexEntry>(this, entriesWithHeadword);
		}


		public ResultSet<LexEntry> GetEntriesMatchingFilterSortedByLexicalUnit(
				Field filter, WritingSystem lexicalUnitWritingSystem)
		{
			throw new NotImplementedException();
			//LexEntrySortHelper lexEntrySortHelper =
			//        new LexEntrySortHelper(this, lexicalUnitWritingSystem, true);
			//_recordListManager.Register(filter, lexEntrySortHelper);
			//List<RecordToken<LexEntry>> result = new List<RecordToken<LexEntry>>();
			//foreach (LexEntry entry in
			//        _recordListManager.GetListOfTypeFilteredFurther(filter, lexEntrySortHelper))
			//{
			//    RepositoryId id = GetId(entry);
			//    int i =
			//            result.FindAll(
			//                    delegate(RecordToken<LexEntry> match)
			//                    {
			//                        return match.Id == id;
			//                    }).
			//                    Count;

			//    result.Add(
			//            new RecordToken<LexEntry>(this,
			//                                      lexEntrySortHelper,
			//                                      i,
			//                                      entry.LexicalForm[lexicalUnitWritingSystem.Id],
			//                                      id));
			//}
			//return new ResultSet<LexEntry>(this, result);
		}
		#region IDisposable Members

#if DEBUG
		~LexEntryRepository()
		{
			if (!_disposed)
			{
				throw new ApplicationException(
						"Disposed not explicitly called on LexEntryRepository.");
			}
		}
#endif

		private bool _disposed = false;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					// dispose-only, i.e. non-finalizable logic
					_decoratedRepository.Dispose();
				}

				// shared (dispose and finalizable) cleanup logic
				_disposed = true;
			}
		}

		protected void VerifyNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("LexEntryRepository");
			}
		}

		#endregion
	}
}
