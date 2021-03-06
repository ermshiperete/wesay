﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace WeSay.UI
{
	/// <summary>
	/// Labels are fairly limitted even in .net, but on mono so far, multi-line
	/// labels are trouble.  This class uses TextBox to essentially be a better
	/// cross-platform label.
	/// </summary>
	public partial class BetterLabel : TextBox
	{
		public BetterLabel()
		{
			InitializeComponent();
		}

		//make it transparent
		private void BetterLabel_ParentChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (DesignMode)
					return;
				BackColor = Parent.BackColor;
				Parent.BackColorChanged += ((x, y) => BackColor = Parent.BackColor);
			}
			catch (Exception error)
			{
				//trying to harden this against the mysteriously disappearing from a host designer
			}
		}


		private void BetterLabel_TextChanged(object sender, System.EventArgs e)
		{
			//this is apparently dangerous to do in the constructor
			Font = SystemFonts.MessageBoxFont;
		}

	}
}