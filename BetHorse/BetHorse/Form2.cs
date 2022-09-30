using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetHorse
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void Status(string mensagem, string cor)
        {
            toolStripStatusLabel1.Text = mensagem;
            if (cor == "Green")
            {
                toolStripStatusLabel1.ForeColor = Color.Green;
            }
            else
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 14;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button2.PerformClick();
            }
            if (!(char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (textBox2.Text != textBox3.Text)
                    {
                        Status("As senhas não conferem", "Red");
                    }
                    else
                    {
                        if (textBox1.Text.Contains("/") == true || textBox2.Text.Contains("/") == true)
                        {
                            Status("Não é permitido o uso do '/' no seu login ou senha", "Red");
                        }
                        else
                        {
                            string[] teste = textBox1.Text.Split(' ');

                            //Corrigir o nome - ESTRANHO -
                            var upper1 = char.ToUpper(teste[0][0]) + teste[0].Substring(1).ToLower();
                            string nome = upper1;
                            Program.loginForm.IniciarConexao(nome, textBox2.Text, "cadastrar");
                        }
                    }
                }
                else
                {
                    Status("Informe sua senha", "Red");
                }
            }
            else
            {
                Status("Informe o loguin", "Red");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button2.PerformClick();
            }

            if (!(char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = true;
            }
        }
    }
}
