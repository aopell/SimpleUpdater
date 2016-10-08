using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace SimpleZipUpdater
{
    public partial class Form1 : Form
    {
        string[] Args;
        string temp;

        public Form1(string[] args)
        {
            InitializeComponent();
            Args = args;
        }

        /*
         * Args[0] = Download Url
         * Args[1] = Unzip Path
         * Args[2] = Executable Name / Relative Path 
         */


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string downloadUrl = Args[0];
                label2.Text = "Downloading from " + downloadUrl;
                WebClient wc = new WebClient();
                wc.DownloadFileCompleted += Wc_DownloadFileCompleted;

                wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                temp = Path.GetTempFileName();
                wc.DownloadFileAsync(new Uri(downloadUrl), temp);
                label3.Text = "Download in progress";
            }
            catch (Exception ex)
            {
                label3.Text = "Download failed";
                MessageBox.Show("The download failed. Please try again later.", "Download Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label1.Text = e.ProgressPercentage + "%";
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                label3.Text = "Download completed - Extracting Files";

                if (!Directory.Exists(Args[1]))
                    Directory.CreateDirectory(Args[1]);

                ZipArchive zip = new ZipArchive(new FileStream(temp, FileMode.Open));

                zip.ExtractToDirectory(Args[1], true);
                Process.Start(Path.Combine(Args[1], Args[2]));
                Application.Exit();
            }
            catch (Exception ex)
            {
                label3.Text = "Update failed";
                MessageBox.Show("The update failed. Please try again later.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }

    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);

                if (!Directory.Exists(Path.GetDirectoryName(completeFileName)))
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
}
