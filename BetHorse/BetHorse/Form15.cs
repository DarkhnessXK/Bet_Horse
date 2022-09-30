using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BetHorse
{
    public partial class Form15 : Form
    {
        private string Path = Environment.CurrentDirectory + @"\Lembrar.ini";

        public Form15()
        {
            InitializeComponent();
            //Ler arquivo 'Lembrar.ini'
            if (File.Exists(Path))
            {
                if (Arquivo.ReadIni("Lembrar", "Musica", Path) == "True")
                {
                    checkBox1.Checked = true;
                }

                if (Arquivo.ReadIni("Lembrar", "Trade", Path) == "True")
                {
                    checkBox2.Checked = true;
                }
            }

            //ToolTip
            toolTip1.SetToolTip(checkBox2, "Irá recusar todas as negociações automaticamente");
        }

        private void Form15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Música
            if (checkBox1.Checked == true)
            {
                Arquivo.WriteINI("Lembrar", "Musica", "True", Path);
            }
            else
            {
                Arquivo.WriteINI("Lembrar", "Musica", "False", Path);
            }

            //Negociações
            if (checkBox2.Checked == true)
            {
                Arquivo.WriteINI("Lembrar", "Trade", "True", Path);
            }
            else
            {
                Arquivo.WriteINI("Lembrar", "Trade", "False", Path);
            }

            //Terminar
            Program.painelForm.toolStripStatusLabel1.Text = "Configurações salvas";
            Program.painelForm.toolStripStatusLabel1.ForeColor = Color.Green;
            this.Close();
        }
    }
}
