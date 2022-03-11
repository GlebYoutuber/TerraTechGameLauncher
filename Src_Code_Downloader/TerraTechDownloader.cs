using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerraTechDownloader
{
    public partial class TerraTechDownloader : Form
    {
        public TerraTechDownloader()
        {
            Directory.CreateDirectory(@"C:\Temp");
            Directory.CreateDirectory(@"C:\TerraTech");
            InitializeComponent();
            this.Show();
            this.label1.Text = String.Empty;
            using(WebClient web = new WebClient())
            {
                web.DownloadProgressChanged += (s, e) => 
                {
                    label1.Text = $"Downloaded: {e.ProgressPercentage}% ({e.BytesReceived / 1024})";
                    downloadstatus.Value = e.ProgressPercentage; 
                };
                web.DownloadFileAsync(new Uri("https://objects.githubusercontent.com/github-production-release-asset-2e65be/468667248/50da8eb2-45ba-4c2a-a1a4-c23d6b7ba247?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20220311%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220311T083815Z&X-Amz-Expires=300&X-Amz-Signature=f4effb5b6359fc74dcb02cdaa3529a2f29a57edc84dc3f876be90c8544fe11db&X-Amz-SignedHeaders=host&actor_id=73891760&key_id=0&repo_id=468667248&response-content-disposition=attachment%3B%20filename%3DTerraTechSetup.zip&response-content-type=application%2Foctet-stream"), @"C:\Temp\TerraTechSetup.zip");
                web.DownloadFileCompleted += TerraTechDown;
            }
        }
        private static void TerraTechDown(object sender, AsyncCompletedEventArgs a)
        {
            MessageBox.Show("Downloading File is Completed", "TerraTech Game Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ZipFile.ExtractToDirectory(@"C:\Temp\TerraTechSetup.zip", @"C:\TerraTech");
            if (File.Exists(@"C:\TerraTech\setup.exe"))
            {
                Process.Start(@"C:\TerraTech\setup.exe");
            }
            Environment.Exit(412);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you for using this Program, Made by GlebYoutuber", "TerraTech Game Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
