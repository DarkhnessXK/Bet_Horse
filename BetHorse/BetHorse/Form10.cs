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
    public partial class Form10 : Form
    {
        string pular = System.Environment.NewLine;

        public Form10()
        {
            InitializeComponent();
            textBox1.AppendText(
                "                           >> Caixa de Mensagens <<                           "+pular+pular+
                " Aqui você recebe informações importantes e mensagens dos administradores."
                );
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsNumber(e.KeyChar) || Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form9>().Count() > 0)
            {
                Program.supForm.Visible = false;
                Program.supForm.Visible = true;
            }
            else
            {
                //Novo
                Program.supForm = new Form9();
                Program.supForm.label4.Text = "1";
                Program.supForm.Visible = false;
                Program.supForm.Visible = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            //Validando se o usuario aperto no teclado Ctrl + V
            if (e.Control && e.KeyValue == 86)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        //Add mensagem
        public void AddMensagem(string mensagem)
        {
            if (mensagem == "")
            {
                //passa
            }
            else
            {
                string[] quebrar = mensagem.Split(',');

                //Verificar oq não existe
                for (int a = 0; a < quebrar.Length-1; a++)
                {
                    if (listBox1.FindString(quebrar[a]) != -1)
                    {
                        //passa
                    }
                    else
                    {
                        listBox1.Items.Add(quebrar[a]);
                    }
                }
            }

        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.loginForm.Send("conmensagem/" + listBox1.Text + "/" + Program.painelForm.lbname.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Text != "")
            {
                Program.loginForm.Send("deletarmensagem/" + Program.painelForm.lbname.Text + "/" + listBox1.Text);
                listBox1.Items.Remove(listBox1.Text);
            }
        }

        private void Form10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
