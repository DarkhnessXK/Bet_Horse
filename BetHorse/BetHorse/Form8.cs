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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 14;
            textBox2.MaxLength = 14;
            textBox4.MaxLength = 14;
            textBox9.MaxLength = 35;
            textBox5.MaxLength = 9;
            textBox7.MaxLength = 239;
            textBox10.MaxLength = 22;
            textBox7.ScrollToCaret();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Program.loginForm.Send("noticia/"+textBox9.Text);
            button7.Enabled = false;
            timer1.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox7.Text.Contains("/") || textBox8.Text == "" || textBox8.Text.Contains("/") || textBox10.Text == "" || textBox10.Text.Contains("/"))
            {
                MessageBox.Show("Texto inválido");
            }
            else
            {
                Program.loginForm.Send("mensagem/"+ textBox8.Text + "/" + textBox10.Text + "/" + textBox7.Text.Replace(System.Environment.NewLine, "X"));
                textBox10.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                MessageBox.Show("Mensagem enviada");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Preencha os campos Nome e Valor");
            }
            else
            {
                if (textBox4.Text.Contains("/") == true || textBox5.Text.Contains("/") == true)
                {
                    MessageBox.Show(" - / - Inválido");
                }
                else
                {
                    Program.loginForm.Send("admadd/"+textBox4.Text+"/"+textBox5.Text+"/"+Program.painelForm.lbname.Text);
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Preencha os campos Nome e Valor");
            }
            else
            {
                if (textBox4.Text.Contains("/") == true || textBox5.Text.Contains("/") == true)
                {
                    MessageBox.Show(" - / - Inválido");
                }
                else
                {
                    Program.loginForm.Send("admfall/" + textBox4.Text + "/" + textBox5.Text + "/" + Program.painelForm.lbname.Text);
                }
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            //Validando se o usuario aperto no teclado Ctrl + V
            if (e.Control && e.KeyValue == 86)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text.Contains("/") == true)
            {
                MessageBox.Show("Informe o nome corretamente - '/'  inválido");
            }
            else
            {
                if (textBox1.Text == Program.painelForm.lbname.Text)
                {
                    MessageBox.Show("Você não pode kikar você mesmo");
                }
                else
                {
                    Program.loginForm.Send("kick/" + textBox1.Text + "/" + Program.painelForm.lbname.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text.Contains("/") == true)
            {
                MessageBox.Show("Informe o nome corretamente - '/'  inválido");
            }
            else
            {
                Program.loginForm.Send("banido/" + textBox2.Text + "/" + Program.painelForm.lbname.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox2.Text.Contains("/") == true)
            {
                MessageBox.Show("Informe o nome corretamente - '/'  inválido");
            }
            else
            {
                Program.loginForm.Send("desbanir/" + textBox2.Text + "/" + Program.painelForm.lbname.Text);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Text != "")
            {
                Program.loginForm.Send("admdadossup/"+listBox1.Text+"/"+Program.painelForm.lbname.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Text != "")
            {
                if (listBox1.Text == "1001")
                {
                    MessageBox.Show("Essa mensagem de suporte não pode ser apagada");
                }
                else
                {
                    Program.loginForm.Send("admdeletesup/" + listBox1.Text + "/" + Program.painelForm.lbname.Text);
                    listBox1.Items.Remove(listBox1.Text);
                }
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            //Validando se o usuario aperto no teclado Ctrl + V
            if (e.Control && e.KeyValue == 86)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsNumber(e.KeyChar) || Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        int tempo = 8;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tempo == 0)
            {
                timer1.Stop();
                button7.Text = "Enviar";
                button7.Enabled = true;
                tempo = 8;
            }
            else
            {
                tempo--;
                button7.Text = "Enviar " + tempo;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
