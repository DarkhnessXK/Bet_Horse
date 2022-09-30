using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace BetHorse
{
    public partial class Form7 : Form
    {
        SoundPlayer player = new SoundPlayer("corrida2.wav");
        private static string Path = Environment.CurrentDirectory + @"\Lembrar.ini";

        public Form7()
        {
            InitializeComponent();
        }

        async Task PausaComTaskDelay()
        {
            await Task.Delay(10000);
        }

        private async void Form7_LoadAsync(object sender, EventArgs e)
        {
            if (File.Exists(Path))
            {
                if (Arquivo.ReadIni("Lembrar", "Musica", Path) == "True")
                {
                    player.Play();
                    await PausaComTaskDelay();
                    player.Stop();
                }
            }
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.Stop();
        }

        private void Form7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
