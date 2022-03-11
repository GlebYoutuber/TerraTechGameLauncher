using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using AxXtremeSkinFramework;
using XtremeSuiteControls;
using System.Data.Common;
using System.Windows;
using System.IO;
using System.Media;
using Extracts;
using System.IO.Compression;

namespace TerraTechLauncher
{
    public partial class TerraTechLauncher : Form
    {
        static public AxXtremeSkinFramework.AxSkinFramework skinFramework;
        public TerraTechLauncher()
        {
            Directory.CreateDirectory(@"C:\Temp");
            Extrct x = new Extrct();
            x.Extract("TerraTechLauncher", @"C:\Temp", "Resources", "iPhone.cjstyles");
            x.Extract("TerraTechLauncher", @"C:\Temp", "Resources", "TerraTechDownloader.zip");
            x.Extract("TerraTechLauncher", @"C:\Temp", "Resources", "VentureTerraTechCombat.wav");
            if (File.Exists(@"C:\Temp\VentureTerraTechCombat.wav"))
            {
                SoundPlayer terratech = new SoundPlayer();
                terratech = new SoundPlayer(@"C:\Temp\VentureTerraTechCombat.wav");
                terratech.PlayLooping();
            }
            else
            {
                Trace.Fail("Music not Found");
            }
            if (File.Exists(@"C:\Temp\iPhone.cjstyles"))
            {

            }
            else
            {
                Trace.Fail("Loaded Skin is not Found");
            }
            InitializeComponent();
            this.Text = "TerraTech Game Launcher";
            skinFramework = new AxSkinFramework();
            ((System.ComponentModel.ISupportInitialize)(skinFramework)).BeginInit();
            this.Controls.Add(skinFramework);
            ((System.ComponentModel.ISupportInitialize)(skinFramework)).EndInit();
            Trace.WriteLine("Loading Skin iPhone...");
            skinFramework.LoadSkin(@"C:\Temp\iPhone.cjstyles", String.Empty);
            skinFramework.Update();
            Trace.Write("Loading Skin is Complete");
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible)
            {
                skinFramework.ApplyWindow(this.Handle.ToInt32());
                this.BackColor = skinFramework.GetColor(XtremeSkinFramework.XTPColorManagerColor.STDCOLOR_BACKGROUND);
            }
            if(this.Visible == false)
            {
                Trace.Fail("Loaded Skin is not Found");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Games\TerraTech\TerraTechWin64.exe"))
            {
                Trace.WriteLine("File is Found, Launching File...");
                skinFramework.RemoveAllWindows();
                Process.Start(@"C:\Games\TerraTech\TerraTechWin64.exe");
                skinFramework.RemoveWindow(this.Handle.ToInt32());
                Trace.WriteLine("Exiting with Code 31023");
                Environment.Exit(31023);
            }
            else
            {
                //Show MsgBox and Exit with Code -144
                MessageBox.Show("File in TerraTech not found, Exiting...", "TerraTech Launcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine("File is not Found, Exiting with Exit Code -144");
                Environment.Exit(-144);
            }
        }

        private void rndcolor_Tick(object sender, EventArgs e)
        {
            //Effects :D
            rndcolor.Stop();
            label1.ForeColor = Color.Yellow;
            randcolor.Start();
        }

        private void randcolor_Tick(object sender, EventArgs e)
        {
            //Effects with Text :D
            randcolor.Stop();
            label1.ForeColor = Color.FromArgb(100, 23, 120);
            rndcolor.Start();
        }

        private void dwnload_terratech_Click(object sender, EventArgs e)
        {
            ZipFile.ExtractToDirectory(@"C:\Temp\TerraTechDownloader.zip", @"C:\Temp");
            Process.Start(@"C:\Temp\TerraTechDownloader.exe");
            MessageBox.Show("Please Relaunch of this Launcher after Finishing Downloading this Game", "TerraTech Game Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Environment.Exit(1021);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
