using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleUpdater
{
    public partial class Form2 : Form
    {
        string[] args;
        string temp;
        public Form2(string[] inArgs)
        {
            args = inArgs;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                string downloadUrl = args[0];
                label2.Text = "Downloading from " + downloadUrl;
                WebClient wc = new WebClient();
                wc.DownloadFileCompleted += wc_DownloadFileCompleted;

                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                temp = Path.GetTempFileName();
                wc.DownloadFileAsync(new Uri(downloadUrl), temp);
                label3.Text = "Download in progress";
            }
            catch
            {
                label3.Text = "Download failed";
                System.Windows.Forms.MessageBox.Show("The update failed. Please try again later.", "Update Failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            label3.Text = "Download completed";
            File.Delete(args[1]);
            File.Move(temp, args[1]);
            Process.Start(args[1]);
            Application.Exit();
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label1.Text = e.ProgressPercentage + "%";
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
