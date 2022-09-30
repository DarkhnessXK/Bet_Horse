using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BetHorse
{
    public partial class Form14 : Form
    {
        int tempo = 25;
        int tempo_saiu = 8;

        public Form14()
        {
            InitializeComponent();
            //Valor - Marca d'agua -
            List<TextBox> tList = new List<TextBox>();
            List<string> sList = new List<string>();
            tList.Add(textBox1);
            sList.Add("Dinheiro");
            SetCueBanner(ref tList, sList);
        }

        //Marca d'agua
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr i, string str);

        void SetCueBanner(ref List<TextBox> textBox, List<string> CueText)
        {
            for (int x = 0; x < textBox.Count; x++)
            {
                SendMessage(textBox[x].Handle, 0x1501, (IntPtr)1, CueText[x]);
            }
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 9;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tempo--;

            if (tempo == 0)
            {
                this.Close();
            }
            else
            {
                label7.Text = "Tempo: " + tempo;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                toolStripStatusLabel1.Text = "Informe o dinheiro que quer enviar";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                if (Convert.ToInt32(textBox1.Text) < 0)
                {
                    toolStripStatusLabel1.Text = "Dinheiro inválido";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    if (Convert.ToInt32(textBox1.Text) > Convert.ToInt32(label6.Text))
                    {
                        toolStripStatusLabel1.Text = "Você não tem dinheiro suficiente";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (Convert.ToInt32(textBox1.Text) == 0)
                        {
                            toolStripStatusLabel1.Text = "Você não pode enviar R$0";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            label3.Text = "R$" + textBox1.Text;
                            toolStripStatusLabel1.Text = "Dinheiro confirmado";
                            toolStripStatusLabel1.ForeColor = Color.Green;
                            timer1.Stop();
                            label5.Visible = true;
                            label4.Visible = true;
                            textBox1.Enabled = false;
                            button1.Enabled = false;
                            timer2.Start();
                            Program.loginForm.Send("trade/" + Program.painelForm.lbname.Text + "/" + label3.Text.Replace("R$", "") + "/" + label2.Text);
                        }
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            tempo_saiu--;

            if (tempo_saiu == 0)
            {
                label9.Text = "   Sem resposta";
                label9.ForeColor = Color.Red;
                label9.Visible = true;
                timer2.Stop();
            }

            if (label4.Text == "Esperando")
            {
                label4.Text = "Esperando.";
            }else if (label4.Text == "Esperando.")
            {
                label4.Text = "Esperando..";
            }else if (label4.Text == "Esperando..")
            {
                label4.Text = "Esperando...";
            }else if (label4.Text == "Esperando...")
            {
                label4.Text = "Esperando";
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Validando se o usuario aperto no teclado Ctrl + V
            if (e.Control && e.KeyValue == 86)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
