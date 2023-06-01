namespace SimplePlayer.NETFramework
{
	partial class Main
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.WMP = new AxWMPLib.AxWindowsMediaPlayer();
			this.openBtn = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.listBox = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();
			this.SuspendLayout();
			// 
			// WMP
			// 
			this.WMP.Enabled = true;
			this.WMP.Location = new System.Drawing.Point(12, 334);
			this.WMP.Name = "WMP";
			this.WMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WMP.OcxState")));
			this.WMP.Size = new System.Drawing.Size(1001, 104);
			this.WMP.TabIndex = 0;
			// 
			// openBtn
			// 
			this.openBtn.Location = new System.Drawing.Point(51, 242);
			this.openBtn.Name = "openBtn";
			this.openBtn.Size = new System.Drawing.Size(75, 23);
			this.openBtn.TabIndex = 1;
			this.openBtn.Text = "Otwórz";
			this.openBtn.UseVisualStyleBackColor = true;
			this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "file";
			// 
			// listBox
			// 
			this.listBox.FormattingEnabled = true;
			this.listBox.Location = new System.Drawing.Point(174, 170);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(120, 95);
			this.listBox.TabIndex = 2;
			this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1025, 448);
			this.Controls.Add(this.listBox);
			this.Controls.Add(this.openBtn);
			this.Controls.Add(this.WMP);
			this.Name = "Main";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer WMP;
		private System.Windows.Forms.Button openBtn;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ListBox listBox;
	}
}

