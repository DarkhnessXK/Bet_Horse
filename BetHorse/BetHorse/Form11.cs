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
    public partial class Form11 : Form
    {
        private string pular = System.Environment.NewLine;

        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Vertical;

            //Regras do BetHorse
            textBox1.AppendText(
                //Texto aqui
                "                                                                      >> BetHorse <<"+pular+pular+
                "Regras Gerais das Apostas"
                + pular + pular +
                "Se o resultado de um mercado não puder ser verificado oficialmente, nos reservamos o direito de adiar as decisões até a confirmação oficial."
                + pular + pular +
                "Se tiverem sido oferecidos mercados quando o resultado já era sabido, nos reservamos o direito de anular quaisquer apostas."
                + pular + pular +
                "No caso de preços exibidos ou calculados de maneira obviamente incorreta, nos reservamos o direito de anular apostas. Isso inclui uma variação de mais de 100% no pagamento, comparado à média do mercado."
                + pular + pular +
                "Se a cobertura tiver que ser abandonada e a partida finalizar normalmente, todos os mercados serão decididos de acordo com o resultado final. Se o resultado de um mercado não puder ser verificado oficialmente, nos reservamos o direito de anulá-lo."
                + pular + pular +
                "No caso de uma decisão incorreta de mercados, nos reservamos o direito de corrigi-la em qualquer momento."
                + pular + pular +
                "Se uma partida não corresponder a um formato usual (ex: duração de partida ou procedimento de contagem não usuais, formato da partida etc), nos reservamos o direito de anular qualquer mercado."
                + pular + pular +
                "Se as regras ou formato de uma partida diferem da norma por nós aceita, nos reservamos o direito de anular qualquer mercado."
                + pular + pular +
                "Todos os mercados são considerados apenas pelo tempo regular, salvo disposto em contrário."
                + pular + pular +
                "Se uma partida não for completada ou jogada (ex: desclassificação, interrupção, abandono, alterações etc), todos os mercados indefinidos serão anulados."
                );
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsNumber(e.KeyChar) || Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
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

        private void Form11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
