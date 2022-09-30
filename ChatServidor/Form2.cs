using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatServidor
{
    public partial class Form2 : Form
    {
        //inicia a variável com o valor de Hora zerado
        DateTime hora = Convert.ToDateTime("25/01/2019");
        DateTime hora2 = Convert.ToDateTime("25/01/2019");
        DateTime hora3 = Convert.ToDateTime("25/01/2019");
        DateTime hora4 = Convert.ToDateTime("25/01/2019");
        DateTime hora5 = Convert.ToDateTime("25/01/2019");
        DateTime hora6 = Convert.ToDateTime("25/01/2019");
        DateTime hora7 = Convert.ToDateTime("25/01/2019");
        DateTime hora8 = Convert.ToDateTime("25/01/2019");
        DateTime hora9 = Convert.ToDateTime("25/01/2019");
        DateTime hora10 = Convert.ToDateTime("25/01/2019");
        DateTime hora11 = Convert.ToDateTime("25/01/2019");
        DateTime hora12 = Convert.ToDateTime("25/01/2019");
        DateTime hora13 = Convert.ToDateTime("25/01/2019");
        DateTime hora14 = Convert.ToDateTime("25/01/2019");
        DateTime hora15 = Convert.ToDateTime("25/01/2019");
        DateTime hora16 = Convert.ToDateTime("25/01/2019");
        DateTime hora17 = Convert.ToDateTime("25/01/2019");
        DateTime hora18 = Convert.ToDateTime("25/01/2019");
        DateTime hora19 = Convert.ToDateTime("25/01/2019");
        DateTime hora20 = Convert.ToDateTime("25/01/2019");
        DateTime hora21 = Convert.ToDateTime("25/01/2019");
        DateTime hora22 = Convert.ToDateTime("25/01/2019");
        

        private void pegarCorridas(int tipo)
        {
            if(tipo == 99)
            {
                //Black
                hora = hora.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Black Edition" + ".ini")));
                label1.Text = hora.ToLongTimeString();
                //Fossa Party
                hora2 = hora2.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Fossa Party" + ".ini")));
                label2.Text = hora2.ToLongTimeString();
                //PlayBoy Only
                hora3 = hora3.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "PlayBoy Only" + ".ini")));
                label3.Text = hora3.ToLongTimeString();
                //Deluxe
                hora4 = hora4.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Deluxe" + ".ini")));
                label4.Text = hora4.ToLongTimeString();
                //Legend
                hora5 = hora5.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Legend" + ".ini")));
                label5.Text = hora5.ToLongTimeString();
                //United
                hora6 = hora6.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "United" + ".ini")));
                label6.Text = hora6.ToLongTimeString();
                //Rushback
                hora7 = hora7.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Rushback" + ".ini")));
                label7.Text = hora7.ToLongTimeString();
                //Never
                hora8 = hora8.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Never" + ".ini")));
                label8.Text = hora8.ToLongTimeString();
                //Vortex
                hora9 = hora9.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Vortex" + ".ini")));
                label9.Text = hora9.ToLongTimeString();
                //Hunted
                hora10 = hora10.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Hunted" + ".ini")));
                label10.Text = hora10.ToLongTimeString();
                //Blood
                hora11 = hora11.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Blood" + ".ini")));
                label11.Text = hora11.ToLongTimeString();
                //River
                hora12 = hora12.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "River" + ".ini")));
                label12.Text = hora12.ToLongTimeString();
                //Flawless
                hora13 = hora13.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Flawless" + ".ini")));
                label13.Text = hora13.ToLongTimeString();
                //Destiny
                hora14 = hora14.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Destiny" + ".ini")));
                label14.Text = hora14.ToLongTimeString();
                //Fun
                hora15 = hora15.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Fun" + ".ini")));
                label15.Text = hora15.ToLongTimeString();
                //Chaos
                hora16 = hora16.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Chaos" + ".ini")));
                label16.Text = hora16.ToLongTimeString();
                //Die
                hora17 = hora17.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Die" + ".ini")));
                label17.Text = hora17.ToLongTimeString();
                //Sacrifice
                hora18 = hora18.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Sacrifice" + ".ini")));
                label18.Text = hora18.ToLongTimeString();
                //Heaven
                hora19 = hora19.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Heaven" + ".ini")));
                label19.Text = hora19.ToLongTimeString();
                //Law
                hora20 = hora20.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Law" + ".ini")));
                label20.Text = hora20.ToLongTimeString();
                //Dragons
                hora21 = hora21.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Dragons" + ".ini")));
                label21.Text = hora21.ToLongTimeString();
                //Soul
                hora22 = hora22.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Soul" + ".ini")));
                label22.Text = hora22.ToLongTimeString();
            }
            else
            {

                if (tipo == 1)
                {
                    //Black
                    hora = hora.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Black Edition" + ".ini")));
                    label1.Text = hora.ToLongTimeString();
                }
                else if (tipo == 2)
                {
                    //Fossa Party
                    hora2 = hora2.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Fossa Party" + ".ini")));
                    label2.Text = hora2.ToLongTimeString();
                }
                else if (tipo == 3)
                {
                    //PlayBoy Only
                    hora3 = hora3.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "PlayBoy Only" + ".ini")));
                    label3.Text = hora3.ToLongTimeString();
                }
                else if (tipo == 4)
                {
                    //Delux
                    hora4 = hora4.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Deluxe" + ".ini")));
                    label4.Text = hora4.ToLongTimeString();
                }
                else if (tipo == 5)
                {
                    //Legend
                    hora5 = hora5.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Legend" + ".ini")));
                    label5.Text = hora5.ToLongTimeString();
                }
                else if (tipo == 6)
                {
                    //United
                    hora6 = hora6.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "United" + ".ini")));
                    label6.Text = hora6.ToLongTimeString();
                }
                else if (tipo == 7)
                {
                    //Rushback
                    hora7 = hora7.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Rushback" + ".ini")));
                    label7.Text = hora7.ToLongTimeString();
                }
                else if (tipo == 8)
                {
                    //Never
                    hora8 = hora8.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Never" + ".ini")));
                    label8.Text = hora8.ToLongTimeString();
                }
                else if (tipo == 9)
                {
                    //Vortex
                    hora9 = hora9.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Vortex" + ".ini")));
                    label9.Text = hora9.ToLongTimeString();
                }
                else if (tipo == 10)
                {
                    //Hunted
                    hora10 = hora10.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Hunted" + ".ini")));
                    label10.Text = hora10.ToLongTimeString();
                }
                else if (tipo == 11)
                {
                    //Blood
                    hora11 = hora11.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Blood" + ".ini")));
                    label11.Text = hora11.ToLongTimeString();
                }
                else if (tipo == 12)
                {
                    //River
                    hora12 = hora12.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "River" + ".ini")));
                    label12.Text = hora12.ToLongTimeString();
                }
                else if (tipo == 13)
                {
                    //Flawless
                    hora13 = hora13.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Flawless" + ".ini")));
                    label13.Text = hora13.ToLongTimeString();
                }
                else if (tipo == 14)
                {
                    //Destiny
                    hora14 = hora14.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Destiny" + ".ini")));
                    label14.Text = hora14.ToLongTimeString();
                }
                else if (tipo == 15)
                {
                    //Fun
                    hora15 = hora15.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Fun" + ".ini")));
                    label15.Text = hora15.ToLongTimeString();
                }
                else if (tipo == 16) {
                    //Chaos
                    hora16 = hora16.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Chaos" + ".ini")));
                    label16.Text = hora16.ToLongTimeString();
                }
                else if (tipo == 17) { 
                    //Die
                    hora17 = hora17.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Die" + ".ini")));
                    label17.Text = hora17.ToLongTimeString();
                }
                else if (tipo == 18) { 
                    //Sacrifice
                    hora18 = hora18.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Sacrifice" + ".ini")));
                    label18.Text = hora18.ToLongTimeString();
                }
                else if (tipo == 19) { 
                    //Heaven
                    hora19 = hora19.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Heaven" + ".ini")));
                    label19.Text = hora19.ToLongTimeString();
                }
                else if (tipo == 20) { 
                    //Law
                    hora20 = hora20.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Law" + ".ini")));
                    label20.Text = hora20.ToLongTimeString();
                }
                else if (tipo == 21) { 
                    //Dragons
                    hora21 = hora21.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Dragons" + ".ini")));
                    label21.Text = hora21.ToLongTimeString();
                }
                else if (tipo == 22) {
                    //Soul
                    hora22 = hora22.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Soul" + ".ini")));
                    label22.Text = hora22.ToLongTimeString();
                }
            }
        }

        public void msg(string texto)
        {
            MessageBox.Show(texto);
        }

        public Form2()
        {
            InitializeComponent();
            pegarCorridas(99);
            /*hora = hora.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Black Edition" + ".ini")));
            label1.Text = hora.ToLongTimeString();
            hora2 = hora2.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "Fossa Party" + ".ini")));
            label2.Text = hora2.ToLongTimeString();
            hora3 = hora3.AddMinutes(Convert.ToInt32(Conexao.ReadIni("Corrida", "Tempo", @".\Corridas\" + "PlayBoy Only" + ".ini")));
            label3.Text = hora3.ToLongTimeString();*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //ANTIGOS
            hora = hora.AddSeconds(-1);
            label1.Text = hora.ToLongTimeString();
            hora2 = hora2.AddSeconds(-1);
            label2.Text = hora2.ToLongTimeString();
            hora3 = hora3.AddSeconds(-1);
            label3.Text = hora3.ToLongTimeString();
            //Novos
            hora4 = hora4.AddSeconds(-1);
            label4.Text = hora4.ToLongTimeString();
            hora5 = hora5.AddSeconds(-1);
            label5.Text = hora5.ToLongTimeString();
            hora6 = hora6.AddSeconds(-1);
            label6.Text = hora6.ToLongTimeString();
            hora7 = hora7.AddSeconds(-1);
            label7.Text = hora7.ToLongTimeString();
            hora8 = hora8.AddSeconds(-1);
            label8.Text = hora8.ToLongTimeString();
            hora9 = hora9.AddSeconds(-1);
            label9.Text = hora9.ToLongTimeString();
            hora10 = hora10.AddSeconds(-1);
            label10.Text = hora10.ToLongTimeString();
            hora11 = hora11.AddSeconds(-1);
            label11.Text = hora11.ToLongTimeString();
            hora12 = hora12.AddSeconds(-1);
            label12.Text = hora12.ToLongTimeString();
            hora13 = hora13.AddSeconds(-1);
            label13.Text = hora13.ToLongTimeString();
            hora14 = hora14.AddSeconds(-1);
            label14.Text = hora14.ToLongTimeString();
            hora15 = hora15.AddSeconds(-1);
            label15.Text = hora15.ToLongTimeString();
            hora16 = hora16.AddSeconds(-1);
            label16.Text = hora16.ToLongTimeString();
            hora17 = hora17.AddSeconds(-1);
            label17.Text = hora17.ToLongTimeString();
            hora18 = hora18.AddSeconds(-1);
            label18.Text = hora18.ToLongTimeString();
            hora19 = hora19.AddSeconds(-1);
            label19.Text = hora19.ToLongTimeString();
            hora20 = hora20.AddSeconds(-1);
            label20.Text = hora20.ToLongTimeString();
            hora21 = hora21.AddSeconds(-1);
            label21.Text = hora21.ToLongTimeString();
            hora22 = hora22.AddSeconds(-1);
            label22.Text = hora22.ToLongTimeString();

            //Deluxe, Legend, United, Rushback, Never, Vortex, 
            //Hunted, Blood, River, Flawless, Destiny, 
            //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
            //Reiniciar
            if (label1.Text == "00:00:00")
            {
                pegarCorridas(1);
                ChatServidor.Ganhador("Black Edition");
            }

            if(label2.Text == "00:00:00")
            {
                pegarCorridas(2);
                ChatServidor.Ganhador("Fossa Party");
            }

            if(label3.Text == "00:00:00")
            {
                pegarCorridas(3);
                ChatServidor.Ganhador("PlayBoy Only");
            }
            //Novos
            if (label4.Text == "00:00:00")
            {
                pegarCorridas(4);
                ChatServidor.Ganhador("Deluxe");
            }

            if (label5.Text == "00:00:00")
            {
                pegarCorridas(5);
                ChatServidor.Ganhador("Legend");
            }

            if (label6.Text == "00:00:00")
            {
                pegarCorridas(6);
                ChatServidor.Ganhador("United");
            }

            if (label7.Text == "00:00:00")
            {
                pegarCorridas(7);
                ChatServidor.Ganhador("Rushback");
            }

            if (label8.Text == "00:00:00")
            {
                pegarCorridas(8);
                ChatServidor.Ganhador("Never");
            }

            if (label9.Text == "00:00:00")
            {
                pegarCorridas(9);
                ChatServidor.Ganhador("Vortex");
            }

            //Hunted, Blood, River, Flawless, Destiny, 
            //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
            if (label10.Text == "00:00:00")
            {
                pegarCorridas(10);
                ChatServidor.Ganhador("Hunted");
            }

            if (label11.Text == "00:00:00")
            {
                pegarCorridas(11);
                ChatServidor.Ganhador("Blood");
            }

            if (label12.Text == "00:00:00")
            {
                pegarCorridas(12);
                ChatServidor.Ganhador("River");
            }

            if (label13.Text == "00:00:00")
            {
                pegarCorridas(13);
                ChatServidor.Ganhador("Flawless");
            }

            if (label14.Text == "00:00:00")
            {
                pegarCorridas(14);
                ChatServidor.Ganhador("Destiny");
            }

            if (label15.Text == "00:00:00")
            {
                pegarCorridas(15);
                ChatServidor.Ganhador("Fun");
            }

            //Fun, Chaos, Die, Sacrifice, Heaven, Law, Dragons, Soul
            if (label16.Text == "00:00:00")
            {
                pegarCorridas(16);
                ChatServidor.Ganhador("Chaos");
            }

            if (label17.Text == "00:00:00")
            {
                pegarCorridas(17);
                ChatServidor.Ganhador("Die");
            }

            if (label18.Text == "00:00:00")
            {
                pegarCorridas(18);
                ChatServidor.Ganhador("Sacrifice");
            }

            if (label19.Text == "00:00:00")
            {
                pegarCorridas(19);
                ChatServidor.Ganhador("Heaven");
            }

            if (label20.Text == "00:00:00")
            {
                pegarCorridas(20);
                ChatServidor.Ganhador("Law");
            }

            if (label21.Text == "00:00:00")
            {
                pegarCorridas(21);
                ChatServidor.Ganhador("Dragons");
            }

            if (label22.Text == "00:00:00")
            {
                pegarCorridas(22);
                ChatServidor.Ganhador("Soul");
            }
        }
    }
}
