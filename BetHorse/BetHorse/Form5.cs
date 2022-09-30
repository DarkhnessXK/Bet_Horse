using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace BetHorse
{
    public partial class Form5 : Form
    {
        SoundPlayer player;
        string valorboleto = "";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
            textBox1.MaxLength = 31;
            //Definir Mascara
            maskValor.Text = "";
            maskValor.Mask = "000,000,000-00";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //500 de dinheiro
                    label4.Text = "R$15,00";
                    valorboleto = "0000001500";
                    break;
                case 1:
                    //1000 de dinheiro
                    label4.Text = "R$45,00";
                    valorboleto = "0000004500";
                    break;
                case 2:
                    //1500 de dinheiro
                    label4.Text = "R$75,00";
                    valorboleto = "0000007500";
                    break;
                case 3:
                    //2000 de dinheiro
                    label4.Text = "R$99,00";
                    valorboleto = "0000009900";
                    break;
                case 4:
                    //50000 de dinheiro
                    label4.Text = "R$195,00";
                    valorboleto = "0000019500";
                    break;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
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

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        async Task PausaComTaskDelay()
        {
            await Task.Delay(1000);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            player = new SoundPlayer("cavalo1.wav");
            string valor = maskValor.Text;

            if (ValidaCPF.IsCpf(valor))
            {
                player.Play();
                toolStripStatusLabel1.Text = "Gerando o boleto";
                toolStripStatusLabel1.ForeColor = Color.Green;
                button1.Enabled = false;

                Program.loginForm.Send("boleto/" + Program.painelForm.lbname.Text);
                await PausaComTaskDelay();
                using (var doc = new PdfSharp.Pdf.PdfDocument())
                {
                    //Variáveis
                    var page = doc.AddPage();
                    var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                    var font = new PdfSharp.Drawing.XFont("Segou UI", 15);

                    //Primeira Linha
                    textFormatter.DrawString("|033-7| 03399.69925 58700.0000 8008.1018 8 7733" + valorboleto, new PdfSharp.Drawing.XFont("Arial", 15), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(122, 11, page.Width, page.Height));
                    
                    //Cedente
                    textFormatter.DrawString("BetHorse LTDA", new PdfSharp.Drawing.XFont("Arial", 11), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(16, 85, page.Width, page.Height));

                    //Dados
                    textFormatter.DrawString(label5.Text, new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(20, 120, page.Width, page.Height));
                    textFormatter.DrawString(label6.Text, new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(110, 120, page.Width, page.Height));
                    textFormatter.DrawString("DV", new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(200, 120, page.Width, page.Height));
                    textFormatter.DrawString("N", new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(270, 120, page.Width, page.Height));
                    textFormatter.DrawString(label5.Text, new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(350, 120, page.Width, page.Height));
                    textFormatter.DrawString("102", new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(160, 150, page.Width, page.Height));
                    textFormatter.DrawString("R$", new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(245, 150, page.Width, page.Height));
                    
                    //Instruções
                    textFormatter.DrawString("APÓS O VENCIMENTO COBRAR MULTA DE 2% MAIS\nJUROS MORATÓRIOS DE 0,05 % AO DIA\nNÃO RECEBER APÓS 30 DIAS DO VENCIMENTO", new PdfSharp.Drawing.XFont("Arial", 13), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(16, 185, page.Width, page.Height));

                    //Coluna Vencimento
                    DateTime data = DateTime.Parse(label5.Text);
                    data = data.AddDays(7);
                    textFormatter.DrawString(data.ToString("dd/MM/yyyy"), new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(480, 55, page.Width, page.Height));
                    textFormatter.DrawString("040611/6353371", new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(475, 80, page.Width, page.Height));
                    textFormatter.DrawString("000000000000054-0", new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(470, 105, page.Width, page.Height));
                    textFormatter.DrawString(label4.Text.Replace("R$", ""), new PdfSharp.Drawing.XFont("Arial", 9), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(495, 135, page.Width, page.Height));
                    textFormatter.DrawString("BetHorse LTDA", new PdfSharp.Drawing.XFont("Arial", 11), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(16, 325, page.Width, page.Height));

                    //Informação do Cliente
                    textFormatter.DrawString(textBox1.Text, new PdfSharp.Drawing.XFont("Arial", 11), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(16, 370, page.Width, page.Height));
                    textFormatter.DrawString(maskValor.Text, new PdfSharp.Drawing.XFont("Arial", 11), PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(16, 385, page.Width, page.Height));

                    //Imagens
                    graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile(@".\Img\banco.png"), 7, 0);
                    graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile(@".\Img\modelo.png"), 7, 25);
                    graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile(@".\Img\code.png"), 9, 480);

                    //Salvar Arquivo
                    doc.Save(@".\Boleto.pdf");
                    System.Diagnostics.Process.Start(@".\Boleto.pdf");
                }

                //End
                button1.Enabled = true;
                player.Stop();
            }
            else
            {
                toolStripStatusLabel1.Text = "Cpf inválido";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }  
        }
    }
}
