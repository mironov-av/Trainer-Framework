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
using TrainerFramework.Utilites;
using TrainerFramework;
using System.IO;
using System.Windows.Controls;
using System.Runtime.InteropServices.ComTypes;
using System.Media;

namespace TrainerFramework
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        public bool UseMnemonic { get; set; }

        uint pAddress = 0x18C76C8;
        int ptrOffset = -274;
        uint offset = 0;

        ProcessMemoryReader mreader = new ProcessMemoryReader();

        int bytesOut = 0;

        private void Main_Load(object sender, EventArgs e)
        {
            Process process = Process.GetProcessesByName("").ToList().FirstOrDefault();
            if (process != null)
            {
                mreader.ReadProcess = process;
                mreader.OpenProcess();

                offset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(pAddress + (uint)process.Modules[0].BaseAddress), 4, out bytesOut), 0);
                if (ptrOffset < 0)
                    offset -= (uint)(Math.Abs(ptrOffset));
                else
                    offset += (uint)ptrOffset;
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.NumPad1)
            {
                clearToggle1_Click(clearToggle1.IsOn, null);
            }
            if (e.Control == true && e.KeyCode == Keys.NumPad2)
            {
                clearToggle2_Click(clearToggle2.IsOn, null);
            }
            if (e.Control == true && e.KeyCode == Keys.NumPad3)
            {
                clearToggle3_Click(clearToggle3.IsOn, null);
            }
            if (e.Control == true && e.KeyCode == Keys.NumPad4)
            {
                clearToggle4_Click(clearToggle4.IsOn, null);
            }
        }

        private void clearToggle1_Click(object sender, EventArgs e)
        {
            mreader.WriteMemory((IntPtr)offset + 0x7818, BitConverter.GetBytes(9999999), out bytesOut);
            if (clearToggle1.IsOn == true)
            {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\4.wav");
                player.Play();
            }
            else
                {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\2.wav");
                player.Play();
            }
        }

        private void clearToggle2_Click(object sender, EventArgs e)
        {
            
            if (clearToggle2.IsOn == true)
            {
                for (int i = 0; i < 68; i++)
                {
                    mreader.WriteMemory((IntPtr)offset + 0x7890 + i * 2, new byte[2] { (byte)i, 0x20, }, out bytesOut);
                }

                for (int i = 0; i < 68; i++)
                {
                    mreader.WriteMemory((IntPtr)offset + 0x8392 + i, new byte[] { (byte)255 }, out bytesOut);
                }
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\4.wav");
                player.Play();
            }
            else
            {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\2.wav");
                player.Play();
            }
        }

        private void clearToggle3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 128; i++)
            {
                mreader.WriteMemory((IntPtr)offset + 0x7cc0 + i * 2, new byte[2] { (byte)i, 0x90, }, out bytesOut);
            }

            for (int i = 0; i < 128; i++)
            {
                mreader.WriteMemory((IntPtr)offset + 0x7dc0 + i, new byte[] { (byte)99 }, out bytesOut);
            }
            if (clearToggle3.IsOn == true)
            {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\4.wav");
                player.Play();
            }
            else
            {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\2.wav");
                player.Play();
            }
        }

        private void clearToggle4_Click(object sender, EventArgs e)
        {
            mreader.WriteMemory((IntPtr)offset + 0x8160, new byte[9]{0x7f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff }, out bytesOut);
            if (clearToggle4.IsOn == true)
            {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\4.wav");
                player.Play();
            }
            else
            {
                SoundPlayer player = new SoundPlayer(@"F:\VisualStudio2019Repos\TrainerFramework\TrainerFramework\Sounds\2.wav");
                player.Play();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobject = panel1.CreateGraphics();
            Brush one = new SolidBrush(Color.DarkGray);
            Pen redpen = new Pen(one, 5);

            gobject.DrawLine(redpen, 550,10,10,10);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gobject = panel2.CreateGraphics();
            Brush one = new SolidBrush(Color.DarkGray);
            Pen redpen = new Pen(one, 5);

            gobject.DrawLine(redpen, 550, 10, 10, 10);
        }
    }
}
