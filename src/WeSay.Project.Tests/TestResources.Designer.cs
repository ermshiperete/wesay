﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1434
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WeSay.Project.Tests
{
	/// <summary>
	///   A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	// This class was auto-generated by the StronglyTypedResourceBuilder
	// class via a tool like ResGen or Visual Studio.
	// To add or remove a member, edit your .ResX file then rerun ResGen
	// with the /str option, or rebuild your VS project.
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode()]
	[CompilerGenerated()]
	internal class TestResources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal TestResources() {}

		/// <summary>
		///   Returns the cached ResourceManager instance used by this class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (ReferenceEquals(resourceMan, null))
				{
					ResourceManager temp =
							new ResourceManager("WeSay.Project.Tests.TestResources",
												typeof (TestResources).Assembly);
					resourceMan = temp;
				}
				return resourceMan;
			}
		}

		/// <summary>
		///   Overrides the current thread's CurrentUICulture property for all
		///   resource lookups using this strongly typed resource class.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get { return resourceCulture; }
			set { resourceCulture = value; }
		}

		/// <summary>
		///   Looks up a localized string similar to # SOME DESCRIPTIVE TITLE.
		///# Copyright (C) YEAR THE PACKAGE&apos;S COPYRIGHT HOLDER
		///# This file is distributed under the same license as the PACKAGE package.
		///# FIRST AUTHOR &lt;EMAIL@ADDRESS&gt;, YEAR.
		///#
		///#, fuzzy
		///msgid &quot;&quot;
		///msgstr &quot;&quot;
		///&quot;Project-Id-Version: PACKAGE VERSION\n&quot;
		///&quot;Report-Msgid-Bugs-To: blah balh\n&quot;
		///&quot;POT-Creation-Date: 2005-09-20 20:52+0200\n&quot;
		///&quot;PO-Revision-Date: YEAR-MO-DA HO:MI+ZONE\n&quot;
		///&quot;Last-Translator: FULL NAME &lt;EMAIL@ADDRESS&gt;\n&quot;
		///&quot;Language-Team: LANGUAGE &lt;LL@li.org&gt;\n&quot;
		///&quot;MIME-Version: 1.0\n&quot;
		///&quot;Co [rest of string was truncated]&quot;;.
		/// </summary>
		internal static string poStrings
		{
			get { return ResourceManager.GetString("poStrings", resourceCulture); }
		}

		/// <summary>
		///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
		///&lt;viewTemplate id=&quot;defaultviewTemplate&quot; class=&quot;WeSay.UI.viewTemplate&quot; assembly=&quot;WeSay.UI&quot;&gt;
		///  &lt;fields&gt;
		///    &lt;field class=&quot;WeSay.LexicalModel.Field&quot; assembly=&quot;WeSay.LexicalModel&quot;&gt;
		///      &lt;className&gt;LexEntry&lt;/className&gt;
		///      &lt;fieldName&gt;EntryLexicalForm&lt;/fieldName&gt;
		///      &lt;displayName&gt;Word&lt;/displayName&gt;
		///      &lt;dataType&gt;MultiText&lt;/dataType&gt;
		///      &lt;visibility&gt;Visible&lt;/visibility&gt;
		///      &lt;writingSystems&gt;
		///           &lt;id&gt;xx&lt;/id&gt;
		///          &lt;id&gt;yy&lt;/id&gt;
		///       &lt;/writingSys [rest of string was truncated]&quot;;.
		/// </summary>
		internal static string viewTemplate
		{
			get { return ResourceManager.GetString("viewTemplate", resourceCulture); }
		}
	}
}