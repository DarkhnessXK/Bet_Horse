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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mensagem = "";

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                toolStripStatusLabel1.Text = "Preencha todos os campos";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                if (textBox1.Text.Contains("/") == true || textBox2.Text.Contains("/") == true)
                {
                    toolStripStatusLabel1.Text = "Não é permitido o uso de '/' nos campos";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    mensagem = textBox2.Text.Replace(System.Environment.NewLine, " ");
                    if (Convert.ToInt32(label4.Text) == 0)
                    {
                        Program.loginForm.IniciarConexao(textBox1.Text, "", "suporte/" + comboBox1.Text + "/" + mensagem);
                    }
                    else
                    {
                        Program.loginForm.Send("suporte/" + comboBox1.Text + "/" + mensagem + "/" + textBox1.Text);
                        toolStripStatusLabel1.Text = "Mensagem Enviada";
                        toolStripStatusLabel1.ForeColor = Color.Green;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
            if (label4.Text == "0")
            {
                label5.Visible = true;
            }
            else
            {
                textBox1.Text = Program.painelForm.lbname.Text;
                textBox1.Enabled = false;
            }
        }

        private void Form9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
