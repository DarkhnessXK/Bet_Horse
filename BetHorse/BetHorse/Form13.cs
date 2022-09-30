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
    public partial class Form13 : Form
    {
        private static string Path = Environment.CurrentDirectory + @"\Lembrar.ini";
        //Variáveis
        int tempo = 7;

        //Construtores
        public Form13()
        {
            InitializeComponent();
            
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Verificar se o jogador desativou as negociações
            if (File.Exists(Path))
            {
                if (Arquivo.ReadIni("Lembrar", "Trade", Path) == "False")
                {
                    button1.PerformClick();
                }
            }

            if (tempo == 0)
            {
                button1.PerformClick();
            }
            else
            {
                tempo--;
            }
            button1.Text = "Não " + tempo.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.loginForm.Send("traderesposta/recusou/"+label2.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.loginForm.Send("traderesposta/aceitou/"+label2.Text+"/"+Program.painelForm.lbname.Text+"/"+label1.Text.Split(' ')[7].Replace("R$", ""));
            //timer1.Stop();
            this.Close();
        }
    }
}
