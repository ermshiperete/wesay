using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Palaso.Reporting;
using WeSay.Data;
using WeSay.LexicalModel;
using WeSay.Project;
using WeSay.UI;

namespace WeSay.LexicalTools
{
	public partial class EntryViewControl : UserControl
	{
		private ViewTemplate _viewTemplate;
		private LexEntry _record;
		private Timer _cleanupTimer;
		private bool _isDisposed = false;

		public EntryViewControl()
		{
			_viewTemplate = null;
			InitializeComponent();
			_detailListControl.ChangeOfWhichItemIsInFocus +=
					new EventHandler<CurrentItemEventArgs>(OnChangeOfWhichItemIsInFocus);
			_detailListControl.KeyDown += new KeyEventHandler(_detailListControl_KeyDown);
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (_cleanupTimer != null)
			{
				_cleanupTimer.Dispose();
			}
			base.OnHandleDestroyed(e);
		}

		private void _detailListControl_KeyDown(object sender, KeyEventArgs e)
		{
			OnKeyDown(e);
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ViewTemplate ViewTemplate
		{
			get
			{
				if (_viewTemplate == null)
				{
					throw new InvalidOperationException("ViewTemplate must be initialized");
				}
				return _viewTemplate;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				_viewTemplate = value;
				Refresh();
			}
		}

		public RichTextBox ControlFormattedView
		{
			get { return _lexicalEntryPreview; }
		}

		public DetailList ControlEntryDetail
		{
			get { return _detailListControl; }
		}

		public LexEntry DataSource
		{
			get { return _record; }
			set
			{
				Logger.WriteMinorEvent("In DataSource Set");
				_showNormallyHiddenFields = false;

				if (_record != value)
				{
					if (_record != null && !_record.IsBeingDeleted)
					{
						Logger.WriteMinorEvent("Datasource set. Calling _record.CleanUpAfterEditting()");
						_record.PropertyChanged -= OnRecordPropertyChanged;
						_record.EmptyObjectsRemoved -= OnEmptyObjectsRemoved;
						_record.CleanUpAfterEditting();
					}
					_record = value;
					_currentItemInFocus = null;
					if (_record != null)
					{
						_record.PropertyChanged += new PropertyChangedEventHandler(OnRecordPropertyChanged);
						_record.EmptyObjectsRemoved += new EventHandler(OnEmptyObjectsRemoved);
					}
					Logger.WriteMinorEvent("Datasource set calling RefreshLexicalEntryPreview()");
					RefreshLexicalEntryPreview();
					Logger.WriteMinorEvent("Datasource set calling RefreshEntryDetail()");
					ShowNormallyHiddenFields = false;
					RefreshEntryDetail();
				}

				Logger.WriteMinorEvent("Exit DataSource Set");
			}
		}

		/// <summary>
		/// Use for establishing relations been this entry and the rest
		/// </summary>
		public IRecordListManager RecordListManager
		{
			set { _recordListManager = value; }
		}

		public bool ShowNormallyHiddenFields
		{
			get { return _showNormallyHiddenFields; }
			set
			{
				_showNormallyHiddenFields = value;
				//no... this will lead to extra refreshing. RefreshEntryDetail();
			}
		}

		public void ToggleShowNormallyHiddenFields()
		{
			ShowNormallyHiddenFields = !ShowNormallyHiddenFields;
			RefreshEntryDetail();
		}

		private void OnEmptyObjectsRemoved(object sender, EventArgs e)
		{
			VerifyNotDisposed();
			//find out where our current focus is and attempt to return to that place
			int? row = null;
			if (_detailListControl.ContainsFocus)
			{
				Control focussedControl = _detailListControl.FocussedImmediateChild;
				if (focussedControl != null)
				{
					row = _detailListControl.GetRow(focussedControl);
				}
			}
			Logger.WriteMinorEvent("OnEmptyObjectsRemoved: b4 RefreshEntryDetial");
			RefreshEntryDetail();
			Logger.WriteMinorEvent("OnEmptyObjectsRemoved: b4  Application.DoEvents()");
			Application.DoEvents(); //TODO: We need to remove this.  It's a dangerous thing in a historically buggy spot
			Logger.WriteMinorEvent("OnEmptyObjectsRemoved: b4 MoveInsertionPoint");
			if (row != null)
			{
				row = Math.Min((int) row, _detailListControl.Count - 1);
				Debug.Assert(row > -1, "You've reproduced bug ws-511!"); // bug WS-511, which we haven't yet been able to reproduce
				row = Math.Max((int) row, 0); //would get around bug WS-511
				_detailListControl.MoveInsertionPoint((int) row);
			}
			Logger.WriteMinorEvent("OnEmptyObjectsRemoved end");
		}

		private void OnRecordPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			VerifyNotDisposed();
			LexEntry entry = (LexEntry) sender;
			switch (e.PropertyName)
			{
					// these are changes to the list not a change that needs to clean up
					// and can actually have very detrimental effect if we do.
				case "exampleSentences":
				case "senses":
					break;
				default:
					//this is to fix WS-238. The scenario is this:
					//a paste operation starts by erasing the target... this fired off a cleanup
					//and then the new text came in so fast that we got various crashes.
					//With this, we just schedule a cleanup, as a ui event handler, for
					//a moment in the future.  The interval is chosen to allow even a quick
					//backspace followed by typing, without wiping out the sense/example.
					if (_cleanupTimer == null)
					{
						_cleanupTimer = new Timer();
						_cleanupTimer.Tick += new EventHandler(OnCleanupTimer_Tick);
						_cleanupTimer.Interval = 500;
					}
					_cleanupTimer.Tag = entry;
					_cleanupTimer.Start();
					break;
			}
			RefreshLexicalEntryPreview();
		}

		private void OnCleanupTimer_Tick(object sender, EventArgs e)
		{
			VerifyNotDisposed();
			Logger.WriteMinorEvent("OnCleanupTimer_Tick");
			LexEntry entry = (LexEntry) _cleanupTimer.Tag;
			_cleanupTimer.Stop();
			entry.CleanUpEmptyObjects();
		}

		protected void VerifyNotDisposed()
		{
			if (this._isDisposed)
			{
#if DEBUG
				throw new ObjectDisposedException(GetType().FullName);
#endif
				Palaso.Reporting.ErrorReport.ReportNonFatalMessage("WeSay ran into a problem in the EntryViewControl (it was called after it was disposed.) If you can make this happen again, please contact the developers.");
			}
		}

		private void RefreshLexicalEntryPreview()
		{
			VerifyNotDisposed();
			try
			{
				_lexicalEntryPreview.Rtf = RtfRenderer.ToRtf(_record, _currentItemInFocus);
				_lexicalEntryPreview.Refresh();
			}
			catch (Exception error)
			{
#if DEBUG
				throw error;
#endif
				Palaso.Reporting.ErrorReport.ReportNonFatalMessage("There was an error refreshing the entry preview. If you were quiting the program, this is a know issue (WS-554) that we are trying to track down.  If you can make this happen again, please contact the developers.");
			}
		}

		private void RefreshEntryDetail()
		{
			try
			{
				_panelEntry.SuspendLayout();

				_detailListControl.SuspendLayout();

				_detailListControl.Clear();
				_detailListControl.VerticalScroll.Value = _detailListControl.VerticalScroll.Minimum;
				_panelEntry.Controls.Add(_detailListControl);
				if (_record != null)
				{
					LexEntryLayouter layout = new LexEntryLayouter(_detailListControl, ViewTemplate, _recordListManager);
					layout.ShowNormallyHiddenFields = ShowNormallyHiddenFields;
					layout.AddWidgets(_record);
				}
				_detailListControl.ResumeLayout();
				_panelEntry.ResumeLayout(false);//review
			}
			catch (ConfigurationException e)
			{
				Palaso.Reporting.ErrorReport.ReportNonFatalMessage(e.Message);
			}
		}

		private void OnChangeOfWhichItemIsInFocus(object sender, CurrentItemEventArgs e)
		{
			VerifyNotDisposed();
			_currentItemInFocus = e;
			RefreshLexicalEntryPreview();
		}

		private CurrentItemEventArgs _currentItemInFocus;
		private IRecordListManager _recordListManager;
		private bool _showNormallyHiddenFields=false;

		private void LexPreviewWithEntryControl_BackColorChanged(object sender, EventArgs e)
		{
			_detailListControl.BackColor = BackColor;
			_lexicalEntryPreview.BackColor = BackColor;
		}

		~EntryViewControl()
		{
			if (!this._isDisposed)
			{
				throw new InvalidOperationException("Disposed not explicitly called on " + GetType().FullName + ".");
			}

		}
	}
}