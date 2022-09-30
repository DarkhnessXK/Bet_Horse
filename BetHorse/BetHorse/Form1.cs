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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BetHorse
{
    public partial class Form1 : Form
    {
        //Dados da Conexão
        private StreamWriter stwEnviador;
        private StreamReader strReceptor;
        private TcpClient tcpServidor;
        private Thread mensagemThread;
        private IPAddress enderecoIP = IPAddress.Parse("127.0.0.1");
        private bool Conectado = false;
        private int count = 0;
        private string Path = Environment.CurrentDirectory + @"\Lembrar.ini";
        private string Path_Mallet = Environment.CurrentDirectory + @"\Mallet.ini";

        /* >> Delegate << */
        private delegate void AttStatus(string mensagem, string cor);
        private delegate void AttIrPainel();
        private delegate void AttEscreverPainel(string mensagem);
        private delegate void AttStatusCadastro(string mensagem, string cor);
        private delegate void AttStatusSup(string mensagem, string cor);

        //Status - delegate 'AttStatus'
        private void Status(string mensagem, string cor)
        {
            toolStripStatusLabel1.Text = mensagem;
            if (cor == "Red")
            {
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else if (cor == "Green")
            {
                toolStripStatusLabel1.ForeColor = Color.Green;
            }
            else
            {
                toolStripStatusLabel1.ForeColor = Color.Black;
            }
        }

        //irPainel - delegate 'AttIrPainel'
        private void IrPainel()
        {
            this.Visible = false;
            this.Hide();
            Program.painelForm = new Form3();
            Program.painelForm.Show();
            //Se é um dos criadores
            if (textBox1.Text.ToLower() == "singelo" || textBox1.Text.ToLower() == "richardson" || textBox1.Text.ToLower() == "giovanni")
            {
                Program.painelForm.administradorToolStripMenuItem.Visible = true;
            }
        }

        //EscreverPainel - delegate 'AttEscreverPainel'
        private void EscreverPainel(string mensagem)
        {
            Program.painelForm.EscreverText(mensagem);
        }

        //StatusCadastro - delegate 'AttStatusCadastro'
        private void StatusCadastro(string mensagem, string cor)
        {
            Program.cadastroForm.Status(mensagem, cor);
        }

        //StatusSup - delegate 'AttStatusSup'
        private void StatusSup(string mensagem, string cor)
        {
            Program.supForm.toolStripStatusLabel1.Text = mensagem;
            if (cor == "Green")
            {
                Program.supForm.toolStripStatusLabel1.ForeColor = Color.Green;
            }
            else
            {
                Program.supForm.toolStripStatusLabel1.ForeColor = Color.Red;
            }
        }

        //Iniciar conexão
        public void IniciarConexao(string nome, string senha, string tipo)
        {
            try
            {
                //Iniciar conexão
                tcpServidor = new TcpClient();
                tcpServidor.Connect(enderecoIP, 3389);

                //Definir que estamos conectados
                Conectado = true;

                //Criar StreamWriter e enviar a primeira mensagem
                stwEnviador = new StreamWriter(tcpServidor.GetStream());
                stwEnviador.WriteLine(nome + "/" + senha + "/" + tipo);
                stwEnviador.Flush();

                //Iniciar Thread
                mensagemThread = new Thread(new ThreadStart(ReceberDados));
                mensagemThread.Start();
            }
            catch
            {
                count++;
                if (Application.OpenForms.OfType<Form2>().Count() > 0)
                {
                    if (Program.cadastroForm.Visible == true)
                    {
                        this.Invoke(new AttStatusCadastro(this.StatusCadastro), new object[] { "Offline - Servidor não está ligado... (" + count + ")", "Red" });
                    }
                }else if (Application.OpenForms.OfType<Form9>().Count() > 0)
                {
                    if (Program.supForm.Visible == true)
                    {
                        this.Invoke(new AttStatusSup(this.StatusSup), new object[] { "Offline - Servidor não está ligado... (" + count + ")", "Red"});
                    }
                }
                else
                {
                    this.Invoke(new AttStatus(this.Status), new object[] { "Offline - Servidor não está ligado... (" + count + ")", "Red" });
                }
            }
        }

        private void ReceberDados()
        {
            strReceptor = new StreamReader(tcpServidor.GetStream());
            string ConResposta = strReceptor.ReadLine();
            Form1 form1 = this;

            // >> PRIMEIRO TRATAMENTO <<
            if (ConResposta[0] == '0')
            {
                //Conta não existe -:> 0
                form1.Invoke(new AttStatus(form1.Status), new object[] { "Conta não existe", "Red" });
                FecharConexao();
            }
            else if (ConResposta[0] == '2')
            {
                //Acesso negado -:> 2
                form1.Invoke(new AttStatus(form1.Status), new object[] { "Esta conta não tem mais acesso", "Red" });
                FecharConexao();
            }
            else if (ConResposta[0] == '3')
            {
                //Senha errada -:> 3
                form1.Invoke(new AttStatus(form1.Status), new object[] { "Senha incorreta", "Red" });
                FecharConexao();
            }
            else if (ConResposta[0] == '4')
            {
                //Usuário conectado -:> 4
                form1.Invoke(new AttStatus(form1.Status), new object[] { "Usuário conectado", "d" });
                FecharConexao();
            }
            else if (ConResposta[0] == '5')
            {
                //Conta já existe 'Cadastro' -:> 5
                form1.Invoke(new AttStatusCadastro(form1.StatusCadastro), new object[] { "Conta já existe", "red" });
                FecharConexao();
            }
            else if (ConResposta[0] == '6')
            {
                //Conta cadastrada 'Cadastro' -:> 6
                form1.Invoke(new AttStatusCadastro(form1.StatusCadastro), new object[] { "Cadastrado com sucesso", "Green" });
                FecharConexao();
            }
            else if (ConResposta[0] == '7')
            {
                //Enviado 'Suporte' -:> 7
                form1.Invoke(new AttStatusSup(form1.StatusSup), new object[] { "Mensagem enviada com sucesso", "Green" });
                FecharConexao();
            }
            else
            {
                if (File.Exists(Path))
                {
                    //Lembrar.ini
                    if (checkBox1.Checked == false)
                    {
                        Arquivo.WriteINI("Lembrar", "Valor", "False", Path);
                    }
                    else
                    {
                        Arquivo.WriteINI("Lembrar", "Login", textBox1.Text, Path);
                        Arquivo.WriteINI("Lembrar", "Senha", textBox2.Text, Path);
                        Arquivo.WriteINI("Lembrar", "Valor", "True", Path);
                    }
                }
                else
                {
                    //Lembrar.ini NEW
                    if (checkBox1.Checked == false)
                    {
                        Arquivo.WriteINI("Lembrar", "Login", textBox1.Text, Path);
                        Arquivo.WriteINI("Lembrar", "Senha", textBox2.Text, Path);
                        Arquivo.WriteINI("Lembrar", "Valor", "False", Path);
                        Arquivo.WriteINI("Lembrar", "Musica", "True", Path);
                        Arquivo.WriteINI("Lembrar", "Trade", "False", Path);
                    }
                    else
                    {
                        Arquivo.WriteINI("Lembrar", "Login", textBox1.Text, Path);
                        Arquivo.WriteINI("Lembrar", "Senha", textBox2.Text, Path);
                        Arquivo.WriteINI("Lembrar", "Valor", "True", Path);
                        Arquivo.WriteINI("Lembrar", "Musica", "True", Path);
                        Arquivo.WriteINI("Lembrar", "Trade", "False", Path);
                    }
                }

                //Login realizado
                form1.Invoke(new AttIrPainel(form1.IrPainel));
            }

            //Packet's
            while (Conectado)
            {
                try
                {
                    form1.Invoke(new AttEscreverPainel(form1.EscreverPainel), new object[] { form1.strReceptor.ReadLine() });
                }
                catch (IOException i)
                {
                    //MessageBox.Show(Convert.ToString(i));
                }
                catch (Exception e)
                {
                    //MessageBox.Show(Convert.ToString(e));
                }

            }
        }

        //Fechar programa
        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Conectado == true)
            {
                FecharConexao();
            }
        }

        //Fechar Conexão
        public void FecharConexao(int tipo = 0)
        {
            if (tipo == 0)
            {
                //Fecha os objetos
                Conectado = false;
                stwEnviador.Close();
                strReceptor.Close();
                tcpServidor.Close();
                mensagemThread.Abort(); //Terminar Thread

                //Exit  
                Application.Exit();
            }
            else
            {
                //Fecha os objetos
                Conectado = false;
                stwEnviador.Close();
                strReceptor.Close();
                tcpServidor.Close();
                mensagemThread.Abort(); //Terminar Thread

                //Program.painelForm.Close();
                Show();
            }
        }

        public Form1()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponent();
            if (File.Exists(Path))
            {
                if (Arquivo.ReadIni("Lembrar", "Valor", Path) == "True")
                {
                    checkBox1.Checked = true;
                    textBox1.Text = Arquivo.ReadIni("Lembrar", "Login", Path);
                    textBox2.Text = Arquivo.ReadIni("Lembrar", "Senha", Path);
                }
            }

            if (File.Exists(Path_Mallet))
            {
                if (Arquivo.ReadIni("Login", "TrocarIP", Path_Mallet) == "true")
                {
                    textBox3.Visible = true;
                    button2.Visible = true;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.PerformClick();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Application.OpenForms.OfType<Form2>().Count() > 0)
            {
                Program.cadastroForm.Visible = false;
                Program.cadastroForm.Visible = true;
            }
            else
            {
                //Novo
                Program.cadastroForm = new Form2();
                Program.cadastroForm.Visible = false;
                Program.cadastroForm.Visible = true;
            }
        }

        public void Send(string mensagem)
        {
            if (Conectado)
            {
                stwEnviador.WriteLine(mensagem);
                stwEnviador.Flush();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 14;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Conectado == false)
            {
                if (textBox1.Text != "")
                {
                    if(textBox2.Text != "")
                    {
                        if(textBox1.Text.Contains("/") == true || textBox2.Text.Contains("/") == true)
                        {
                            this.Invoke(new AttStatus(Status), new object[] { "Não é permitido o uso do '/'", "Red" });
                        }
                        else
                        {
                            string[] teste = textBox1.Text.Split(' ');

                            //Corrigir o nome - ESTRANHO -
                            var upper1 = char.ToUpper(teste[0][0]) + teste[0].Substring(1).ToLower();
                            string nome = upper1;
                            IniciarConexao(nome, textBox2.Text, "entrar");
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
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                Program.supForm.Visible = false;
                Program.supForm.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            enderecoIP = IPAddress.Parse(textBox3.Text);
            MessageBox.Show("IP Alterado");
        }
    }
}
