using System.Windows.Forms;

namespace WeSay.LexicalTools
{
    partial class CongratulationsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._statusMessage = new System.Windows.Forms.Label();
            this.checkmarkLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _statusMessage
            // 
            this._statusMessage.AutoSize = true;
            this._statusMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._statusMessage.Location = new System.Drawing.Point(70, 37);
            this._statusMessage.Name = "_statusMessage";
            this._statusMessage.Size = new System.Drawing.Size(128, 24);
            this._statusMessage.TabIndex = 0;
            this._statusMessage.Text = "congratsLabel";
            // 
            // checkmarkLabel
            // 
            this.checkmarkLabel.AutoSize = true;
            this.checkmarkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkmarkLabel.Location = new System.Drawing.Point(4, 21);
            this.checkmarkLabel.Name = "checkmarkLabel";
            this.checkmarkLabel.Size = new System.Drawing.Size(60, 46);
            this.checkmarkLabel.TabIndex = 1;
            this.checkmarkLabel.Text = "✔";
            // 
            // CongratulationsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkmarkLabel);
            this.Controls.Add(this._statusMessage);
            this.Name = "CongratulationsControl";
            this.Size = new System.Drawing.Size(342, 144);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _statusMessage;
        private System.Windows.Forms.Label checkmarkLabel;
    }
}
