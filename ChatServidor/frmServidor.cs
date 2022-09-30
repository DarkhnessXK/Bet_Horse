using System.Windows.Forms;
using System.Net;
using System;
using System.Threading;

namespace ChatServidor
{
    public partial class Form1 : Form
    {
        private delegate void AtualizaStatusCallback(string strMensagem);
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }


        public static void Mensagem(string txt)
        {
            MessageBox.Show(txt);
        }

        public Form1()
        {
            InitializeComponent();
            Program.abrir = new Form2();
            Program.abrir.Visible = true;
            txtIP.Text = "127.0.0.1";
        }

        private void btnAtender_Click(object sender, System.EventArgs e)
        {
            if (txtIP.Text==string.Empty)
            {
                MessageBox.Show("Informe o endereço IP.");
                txtIP.Focus();
                return;
            }

            try
            {
                // Analisa o endereço IP do servidor informado no textbox
                IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);

                // Cria uma nova instância do objeto ChatServidor
                ChatServidor mainServidor = new ChatServidor(enderecoIP);

                // Vincula o tratamento de evento StatusChanged a mainServer_StatusChanged
                ChatServidor.StatusChanged += new StatusChangedEventHandler(mainServidor_StatusChanged);

                // Inicia o atendimento das conexões
                mainServidor.IniciaAtendimento();

                // Mostra que nos iniciamos o atendimento para conexões
                txtLog.AppendText("Monitorando as conexões...\r\n");

                Conexao.WriteINI("Dados", "Status", "1", @".\Dados.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão23 : " + ex.Message);
            }
        }

        public void mainServidor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Chama o método que atualiza o formulário
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            // Atualiza o logo com mensagens
            txtLog.AppendText(strMensagem + "\r\n");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Conexao.WriteINI("Dados", "Status", "0", @".\Dados.ini");
            Conexao.WriteINI("Dados", "Online", "0", @".\Dados.ini");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
