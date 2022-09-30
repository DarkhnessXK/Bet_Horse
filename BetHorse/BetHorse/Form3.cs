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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //Valor - Marca d'agua -
            List<TextBox> tList = new List<TextBox>();
            List<string> sList = new List<string>();
            tList.Add(textBox2);
            sList.Add("Ex: 900");
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

        //Autenticar
        private void Autenticar(string dinheiro, string nome, string emblema, string ta, string tg)
        {
            //Modificar o dinheiro - ToInt -
            int recebeu = Convert.ToInt32(dinheiro) - Convert.ToInt32(lbmoney.Text);
            
            if (lbmoney.Text != dinheiro && lbmoney.Text != "0")
            {
                if (recebeu > 0)
                {
                    textBox3.AppendText("Você recebeu R$" + Convert.ToString(recebeu)+System.Environment.NewLine);
                }
                if (recebeu < 0)
                {
                    textBox3.AppendText("Você perdeu R$" + Convert.ToString(-recebeu) + System.Environment.NewLine);
                }
            }
            lbmoney.Text = dinheiro;
            if (nome == "")
            {
                //NADA -:> Recebe só 1 e para
            }
            else
            {
                lbname.Text = nome;
            }

            if(emblema == "icon1")
            {//Rank1 - Emblema Inicial
                pictureBox3.Image = Properties.Resources.inicial;
            }
            else if (emblema == "icon2")
            {//Rank2 - Emblema dado depois de 5 wins
                pictureBox3.Image = Properties.Resources.icon4;
            }
            else if (emblema == "icon3")
            {//Rank3 - Emblema dado depois de 15 wins
                pictureBox3.Image = Properties.Resources.icon3;
            }
            else if (emblema == "icon4")
            {//Rank4 - Emblema dado depois de 20 wins
                pictureBox3.Image = Properties.Resources.icon2;
            }

            lbtotal_ap.Text = ta;
            lbtotal_g.Text = tg;
        }

        //Adicionar Corrida
        public void AdicionarCorrida(string nome, string blind, string tempo)
        {
            // 1 - nome
            // 2 - blind
            // 3 - tempo - TIMER -
            string[] hora = tempo.Split(':'); // [0] - hora | [1] - minutos | [2] - segundos
            int index = listBox1.FindString(nome);

            if (index != -1)
            {
                //listBox1.SetSelected(index, true);
            }
            else
            {
                listBox1.Items.Add(nome);
            }

            if (nome == "Black Edition")
            {
                if(black.ToLongTimeString() == "00:00:00")
                {
                    black = black.AddHours(Convert.ToInt32(hora[0]));
                    black = black.AddMinutes(Convert.ToInt32(hora[1]));
                    black = black.AddSeconds(Convert.ToInt32(hora[2]));
                    label4.Text = "["+black.ToLongTimeString()+"]";
                    label27.Text = blind;
                }
            }else if(nome == "Fossa Party")
            {
                if (fossa.ToLongTimeString() == "00:00:00")
                {
                    fossa = fossa.AddHours(Convert.ToInt32(hora[0]));
                    fossa = fossa.AddMinutes(Convert.ToInt32(hora[1]));
                    fossa = fossa.AddSeconds(Convert.ToInt32(hora[2]));
                    label5.Text = "[" + fossa.ToLongTimeString() + "]";
                    label28.Text = blind;
                }
            }
            else if(nome == "PlayBoy Only")
            {
                if (play.ToLongTimeString() == "00:00:00")
                {
                    play = play.AddHours(Convert.ToInt32(hora[0]));
                    play = play.AddMinutes(Convert.ToInt32(hora[1]));
                    play = play.AddSeconds(Convert.ToInt32(hora[2]));
                    label6.Text = "[" + play.ToLongTimeString() + "]";
                    label29.Text = blind;
                }
            }
            //Deluxe, Legend, United, Rushback, Never, Vortex, 
            else if (nome == "Deluxe")
            {
                if (deluxe.ToLongTimeString() == "00:00:00")
                {
                    deluxe = deluxe.AddHours(Convert.ToInt32(hora[0]));
                    deluxe = deluxe.AddMinutes(Convert.ToInt32(hora[1]));
                    deluxe = deluxe.AddSeconds(Convert.ToInt32(hora[2]));
                    label7.Text = "[" + deluxe.ToLongTimeString() + "]";
                    label30.Text = blind;
                }
            }
            else if (nome == "Legend")
            {
                if (legend.ToLongTimeString() == "00:00:00")
                {
                    legend = legend.AddHours(Convert.ToInt32(hora[0]));
                    legend = legend.AddMinutes(Convert.ToInt32(hora[1]));
                    legend = legend.AddSeconds(Convert.ToInt32(hora[2]));
                    label8.Text = "[" + legend.ToLongTimeString() + "]";
                    label31.Text = blind;
                }
            }
            else if (nome == "United")
            {
                if (united.ToLongTimeString() == "00:00:00")
                {
                    united = united.AddHours(Convert.ToInt32(hora[0]));
                    united = united.AddMinutes(Convert.ToInt32(hora[1]));
                    united = united.AddSeconds(Convert.ToInt32(hora[2]));
                    label9.Text = "[" + united.ToLongTimeString() + "]";
                    label32.Text = blind;
                }
            }
            else if (nome == "Rushback")
            {
                if (rushback.ToLongTimeString() == "00:00:00")
                {
                    rushback = rushback.AddHours(Convert.ToInt32(hora[0]));
                    rushback = rushback.AddMinutes(Convert.ToInt32(hora[1]));
                    rushback = rushback.AddSeconds(Convert.ToInt32(hora[2]));
                    label10.Text = "[" + rushback.ToLongTimeString() + "]";
                    label33.Text = blind;
                }
            }
            else if (nome == "Never")
            {
                if (never.ToLongTimeString() == "00:00:00")
                {
                    never = never.AddHours(Convert.ToInt32(hora[0]));
                    never = never.AddMinutes(Convert.ToInt32(hora[1]));
                    never = never.AddSeconds(Convert.ToInt32(hora[2]));
                    label11.Text = "[" + never.ToLongTimeString() + "]";
                    label34.Text = blind;
                }
            }
            else if (nome == "Vortex")
            {
                if (vortex.ToLongTimeString() == "00:00:00")
                {
                    vortex = vortex.AddHours(Convert.ToInt32(hora[0]));
                    vortex = vortex.AddMinutes(Convert.ToInt32(hora[1]));
                    vortex = vortex.AddSeconds(Convert.ToInt32(hora[2]));
                    label12.Text = "[" + vortex.ToLongTimeString() + "]";
                    label35.Text = blind;
                }
            }
            //Hunted, Blood, River, Flawless, Destiny, 
            else if (nome == "Hunted")
            {
                if (hunted.ToLongTimeString() == "00:00:00")
                {
                    hunted = hunted.AddHours(Convert.ToInt32(hora[0]));
                    hunted = hunted.AddMinutes(Convert.ToInt32(hora[1]));
                    hunted = hunted.AddSeconds(Convert.ToInt32(hora[2]));
                    label13.Text = "[" + hunted.ToLongTimeString() + "]";
                    label36.Text = blind;
                }
            }
            else if (nome == "Blood")
            {
                if (blood.ToLongTimeString() == "00:00:00")
                {
                    blood = blood.AddHours(Convert.ToInt32(hora[0]));
                    blood = blood.AddMinutes(Convert.ToInt32(hora[1]));
                    blood = blood.AddSeconds(Convert.ToInt32(hora[2]));
                    label14.Text = "[" + blood.ToLongTimeString() + "]";
                    label37.Text = blind;
                }
            }
            else if (nome == "River")
            {
                if (river.ToLongTimeString() == "00:00:00")
                {
                    river = river.AddHours(Convert.ToInt32(hora[0]));
                    river = river.AddMinutes(Convert.ToInt32(hora[1]));
                    river = river.AddSeconds(Convert.ToInt32(hora[2]));
                    label15.Text = "[" + river.ToLongTimeString() + "]";
                    label38.Text = blind;
                }
            }
            else if (nome == "Flawless")
            {
                if (flawless.ToLongTimeString() == "00:00:00")
                {
                    flawless = flawless.AddHours(Convert.ToInt32(hora[0]));
                    flawless = flawless.AddMinutes(Convert.ToInt32(hora[1]));
                    flawless = flawless.AddSeconds(Convert.ToInt32(hora[2]));
                    label16.Text = "[" + flawless.ToLongTimeString() + "]";
                    label39.Text = blind;
                }
            }
            else if (nome == "Destiny")
            {
                if (destiny.ToLongTimeString() == "00:00:00")
                {
                    destiny = destiny.AddHours(Convert.ToInt32(hora[0]));
                    destiny = destiny.AddMinutes(Convert.ToInt32(hora[1]));
                    destiny = destiny.AddSeconds(Convert.ToInt32(hora[2]));
                    label17.Text = "[" + destiny.ToLongTimeString() + "]";
                    label40.Text = blind;
                }
            }
            //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
            else if (nome == "Fun")
            {
                if (fun.ToLongTimeString() == "00:00:00")
                {
                    fun = fun.AddHours(Convert.ToInt32(hora[0]));
                    fun = fun.AddMinutes(Convert.ToInt32(hora[1]));
                    fun = fun.AddSeconds(Convert.ToInt32(hora[2]));
                    label18.Text = "[" + fun.ToLongTimeString() + "]";
                    label41.Text = blind;
                }
            }
            else if (nome == "Chaos")
            {
                if (chaos.ToLongTimeString() == "00:00:00")
                {
                    chaos = chaos.AddHours(Convert.ToInt32(hora[0]));
                    chaos = chaos.AddMinutes(Convert.ToInt32(hora[1]));
                    chaos = chaos.AddSeconds(Convert.ToInt32(hora[2]));
                    label19.Text = "[" + chaos.ToLongTimeString() + "]";
                    label42.Text = blind;
                }
            }
            else if (nome == "Die")
            {
                if (die.ToLongTimeString() == "00:00:00")
                {
                    die = die.AddHours(Convert.ToInt32(hora[0]));
                    die = die.AddMinutes(Convert.ToInt32(hora[1]));
                    die = die.AddSeconds(Convert.ToInt32(hora[2]));
                    label20.Text = "[" + die.ToLongTimeString() + "]";
                    label43.Text = blind;
                }
            }
            else if (nome == "Sacrifice")
            {
                if (sacrifice.ToLongTimeString() == "00:00:00")
                {
                    sacrifice = sacrifice.AddHours(Convert.ToInt32(hora[0]));
                    sacrifice = sacrifice.AddMinutes(Convert.ToInt32(hora[1]));
                    sacrifice = sacrifice.AddSeconds(Convert.ToInt32(hora[2]));
                    label21.Text = "[" + sacrifice.ToLongTimeString() + "]";
                    label44.Text = blind;
                }
            }
            else if (nome == "Heaven")
            {
                if (heaven.ToLongTimeString() == "00:00:00")
                {
                    heaven = heaven.AddHours(Convert.ToInt32(hora[0]));
                    heaven = heaven.AddMinutes(Convert.ToInt32(hora[1]));
                    heaven = heaven.AddSeconds(Convert.ToInt32(hora[2]));
                    label22.Text = "[" + heaven.ToLongTimeString() + "]";
                    label45.Text = blind;
                }
            }
            else if (nome == "Law")
            {
                if (law.ToLongTimeString() == "00:00:00")
                {
                    law = law.AddHours(Convert.ToInt32(hora[0]));
                    law = law.AddMinutes(Convert.ToInt32(hora[1]));
                    law = law.AddSeconds(Convert.ToInt32(hora[2]));
                    label23.Text = "[" + law.ToLongTimeString() + "]";
                    label46.Text = blind;
                }
            }
            else if (nome == "Dragons")
            {
                if (dragons.ToLongTimeString() == "00:00:00")
                {
                    dragons = dragons.AddHours(Convert.ToInt32(hora[0]));
                    dragons = dragons.AddMinutes(Convert.ToInt32(hora[1]));
                    dragons = dragons.AddSeconds(Convert.ToInt32(hora[2]));
                    label24.Text = "[" + dragons.ToLongTimeString() + "]";
                    label47.Text = blind;
                }
            }
            else if (nome == "Soul")
            {
                if (soul.ToLongTimeString() == "00:00:00")
                {
                    soul = soul.AddHours(Convert.ToInt32(hora[0]));
                    soul = soul.AddMinutes(Convert.ToInt32(hora[1]));
                    soul = soul.AddSeconds(Convert.ToInt32(hora[2]));
                    label25.Text = "[" + soul.ToLongTimeString() + "]";
                    label48.Text = blind;
                }
            }
        }

        //Adicionar Online
        public void AdicionarOnline(string nome, string tipo)
        {
            
            if (tipo == "add")
            {
                if (listBox3.Items.Contains(nome))
                {
                    //Passa
                }
                else
                {
                    listBox3.Items.Add(nome);
                    Program.loginForm.Send("conectados"); //Verificar - ALL -
                }
            }
            else
            {
                listBox3.Items.Remove(nome);
            }
        }

        //Receber packet's
        public void EscreverText(string mensagem)
        {
            //Tratamento
            string[] conectados = mensagem.Split(' '); // - ONLINE -
            string[] chat = mensagem.Split('/'); // - DIVERSOS 

            //Escrever no painel
            //textBox5.AppendText(mensagem+System.Environment.NewLine);
            //textBox5.ScrollToCaret();

            //Lista dos usuários online
            if (conectados[0] == "Administrador:")
            {
                for(int a = 0; a < conectados.Length; a++)
                {
                    if(conectados[a] == "saiu")
                    {
                        AdicionarOnline(conectados[1], "saiu");
                    }
                    else
                    {
                        AdicionarOnline(conectados[1], "add");
                    }
                }
            }
            else
            {
                //Chat
                if(chat[0] == "CHAT")
                {
                    richTextBox1.AppendText(chat[1] + ": " + chat[2] + "\n");
                }
                else
                {
                    //Privado
                    if (chat[0] == "privado")
                    {
                        richTextBox1.AppendText("[PM]" + chat[1] + ": " + chat[2] + "\n");
                    }
                    else
                    {
                        //Autenticar
                        if(chat[0] == "Autenticar")
                        {
                            // 1 - dinheiro
                            // 2 - nome
                            // 3 - emblema
                            // 4 - Total de apostas
                            // 5 - Total de ganhos

                            Autenticar(chat[1], chat[2], chat[3], chat[4], chat[5]);
                        }
                        else
                        {
                            if(chat[0] == "corridas")
                            {
                                // 1 - nome
                                // 2 - blind
                                // 3 - tempo - TIMER -

                                AdicionarCorrida(chat[1], chat[2], chat[3]);
                            }
                            else
                            {
                                if (chat[0] == "premio")
                                {
                                    // 1 - nome do ganhador
                                    // 2 - cavalo ganhador
                                    // 3 - prémio(R$)
                                    // 4 - emblema do ganhador
                                    // 5 - nome da corrida
                                    Program.premioForm = new Form7();
                                    Program.premioForm.Visible = true;
                                    Program.premioForm.Show();

                                    if(chat[1] == "")
                                    {
                                        Program.premioForm.label4.Text = "Nenhum ganhador";
                                    }
                                    else
                                    {
                                        Program.premioForm.label4.Text = chat[1];
                                    }
                                    Program.premioForm.label5.Text = chat[2];
                                    Program.premioForm.label6.Text = "R$" + chat[3];
                                    Program.premioForm.groupBox1.Text = chat[5];
                                    if (chat[4] == "icon1")
                                    {//Rank1 - Emblema Inicial
                                        Program.premioForm.pictureBox2.Image = Properties.Resources.inicial;
                                    }
                                    else if (chat[4] == "icon2")
                                    {//Rank2 - Emblema dado depois de 5 wins
                                        Program.premioForm.pictureBox2.Image = Properties.Resources.icon4;
                                    }
                                    else if (chat[4] == "icon3")
                                    {//Rank3 - Emblema dado depois de 15 wins
                                        Program.premioForm.pictureBox2.Image = Properties.Resources.icon3;
                                    }
                                    else if (chat[4] == "icon4")
                                    {//Rank4 - Emblema dado depois de 20 wins
                                        Program.premioForm.pictureBox2.Image = Properties.Resources.icon2;
                                    }
                                    else if (chat[4] == "")
                                    {
                                        Program.premioForm.pictureBox2.Image = Properties.Resources.casawin;
                                    }
                                }
                                else
                                {
                                    if(chat[0] == "aposta")
                                    {
                                        if (chat[1] == "1")
                                        {
                                            toolStripStatusLabel1.Text = "Já apostou";
                                            toolStripStatusLabel1.ForeColor = Color.Red;
                                        }
                                        else
                                        {
                                            toolStripStatusLabel1.Text = "Aposta feita";
                                            toolStripStatusLabel1.ForeColor = Color.Green;
                                        }
                                    }
                                    else
                                    {
                                        if (chat[0] == "procurar")
                                        {
                                            if (chat[1] == "1")
                                            {
                                                //Não encontrado
                                                Program.consultaForm.toolStripStatusLabel1.Text = "Cavalo não encontrado";
                                                Program.consultaForm.toolStripStatusLabel1.ForeColor = Color.Red;
                                            }
                                            else
                                            {
                                                //Encontrado
                                                Program.consultaForm.toolStripStatusLabel1.Text = "Cavalo encontrado";
                                                Program.consultaForm.toolStripStatusLabel1.ForeColor = Color.Green;

                                                //Adicionar informações
                                                Program.consultaForm.label2.Text = "Nome: " + chat[2];
                                                Program.consultaForm.label3.Text = "Peso: " + chat[3];
                                                Program.consultaForm.label4.Text = "Total de vitórias: " + chat[4];
                                                chat[5] = chat[5].ToLower();
                                                if (chat[5] == "honey")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Honey;
                                                }else if (chat[5] == "coronel")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Coronel;
                                                }else if (chat[5] == "soberano")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Soberano;
                                                }else if (chat[5] == "sucesso")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Sucesso;
                                                }else if (chat[5] == "pirata")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Pirata;
                                                }else if (chat[5] == "passo preto")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.passopreto;
                                                }else if (chat[5] == "craque")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Craque1;
                                                }else if (chat[5] == "brinquedo")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Brinquedo;
                                                }else if (chat[5] == "profeta")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Profeta4;
                                                }else if (chat[5] == "triunfo")
                                                {
                                                    Program.consultaForm.pictureBox2.Image = Properties.Resources.Triunfo;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Comandos  - ADM -
                                            if (chat[0] == "kick")
                                            {
                                                MessageBox.Show("Você foi expulso pelo [ADM]");
                                                this.Close();
                                            }
                                            else
                                            {
                                                if (chat[0] == "banido")
                                                {
                                                    MessageBox.Show("Você foi banido pelo [ADM]");
                                                    this.Close();
                                                }
                                                else
                                                {
                                                    if (chat[0] == "adm")
                                                    {
                                                        MessageBox.Show(chat[1]);
                                                    }
                                                }
                                            }

                                            if (chat[0] == "admsup")
                                            {
                                                string[] testar = chat[1].Split(',');

                                                for (int a = 0; a < testar.Length-1; a++)
                                                {
                                                    Program.admForm.listBox1.Items.Add(testar[a]);
                                                }
                                            }
                                            else
                                            {
                                                if (chat[0] == "admdadossup")
                                                {
                                                    //[ID:0000] Enviar para NULL Data: 00/00/0000 00:00:00 Assunto: Nenhum
                                                    Program.admForm.label5.Text = "[ID: "+ Program.admForm.listBox1.Text +"] Enviar para "+chat[1];
                                                    Program.admForm.label9.Text = "Assunto: "+chat[3];
                                                    Program.admForm.label10.Text = "Data de envio: " + chat[2].Replace(",", "/");
                                                    Program.admForm.textBox6.Text = "";
                                                    Program.admForm.textBox6.AppendText(chat[4].Replace("X", System.Environment.NewLine));
                                                    Program.admForm.textBox6.ScrollToCaret();
                                                }
                                            }

                                            //Acontecimentos
                                            if (chat[0] == "noticia")
                                            {
                                                textBox3.AppendText(chat[1]+System.Environment.NewLine);
                                                textBox3.ScrollToCaret();
                                            }

                                            //Código
                                            if (chat[0] == "codigo")
                                            {
                                                if (chat[1] == "3")
                                                {
                                                    Program.codigoForm.toolStripStatusLabel1.Text = "Código não existe";
                                                    Program.codigoForm.toolStripStatusLabel1.ForeColor = Color.Red;
                                                }
                                                else
                                                {
                                                    if (chat[1] == "1")
                                                    {
                                                        Program.codigoForm.toolStripStatusLabel1.Text = "Código já foi utilizado";
                                                        Program.codigoForm.toolStripStatusLabel1.ForeColor = Color.Red;
                                                    }
                                                    else
                                                    {
                                                        Program.codigoForm.toolStripStatusLabel1.Text = "Código aplicado";
                                                        Program.codigoForm.toolStripStatusLabel1.ForeColor = Color.Green;
                                                    }
                                                }
                                            }
                                            
                                            //Mensagem
                                            if (chat[0] == "mensagem")
                                            {
                                                Program.msgForm.AddMensagem(chat[1]);
                                            }

                                            if (chat[0] == "conmensagem")
                                            {
                                                Program.msgForm.textBox1.Text = "";
                                                Program.msgForm.textBox1.AppendText(chat[1].Replace("X", System.Environment.NewLine));
                                                Program.msgForm.label1.Text = "Length: " + Program.msgForm.textBox1.Text.Length;
                                            }

                                            //Boleto
                                            if (chat[0] == "boleto")
                                            {
                                                Program.lojaForm.label5.Text = chat[1].Replace(",", "/");
                                                Program.lojaForm.label6.Text = chat[2];
                                            }

                                            //Rank
                                            if (chat[0] == "rank")
                                            {
                                                //First
                                                Program.rankForm.label1.Text = chat[1].Split(',')[0];
                                                Program.rankForm.label5.Text = "R$" + chat[1].Split(',')[1];

                                                //Second
                                                Program.rankForm.label2.Text = chat[2].Split(',')[0];
                                                Program.rankForm.label6.Text = "R$" + chat[2].Split(',')[1];

                                                //Three
                                                Program.rankForm.label3.Text = chat[3].Split(',')[0];
                                                Program.rankForm.label7.Text = "R$" + chat[3].Split(',')[1];
                                            }

                                            //Trade
                                            if (chat[0] == "trade")
                                            {
                                                //Perguntar se aceita a trade
                                                if (chat[1] == "perguntar")
                                                {
                                                    Program.msgshowForm = new Form13();
                                                    Program.msgshowForm.Visible = true;
                                                    Program.msgshowForm.Show();
                                                    Program.msgshowForm.label1.Text = "Aceita o trade de " + chat[3] + " no valor R$" + chat[2] + " ?";
                                                    Program.msgshowForm.label2.Text = chat[3];
                                                }

                                                //Informar para quem iniciou a negociação
                                                if (chat[1] == "saiu")
                                                {
                                                    if (Application.OpenForms.OfType<Form14>().Count() > 0)
                                                    {
                                                        Program.tradeForm.label9.Visible = true;
                                                        Program.tradeForm.label9.Text = "Jogador saiu";
                                                        Program.tradeForm.label9.ForeColor = Color.Red;
                                                        Program.tradeForm.timer2.Stop();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Negociação com o " + chat[2] + " não foi possível por que ele saiu do jogo.");
                                                    }
                                                }
                                            }

                                            if (chat[0] == "traderesposta")
                                            {
                                                //Recusou
                                                if (chat[1] == "recusou")
                                                {
                                                    if (Application.OpenForms.OfType<Form14>().Count() > 0)
                                                    {
                                                        Program.tradeForm.label9.Visible = true;
                                                        Program.tradeForm.label9.Text = "Jogador recusou";
                                                        Program.tradeForm.label9.ForeColor = Color.Red;
                                                        Program.tradeForm.timer2.Stop();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Jogador recusou sua negociação.");
                                                    }
                                                }

                                                //Aceitou
                                                if (chat[1] == "aceitou")
                                                {
                                                    if (Application.OpenForms.OfType<Form14>().Count() > 0)
                                                    {
                                                        Program.tradeForm.label9.Visible = true;
                                                        Program.tradeForm.label9.Text = "Jogador aceitou";
                                                        Program.tradeForm.label9.ForeColor = Color.Green;
                                                        Program.tradeForm.timer2.Stop();
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Jogador aceitou sua negociação.");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        //Corridas - DATETIME -
        //Deluxe, Legend, United, Rushback, Never, Vortex, 
        //Hunted, Blood, River, Flawless, Destiny, 
        //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
        DateTime black = Convert.ToDateTime("25/01/2019");
        DateTime fossa = Convert.ToDateTime("26/01/2019");
        DateTime play = Convert.ToDateTime("27/01/2019");
        DateTime deluxe = Convert.ToDateTime("25/01/2019");
        DateTime legend = Convert.ToDateTime("26/01/2019");
        DateTime united = Convert.ToDateTime("27/01/2019");
        DateTime rushback = Convert.ToDateTime("25/01/2019");
        DateTime never = Convert.ToDateTime("26/01/2019");
        DateTime vortex = Convert.ToDateTime("27/01/2019");
        DateTime hunted = Convert.ToDateTime("25/01/2019");
        DateTime blood = Convert.ToDateTime("26/01/2019");
        DateTime river = Convert.ToDateTime("27/01/2019");
        DateTime flawless = Convert.ToDateTime("25/01/2019");
        DateTime destiny = Convert.ToDateTime("26/01/2019");
        DateTime fun = Convert.ToDateTime("27/01/2019");
        DateTime chaos = Convert.ToDateTime("25/01/2019");
        DateTime die = Convert.ToDateTime("26/01/2019");
        DateTime sacrifice = Convert.ToDateTime("27/01/2019");
        DateTime heaven = Convert.ToDateTime("25/01/2019");
        DateTime law = Convert.ToDateTime("26/01/2019");
        DateTime dragons = Convert.ToDateTime("27/01/2019");
        DateTime soul = Convert.ToDateTime("27/01/2019");

        private void Form3_Load(object sender, EventArgs e)
        {
            //Combobox
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            //Label & TextBox
            textBox2.MaxLength = 9;
            label4.Text = "[" + black.ToLongTimeString() + "]";
            label5.Text = "[" + fossa.ToLongTimeString() + "]";
            label6.Text = "[" + play.ToLongTimeString() + "]";
            //ToolTip
            toolTip1.SetToolTip(comboBox2, "Cavalos que irão correr");
            toolTip1.SetToolTip(lbmoney, "Seu dinheiro");
            toolTip1.SetToolTip(pictureBox3, "Seu emblema conquistado");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Deluxe, Legend, United, Rushback, Never, Vortex, 
            //Hunted, Blood, River, Flawless, Destiny, 
            //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
            black = black.AddSeconds(-1);
            fossa = fossa.AddSeconds(-1);
            play = play.AddSeconds(-1);
            deluxe = deluxe.AddSeconds(-1);
            legend = legend.AddSeconds(-1);
            united = united.AddSeconds(-1);
            rushback = rushback.AddSeconds(-1);
            never = never.AddSeconds(-1);
            vortex = vortex.AddSeconds(-1);
            hunted = hunted.AddSeconds(-1);
            blood = blood.AddSeconds(-1);
            river = river.AddSeconds(-1);
            flawless = flawless.AddSeconds(-1);
            destiny = destiny.AddSeconds(-1);
            fun = fun.AddSeconds(-1);
            chaos = chaos.AddSeconds(-1);
            die = die.AddSeconds(-1);
            sacrifice = sacrifice.AddSeconds(-1);
            heaven = heaven.AddSeconds(-1);
            law = law.AddSeconds(-1);
            dragons = dragons.AddSeconds(-1);
            soul = soul.AddSeconds(-1);

            if (black.ToLongTimeString() == "00:00:00" || fossa.ToLongTimeString() == "00:00:00" || play.ToLongTimeString() == "00:00:00" || 
                deluxe.ToLongTimeString() == "00:00:00" || legend.ToLongTimeString() == "00:00:00" || united.ToLongTimeString() == "00:00:00" ||
                rushback.ToLongTimeString() == "00:00:00" || never.ToLongTimeString() == "00:00:00" || vortex.ToLongTimeString() == "00:00:00" ||
                hunted.ToLongTimeString() == "00:00:00" || blood.ToLongTimeString() == "00:00:00" || river.ToLongTimeString() == "00:00:00" ||
                flawless.ToLongTimeString() == "00:00:00" || destiny.ToLongTimeString() == "00:00:00" || fun.ToLongTimeString() == "00:00:00" ||
                chaos.ToLongTimeString() == "00:00:00" || die.ToLongTimeString() == "00:00:00" || sacrifice.ToLongTimeString() == "00:00:00" ||
                heaven.ToLongTimeString() == "00:00:00" || law.ToLongTimeString() == "00:00:00" || dragons.ToLongTimeString() == "00:00:00" ||
                soul.ToLongTimeString() == "00:00:00")
            {
                Program.loginForm.Send("corridas/" + lbname.Text);
            }
            else
            {
                label4.Text = "[" + black.ToLongTimeString() + "]";
                label5.Text = "[" + fossa.ToLongTimeString() + "]";
                label6.Text = "[" + play.ToLongTimeString() + "]";
                //Deluxe, Legend, United, Rushback, Never, Vortex, 
                //Hunted, Blood, River, Flawless, Destiny, 
                //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
                label7.Text = "[" + deluxe.ToLongTimeString() + "]";
                label8.Text = "[" + legend.ToLongTimeString() + "]";
                label9.Text = "[" + united.ToLongTimeString() + "]";
                label10.Text = "[" + rushback.ToLongTimeString() + "]";
                label11.Text = "[" + never.ToLongTimeString() + "]";
                label12.Text = "[" + vortex.ToLongTimeString() + "]";
                label13.Text = "[" + hunted.ToLongTimeString() + "]";
                label14.Text = "[" + blood.ToLongTimeString() + "]";
                label15.Text = "[" + river.ToLongTimeString() + "]";
                label16.Text = "[" + flawless.ToLongTimeString() + "]";
                label17.Text = "[" + destiny.ToLongTimeString() + "]";
                label18.Text = "[" + fun.ToLongTimeString() + "]";
                label19.Text = "[" + chaos.ToLongTimeString() + "]";
                label20.Text = "[" + die.ToLongTimeString() + "]";
                label21.Text = "[" + sacrifice.ToLongTimeString() + "]";
                label22.Text = "[" + heaven.ToLongTimeString() + "]";
                label23.Text = "[" + law.ToLongTimeString() + "]";
                label24.Text = "[" + dragons.ToLongTimeString() + "]";
                label25.Text = "[" + soul.ToLongTimeString() + "]";
            }

            //Fechar as apostas - 15 segundos -
            if (Convert.ToInt32(black.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(black.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(black.ToLongTimeString().Split(':')[2]) < 16)
            {
                label4.ForeColor = Color.Red;
            }
            else
            {
                label4.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(fossa.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(fossa.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(fossa.ToLongTimeString().Split(':')[2]) < 16)
            {
                label5.ForeColor = Color.Red;
            }
            else
            {
                label5.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(play.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(play.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(play.ToLongTimeString().Split(':')[2]) < 16)
            {
                label6.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Green;
            }

            //Novos
            //Deluxe, Legend, United, Rushback, Never, Vortex, 
            if (Convert.ToInt32(deluxe.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(deluxe.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(deluxe.ToLongTimeString().Split(':')[2]) < 16)
            {
                label7.ForeColor = Color.Red;
            }
            else
            {
                label7.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(legend.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(legend.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(legend.ToLongTimeString().Split(':')[2]) < 16)
            {
                label8.ForeColor = Color.Red;
            }
            else
            {
                label8.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(united.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(united.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(united.ToLongTimeString().Split(':')[2]) < 16)
            {
                label9.ForeColor = Color.Red;
            }
            else
            {
                label9.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(rushback.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(rushback.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(rushback.ToLongTimeString().Split(':')[2]) < 16)
            {
                label10.ForeColor = Color.Red;
            }
            else
            {
                label10.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(never.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(never.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(never.ToLongTimeString().Split(':')[2]) < 16)
            {
                label11.ForeColor = Color.Red;
            }
            else
            {
                label11.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(vortex.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(vortex.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(vortex.ToLongTimeString().Split(':')[2]) < 16)
            {
                label12.ForeColor = Color.Red;
            }
            else
            {
                label12.ForeColor = Color.Green;
            }

            //Hunted, Blood, River, Flawless, Destiny, 

            if (Convert.ToInt32(hunted.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(hunted.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(hunted.ToLongTimeString().Split(':')[2]) < 16)
            {
                label13.ForeColor = Color.Red;
            }
            else
            {
                label13.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(blood.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(blood.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(blood.ToLongTimeString().Split(':')[2]) < 16)
            {
                label14.ForeColor = Color.Red;
            }
            else
            {
                label14.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(river.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(river.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(river.ToLongTimeString().Split(':')[2]) < 16)
            {
                label15.ForeColor = Color.Red;
            }
            else
            {
                label15.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(flawless.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(flawless.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(flawless.ToLongTimeString().Split(':')[2]) < 16)
            {
                label16.ForeColor = Color.Red;
            }
            else
            {
                label16.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(destiny.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(destiny.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(destiny.ToLongTimeString().Split(':')[2]) < 16)
            {
                label17.ForeColor = Color.Red;
            }
            else
            {
                label17.ForeColor = Color.Green;
            }

            //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
            if (Convert.ToInt32(fun.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(fun.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(fun.ToLongTimeString().Split(':')[2]) < 16)
            {
                label18.ForeColor = Color.Red;
            }
            else
            {
                label18.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(chaos.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(chaos.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(chaos.ToLongTimeString().Split(':')[2]) < 16)
            {
                label19.ForeColor = Color.Red;
            }
            else
            {
                label19.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(die.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(die.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(die.ToLongTimeString().Split(':')[2]) < 16)
            {
                label20.ForeColor = Color.Red;
            }
            else
            {
                label20.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(sacrifice.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(sacrifice.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(sacrifice.ToLongTimeString().Split(':')[2]) < 16)
            {
                label21.ForeColor = Color.Red;
            }
            else
            {
                label21.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(heaven.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(heaven.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(heaven.ToLongTimeString().Split(':')[2]) < 16)
            {
                label22.ForeColor = Color.Red;
            }
            else
            {
                label22.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(law.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(law.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(law.ToLongTimeString().Split(':')[2]) < 16)
            {
                label23.ForeColor = Color.Red;
            }
            else
            {
                label23.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(dragons.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(dragons.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(dragons.ToLongTimeString().Split(':')[2]) < 16)
            {
                label24.ForeColor = Color.Red;
            }
            else
            {
                label24.ForeColor = Color.Green;
            }

            if (Convert.ToInt32(soul.ToLongTimeString().Split(':')[0]) == 0 && Convert.ToInt32(soul.ToLongTimeString().Split(':')[1]) == 0 && Convert.ToInt32(soul.ToLongTimeString().Split(':')[2]) < 16)
            {
                label25.ForeColor = Color.Red;
            }
            else
            {
                label25.ForeColor = Color.Green;
            }

        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form4>().Count() > 0)
            {
                Program.consultaForm.Visible = false;
                Program.consultaForm.Visible = true;
            }
            else
            {
                //Novo
                Program.consultaForm = new Form4();
                Program.consultaForm.Visible = false;
                Program.consultaForm.Visible = true;
            }
        }

        private void adicionarFundosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form5>().Count() > 0)
            {
                Program.lojaForm.Visible = false;
                Program.lojaForm.Visible = true;
            }
            else
            {
                //Novo
                Program.lojaForm = new Form5();
                Program.lojaForm.Visible = false;
                Program.lojaForm.Visible = true;
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (label73.Text == "0")
            {
                Program.loginForm.FecharConexao();
            }
            else
            {
                Program.loginForm.FecharConexao(1);
            }
            //Program.loginForm.FecharConexao();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.FindString(listBox1.Text);
        
            if(index == 0)
            {//Black Edition
                if(label4.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }else if(index == 1)
            {//Fossa Party
                if (label5.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 2)
            {//PlayBoy Party
                if (label6.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 3)
            {//Deluxe
                if (label7.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 4)
            {
                if (label8.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 5)
            {
                if (label9.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 6)
            {
                if (label10.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 7)
            {
                if (label11.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 8)
            {
                if (label12.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 9)
            {
                if (label13.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 10)
            {
                if (label14.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 11)
            {
                if (label15.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 12)
            {
                if (label16.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 13)
            {
                if (label17.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 14)
            {
                if (label18.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 15)
            {
                if (label19.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 16)
            {
                if (label20.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 17)
            {
                if (label21.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 18)
            {
                if (label22.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 19)
            {
                if (label23.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 20)
            {
                if (label24.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
            else if (index == 21)
            {
                if (label25.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerradas";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    toolStripStatusLabel1.Text = "Painel de apostas";
                    toolStripStatusLabel1.ForeColor = Color.Black;
                    textBox1.Text = listBox1.Text;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                toolStripStatusLabel1.Text = "Você precisa escrever algo";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                if (textBox4.Text.Contains("/") == true)
                {
                    toolStripStatusLabel1.Text = "Não é permitido '/' na sua mensagem";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        Program.loginForm.Send("chat/" + textBox4.Text);
                        richTextBox1.ScrollToCaret();
                        textBox4.Text = "";
                        toolStripStatusLabel1.Text = "Painel de apostas";
                        toolStripStatusLabel1.ForeColor = Color.Black;
                    }
                    else
                    {
                        if (listBox3.Text == lbname.Text)
                        {
                            toolStripStatusLabel1.Text = "Você não pode enviar um PM para você mesmo";
                            toolStripStatusLabel1.ForeColor = Color.Black;
                            textBox4.Text = "";
                        }
                        else
                        {
                            richTextBox1.AppendText("[PM]Você(Para:" + listBox3.Text + ")" + ": " + textBox4.Text + "\n");
                            Program.loginForm.Send("privado" + "/" + lbname.Text + "/" + listBox3.Text + "/" + textBox4.Text);
                            richTextBox1.ScrollToCaret();
                            textBox4.Text = "";
                        }
                    }
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button4.PerformClick();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                //Privado
                toolStripStatusLabel1.Text = "Selecione o usuário online e escreva a mensagem";
                toolStripStatusLabel1.ForeColor = Color.Black;
            }
            else
            {
                //Global
                toolStripStatusLabel1.Text = "Painel de apostas";
                toolStripStatusLabel1.ForeColor = Color.Black;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }

        private void códigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form6>().Count() > 0)
            {
                Program.codigoForm.Visible = false;
                Program.codigoForm.Visible = true;
            }
            else
            {
                //Novo
                Program.codigoForm = new Form6();
                Program.codigoForm.Visible = false;
                Program.codigoForm.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] blind = label27.Text.Split('|');
            string[] blind2 = label28.Text.Split('|');
            string[] blind3 = label29.Text.Split('|');
            string[] blind4 = label30.Text.Split('|');
            string[] blind5 = label31.Text.Split('|');
            string[] blind6 = label32.Text.Split('|');
            string[] blind7= label33.Text.Split('|');
            string[] blind8 = label34.Text.Split('|');
            string[] blind9 = label35.Text.Split('|');
            string[] blind10 = label36.Text.Split('|');
            string[] blind11 = label37.Text.Split('|');
            string[] blind12 = label38.Text.Split('|');
            string[] blind13 = label39.Text.Split('|');
            string[] blind14 = label40.Text.Split('|');
            string[] blind15 = label41.Text.Split('|');
            string[] blind16 = label42.Text.Split('|');
            string[] blind17 = label43.Text.Split('|');
            string[] blind18 = label44.Text.Split('|');
            string[] blind19 = label45.Text.Split('|');
            string[] blind20 = label46.Text.Split('|');
            string[] blind21 = label47.Text.Split('|');
            string[] blind22 = label48.Text.Split('|');


            if (textBox1.Text == "" || textBox2.Text == "")
            {
                toolStripStatusLabel1.Text = "Os campos não foram preenchidos corretamente";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                if (Convert.ToInt32(lbmoney.Text) < Convert.ToInt32(textBox2.Text))
                {
                    toolStripStatusLabel1.Text = "Você não tem dinheiro suficiente";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    //Black Edition
                    if (textBox1.Text == "Black Edition")
                    {
                        int min = Convert.ToInt32(blind[0]);
                        int max = Convert.ToInt32(blind[1]);

                        if (label4.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Fossa Party
                    if (textBox1.Text == "Fossa Party")
                    {
                        int min = Convert.ToInt32(blind2[0]);
                        int max = Convert.ToInt32(blind2[1]);

                        if (label5.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //PlayBoy Only
                    if (textBox1.Text == "PlayBoy Only")
                    {
                        int min = Convert.ToInt32(blind3[0]);
                        int max = Convert.ToInt32(blind3[1]);

                        if (label6.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                                {
                                    toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                    toolStripStatusLabel1.ForeColor = Color.Red;
                                }
                                else
                                {
                                    //Enviar aposta
                                    Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                                }
                            }
                        }
                    }
                    
                    //Deluxe
                    if (textBox1.Text == "Deluxe")
                    {
                        int min = Convert.ToInt32(blind4[0]);
                        int max = Convert.ToInt32(blind4[1]);

                        if (label7.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Legend
                    if (textBox1.Text == "Legend")
                    {
                        int min = Convert.ToInt32(blind5[0]);
                        int max = Convert.ToInt32(blind5[1]);

                        if (label8.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //United
                    if (textBox1.Text == "United")
                    {
                        int min = Convert.ToInt32(blind6[0]);
                        int max = Convert.ToInt32(blind6[1]);

                        if (label9.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Rushback
                    if (textBox1.Text == "Rushback")
                    {
                        int min = Convert.ToInt32(blind7[0]);
                        int max = Convert.ToInt32(blind7[1]);

                        if (label10.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Never
                    if (textBox1.Text == "Never")
                    {
                        int min = Convert.ToInt32(blind8[0]);
                        int max = Convert.ToInt32(blind8[1]);

                        if (label11.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Vortex
                    if (textBox1.Text == "Vortex")
                    {
                        int min = Convert.ToInt32(blind9[0]);
                        int max = Convert.ToInt32(blind9[1]);

                        if (label12.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Hunted
                    if (textBox1.Text == "Hunted")
                    {
                        int min = Convert.ToInt32(blind10[0]);
                        int max = Convert.ToInt32(blind10[1]);

                        if (label13.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Blood
                    if (textBox1.Text == "Blood")
                    {
                        int min = Convert.ToInt32(blind11[0]);
                        int max = Convert.ToInt32(blind11[1]);

                        if (label14.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //River
                    if (textBox1.Text == "River")
                    {
                        int min = Convert.ToInt32(blind12[0]);
                        int max = Convert.ToInt32(blind12[1]);

                        if (label15.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Flawless
                    if (textBox1.Text == "Flawless")
                    {
                        int min = Convert.ToInt32(blind13[0]);
                        int max = Convert.ToInt32(blind13[1]);

                        if (label16.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Destiny
                    if (textBox1.Text == "Destiny")
                    {
                        int min = Convert.ToInt32(blind14[0]);
                        int max = Convert.ToInt32(blind14[1]);

                        if (label17.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Fun
                    if (textBox1.Text == "Fun")
                    {
                        int min = Convert.ToInt32(blind15[0]);
                        int max = Convert.ToInt32(blind15[1]);

                        if (label18.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Chaos
                    if (textBox1.Text == "Chaos")
                    {
                        int min = Convert.ToInt32(blind16[0]);
                        int max = Convert.ToInt32(blind16[1]);

                        if (label19.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Die
                    if (textBox1.Text == "Die")
                    {
                        int min = Convert.ToInt32(blind17[0]);
                        int max = Convert.ToInt32(blind17[1]);

                        if (label20.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Sacrifice
                    if (textBox1.Text == "Sacrifice")
                    {
                        int min = Convert.ToInt32(blind18[0]);
                        int max = Convert.ToInt32(blind18[1]);

                        if (label21.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Heaven
                    if (textBox1.Text == "Heaven")
                    {
                        int min = Convert.ToInt32(blind19[0]);
                        int max = Convert.ToInt32(blind19[1]);

                        if (label22.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Law
                    if (textBox1.Text == "Law")
                    {
                        int min = Convert.ToInt32(blind20[0]);
                        int max = Convert.ToInt32(blind20[1]);

                        if (label23.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Dragons
                    if (textBox1.Text == "Dragons")
                    {
                        int min = Convert.ToInt32(blind21[0]);
                        int max = Convert.ToInt32(blind21[1]);

                        if (label24.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                    
                    //Soul
                    if (textBox1.Text == "Soul")
                    {
                        int min = Convert.ToInt32(blind22[0]);
                        int max = Convert.ToInt32(blind22[1]);

                        if (label25.ForeColor == Color.Red)
                        {
                            toolStripStatusLabel1.Text = "Apostas encerrada";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                            {
                                toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);

                            }
                        }
                    }
                }
            }

            /*if (textBox1.Text == "Black Edition")
            {
                int min = Convert.ToInt32(blind[0]);
                int max = Convert.ToInt32(blind[1]);

                if (label4.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerrada";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        toolStripStatusLabel1.Text = "Os campos não foram preenchidos corretamente";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                        {
                            toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(lbmoney.Text) < Convert.ToInt32(textBox2.Text))
                            {
                                toolStripStatusLabel1.Text = "Você não tem dinheiro suficiente";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);
                            }
                        }
                    }
                }
            }else if (textBox1.Text == "Fossa Party")
            {
                int min = Convert.ToInt32(blind2[0]);
                int max = Convert.ToInt32(blind2[1]);

                if (label5.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerrada";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        toolStripStatusLabel1.Text = "Os campos não foram preenchidos corretamente";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                        {
                            toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(lbmoney.Text) < Convert.ToInt32(textBox2.Text))
                            {
                                toolStripStatusLabel1.Text = "Você não tem dinheiro suficiente";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);
                            }
                        }
                    }
                }
            }else if (textBox1.Text == "PlayBoy Only")
            {
                int min = Convert.ToInt32(blind3[0]);
                int max = Convert.ToInt32(blind3[1]);

                if (label6.ForeColor == Color.Red)
                {
                    toolStripStatusLabel1.Text = "Apostas encerrada";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
                else
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        toolStripStatusLabel1.Text = "Os campos não foram preenchidos corretamente";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (Convert.ToInt32(textBox2.Text) > max || Convert.ToInt32(textBox2.Text) < min)
                        {
                            toolStripStatusLabel1.Text = "Valor não corresponde ao blind da corrida";
                            toolStripStatusLabel1.ForeColor = Color.Red;
                        }
                        else
                        {
                            if (Convert.ToInt32(lbmoney.Text) < Convert.ToInt32(textBox2.Text))
                            {
                                toolStripStatusLabel1.Text = "Você não tem dinheiro suficiente";
                                toolStripStatusLabel1.ForeColor = Color.Red;
                            }
                            else
                            {
                                //Enviar aposta
                                Program.loginForm.Send("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);
                                MessageBox.Show("aposta/" + lbname.Text + "/" + comboBox2.Text + "/" + textBox2.Text + "/" + textBox1.Text);
                            }
                        }
                    }
                }
            }*/
        }

        private void administradorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form8>().Count() > 0)
            {
                Program.admForm. Visible = false;
                Program.admForm.Visible = true;
            }
            else
            {
                //Novo
                Program.loginForm.Send("admsup/"+Program.painelForm.lbname.Text);
                Program.admForm = new Form8();
                Program.admForm.Visible = false;
                Program.admForm.Visible = true;
            }
        }

        private void mensagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form10>().Count() > 0)
            {
                Program.msgForm.Visible = false;
                Program.msgForm.Visible = true;
            }
            else
            {
                //Novo
                Program.loginForm.Send("attmsg/"+lbname.Text);
                Program.msgForm = new Form10();
                Program.msgForm.Visible = false;
                Program.msgForm.Visible = true;
            }
        }

        private void regrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form11>().Count() > 0)
            {
                Program.regrasForm.Visible = false;
                Program.regrasForm.Visible = true;
            }
            else
            {
                //Novo
                Program.regrasForm = new Form11();
                Program.regrasForm.Visible = false;
                Program.regrasForm.Visible = true;
            }
        }

        private void suporteToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label73.Text = "1";
            this.Close();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || Char.IsSymbol(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || Char.IsNumber(e.KeyChar) || Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //Validando se o usuario aperto no teclado Ctrl + V
            if (e.Control && e.KeyValue == 86)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void rankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form12>().Count() > 0)
            {
                Program.rankForm.Visible = false;
                Program.rankForm.Visible = true;
            }
            else
            {
                //Novo
                Program.loginForm.Send("rank/"+Program.painelForm.lbname.Text);
                Program.rankForm = new Form12();
                Program.rankForm.Visible = false;
                Program.rankForm.Visible = true;
            }
        }

        /*private void MSG(string mensagem)
        {
            if (Application.OpenForms.OfType<Form13>().Count() > 0)
            {
                Program.msgshowForm.Close();
                //Novo
                Program.msgshowForm = new Form13();
                Program.msgshowForm.Visible = false;
                Program.msgshowForm.Visible = true;
            }
            else
            {
                //Novo
                Program.msgshowForm = new Form13();
                Program.msgshowForm.Visible = false;
                Program.msgshowForm.Visible = true;
            }
        }*/

        private void listBox3_MouseUp(object sender, MouseEventArgs e)
        {
            int location = listBox3.IndexFromPoint(e.Location);
            listBox3.SelectedIndex = location;

            if (listBox3.Text != "")
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(Cursor.Position);    //Show Menu
                }
            }
        }

        private void negociarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MSG("Deseja negociar com "+listBox3.Text+"?");
            if (listBox3.Text == lbname.Text)
            {
                toolStripStatusLabel1.Text = "Não pode negociar com você mesmo";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                if (Application.OpenForms.OfType<Form14>().Count() > 0)
                {
                    //Program.tradeForm.Visible = false;
                    //Program.tradeForm.Visible = true;
                    Program.tradeForm.Close();
                    //Novo
                    Program.tradeForm = new Form14();
                    Program.tradeForm.Visible = false;
                    Program.tradeForm.Visible = true;
                    Program.tradeForm.label1.Text = Program.painelForm.lbname.Text;
                    Program.tradeForm.label6.Text = Program.painelForm.lbmoney.Text;
                    Program.tradeForm.label2.Text = listBox3.Text;
                }
                else
                {
                    //Novo
                    Program.tradeForm = new Form14();
                    Program.tradeForm.Visible = false;
                    Program.tradeForm.Visible = true;
                    Program.tradeForm.label1.Text = Program.painelForm.lbname.Text;
                    Program.tradeForm.label6.Text = Program.painelForm.lbmoney.Text;
                    Program.tradeForm.label2.Text = listBox3.Text;
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            //Validando se o usuario aperto no teclado Ctrl + V
            if (e.Control && e.KeyValue == 86)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void configuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form15>().Count() > 0)
            {
                Program.configForm.Visible = false;
                Program.configForm.Visible = true;
            }
            else
            {
                Program.configForm = new Form15();
                Program.configForm.Visible = false;
                Program.configForm.Visible = true;
            }
        }

        private void pokerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form16>().Count() > 0)
            {
                Program.listaForm.Visible = false;
                Program.listaForm.Visible = true;
            }
            else
            {
                Program.listaForm = new Form16();
                Program.listaForm.Visible = false;
                Program.listaForm.Visible = true;
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
