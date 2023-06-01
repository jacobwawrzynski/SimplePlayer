using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePlayer.NETFramework
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		string[] files;
		string[] path;

		private void openBtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				files = openFileDialog.SafeFileNames;
				path = openFileDialog.FileNames;
				for (int i = 0; i < files.Length; i++)
				{
					listBox.Items.Add(files[i]);
				}
			}
		}

		private void listBox_SelectedIndexChanged(object sender, EventArgs e)
		{

			WMP.URL = path[listBox.SelectedIndex];
		}
	}
}
