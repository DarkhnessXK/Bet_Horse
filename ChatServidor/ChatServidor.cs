using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServidor
{ 
    // Trata os argumentos para o evento StatusChanged
    public class StatusChangedEventArgs : EventArgs
    {
        // Estamos interessados na mensagem descrevendo o evento
        private string EventMsg;

        // Propriedade para retornar e definir um mensagem do evento
        public string EventMessage
        {
            get { return EventMsg;}
            set { EventMsg = value;}
        }

        // Construtor para definir a mensagem do evento
        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    // Este delegate é necessário para especificar os parametros que estamos pasando com o nosso evento
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    class ChatServidor
    {

        // Esta hash table armazena os usuários e as conexões (acessado/consultado por usuário)
        public static Hashtable htUsuarios = new Hashtable(30); // 30 usuarios é o limite definido
        // Esta hash table armazena os usuários e as conexões (acessada/consultada por conexão)
        public static Hashtable htConexoes = new Hashtable(30); // 30 usuários é o limite definido
        // armazena o endereço IP passado
        private IPAddress enderecoIP;
        private TcpClient tcpCliente;
        // O evento e o seu argumento irá notificar o formulário quando um usuário se conecta, desconecta, envia uma mensagem,etc
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;

        // O construtor define o endereço IP para aquele retornado pela instanciação do objeto
        public ChatServidor(IPAddress endereco)
        {
            enderecoIP = endereco;
        }

        // A thread que ira tratar o escutador de conexões
        private Thread thrListener;

        // O objeto TCP object que escuta as conexões
        private TcpListener tlsCliente;

        // Ira dizer ao laço while para manter a monitoração das conexões
        bool ServRodando = false;


        // Inclui o usuário nas tabelas hash
        public static void IncluiUsuario(TcpClient tcpUsuario, string strUsername)
        {
            //Separar o usuário da senha
            string[] texto = strUsername.Split('/');

            //AdicionarRegistro
            int online = Convert.ToInt32(Conexao.ReadIni("Dados", "Online", @".\Dados.ini")) + 1;
            Conexao.WriteINI("Dados", "Online", Convert.ToString(online), @".\Dados.ini");

            // Primeiro inclui o nome e conexão associada para ambas as hash tables
            ChatServidor.htUsuarios.Add(texto[0], tcpUsuario);
            ChatServidor.htConexoes.Add(tcpUsuario, texto[0]);

            e = new StatusChangedEventArgs(Convert.ToString(htUsuarios[texto[0]]));
            OnStatusChanged(e);

            // Informa a nova conexão para todos os usuário e para o formulário do servidor
            EnviaMensagemAdmin(htConexoes[tcpUsuario] + " entrou");
        }

        // Remove o usuário das tabelas (hash tables)
        public static void RemoveUsuario(TcpClient tcpUsuario)
        {
            // Se o usuário existir
            if (htConexoes[tcpUsuario] != null)
            {
                // Primeiro mostra a informação e informa os outros usuários sobre a conexão
                EnviaMensagemAdmin(htConexoes[tcpUsuario] + " saiu");

                //AdicionarRegistro
                int online = Convert.ToInt32(Conexao.ReadIni("Dados", "Online", @".\Dados.ini")) - 1;
                Conexao.WriteINI("Dados", "Online", Convert.ToString(online), @".\Dados.ini");

                // Removeo usuário da hash table
                ChatServidor.htUsuarios.Remove(ChatServidor.htConexoes[tcpUsuario]);
                ChatServidor.htConexoes.Remove(tcpUsuario);
            }
        }

        // Este evento é chamado quando queremos disparar o evento StatusChanged
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                // invoca o  delegate
                statusHandler(null, e);
            }
        }

        //Buscar
        public static string BuscaArquivos(DirectoryInfo dir, int tipo = 0)
        {
            int count = 0;
            string teste = "";

            // lista arquivos do diretorio corrente
            foreach (FileInfo file in dir.GetFiles())
            {
                count++;
                teste += file.Name + "/";
            }

            string[] array = teste.Split('/');
            string ultimo = array[count - 1].Replace(".ini", "");

            if (tipo == 1)
            {
                ultimo = teste.Replace(".ini", "");
            }

            return ultimo;
        }

        //Variables Corrida
        public static string[] cavalos = { "Honey", "Coronel", "Soberano", "Sucesso", "Pirata", "Passo Preto", "Craque", "Brinquedo",
            "Profeta", "Triunfo"};

        //Verificar ganhador
        public static void Ganhador(string nome_corrida)
        {
            //Pegar Jogadores
            string[] nome = Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + nome_corrida + ".ini").Split(',');
            string[] cavalo = Conexao.ReadIni("Jogadores", "Cavalo", @".\Corridas\" + nome_corrida + ".ini").Split(',');
            string[] aposta = Conexao.ReadIni("Jogadores", "Aposta", @".\Corridas\" + nome_corrida + ".ini").Split(',');
            string cavalo_ganhador = "";
            string nome_jogador = "";
            string dinheiro = "";
            string aposta_ganhador = "";
            string jogador_c = "";
            int new_dinheiro = 0;
            int old_dinheiro = 0;
            int controle = 0;
            int jogadorc = 0;
            int new_ganhos = 0;
            bool passar = false;

            //Casa
            string fundos = "";
            int new_fundos = 0;

            //Gera o cavalo vencendor
            Random cavaloWin = new Random();
            int valor = Convert.ToInt32(cavaloWin.Next(cavalos.Length));
            cavalo_ganhador = cavalos[valor];
            
            e = new StatusChangedEventArgs(" CAVALO GANHADOR >> INT = "+ valor + "Cavalo ganhador = " + cavalo_ganhador);
            OnStatusChanged(e);

            //Verificar se houve apostas
            if (Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + nome_corrida + ".ini") == "")
            {
                //Não houve aposta
            }
            else
            {
                //Houve apostas
                if (nome.Length == 2)
                {   
                    //Uma aposta
                    if (cavalo[0] == cavalo_ganhador)
                    {
                        nome_jogador = nome[0];
                        aposta_ganhador = aposta[0];
                    }
                    else
                    {
                        nome_jogador = "Casa,";
                        aposta_ganhador = Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + nome_corrida + ".ini");
                    }
                }
                else
                {
                    //Houve mais apostas
                    for (int a = 0; a < nome.Length-1; a++)
                    {
                        if (cavalo[a] == cavalo_ganhador)
                        {
                            nome_jogador = nome_jogador + nome[a] + ",";
                            aposta_ganhador = aposta_ganhador + aposta[a] + ",";
                            passar = true;
                        }
                        else
                        {
                            if (passar == false)
                            {
                                //Não teve vencendor
                                nome_jogador = "Casa,";
                                aposta_ganhador = Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + nome_corrida + ".ini") + ",";
                            }
                        }
                    }
                }

                e = new StatusChangedEventArgs(" TESTANDO POHA22 >> " + nome_jogador);
                OnStatusChanged(e);

                e = new StatusChangedEventArgs(" TESTANDO POHA223 >> " + aposta_ganhador);
                OnStatusChanged(e);


                //Verificar se houve empate
                // ---- 'controle' ----
                // 1 - casa ganhou
                // 2 - jogador ganhou
                if (nome_jogador.Split(',').Length == 2)
                {
                    //Não houve
                    if (nome_jogador.Split(',')[0] == "Casa")
                    {
                        //Se a casa ganhou
                        fundos = Conexao.ReadIni("Casa", "Fundos", @".\Corridas\Casa.ini");
                        new_fundos = Convert.ToInt32(fundos) + Convert.ToInt32(Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + nome_corrida + ".ini"));
                        Conexao.WriteINI("Casa", "Fundos", Convert.ToString(new_fundos), @".\Corridas\Casa.ini");

                        controle = 1;
                    }
                    else
                    {
                        for (int b = 0; b < nome_jogador.Split(',').Length-1; b++)
                        {
                            if (nome_jogador.Split(',')[b] == "Casa")
                            {
                                nome_jogador.Remove(b);
                            }
                        }

                        //Se o jogador ganhou
                        dinheiro = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + nome_jogador.Split(',')[0] + ".ini");
                        new_dinheiro = Convert.ToInt32(aposta_ganhador.Split(',')[0]);
                        old_dinheiro = new_dinheiro * 2;
                        new_dinheiro = Convert.ToInt32(dinheiro) + old_dinheiro;
                        Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(new_dinheiro), @".\Contas\" + nome_jogador.Split(',')[0] + ".ini");

                        //Remover fundos da casa
                        fundos = Conexao.ReadIni("Casa", "Fundos", @".\Corridas\Casa.ini");
                        new_fundos = Convert.ToInt32(fundos) + (Convert.ToInt32(Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + nome_corrida + ".ini")) - old_dinheiro);
                        Conexao.WriteINI("Casa", "Fundos", Convert.ToString(new_fundos), @".\Corridas\Casa.ini");

                        controle = 2;
                    }
                }
                else
                {
                    //Houve empate
                    Random jogador = new Random();
                    jogadorc = jogador.Next(0, nome_jogador.Split(',').Length - 1);

                    //Se o jogador ganhou
                    dinheiro = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + nome_jogador.Split(',')[0] + ".ini");
                    new_dinheiro = Convert.ToInt32(aposta_ganhador.Split(',')[0]);
                    old_dinheiro = new_dinheiro * 2;
                    new_dinheiro = Convert.ToInt32(dinheiro) + old_dinheiro;
                    Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(new_dinheiro), @".\Contas\" + nome_jogador.Split(',')[0] + ".ini");

                    //Remover fundos da casa
                    fundos = Conexao.ReadIni("Casa", "Fundos", @".\Corridas\Casa.ini");
                    new_fundos = Convert.ToInt32(fundos) + (Convert.ToInt32(Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + nome_corrida + ".ini")) - old_dinheiro);
                    Conexao.WriteINI("Casa", "Fundos", Convert.ToString(new_fundos), @".\Corridas\Casa.ini");

                    jogador_c = nome_jogador.Split(',')[jogadorc];
                    controle = 3;
                }

                //Autenticar 
                if (controle == 2)
                {
                    Autenticar(nome_jogador.Split(',')[0]);
                }
                else if (controle == 3)
                {
                    Autenticar(jogador_c);
                }

                // --------- 'premio' ----------
                // 1 - nome do ganhador
                // 2 - cavalo ganhador
                // 3 - prémio(R$)
                // 4 - emblema do ganhador
                //--------------------------
                StreamWriter swSenderSender;
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
                // Percorre a lista de clientes TCP
                for (int i = 0; i < tcpClientes.Length; i++)
                {
                    // Tenta enviar uma mensagem para cada cliente
                    try
                    {
                        // Se a mensagem estiver em branco ou a conexão for nula sai...
                        if (tcpClientes[i] == null)
                        {
                            continue;
                        }
                        if (controle == 2)
                        {
                            swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                            swSenderSender.WriteLine("premio/" + nome_jogador.Split(',')[0] + "/" + cavalo_ganhador + "/" + Convert.ToString(old_dinheiro) + "/" + Conexao.ReadIni("Conta", "Foto", @".\Contas\" + nome_jogador.Split(',')[0] + ".ini") + "/" + nome_corrida);
                            swSenderSender.Flush();
                            swSenderSender = null;

                            //Adicionar Ganhos
                            new_ganhos = Convert.ToInt32(Conexao.ReadIni("Conta", "TG", @".\Contas\" + nome_jogador.Split(',')[0] + ".ini")) + old_dinheiro;
                            Conexao.WriteINI("Conta", "TG", Convert.ToString(new_ganhos), @".\Contas\" + nome_jogador.Split(',')[0] + ".ini");
                            Autenticar(nome_jogador.Split(',')[0]);
                        }
                        else if (controle == 3)
                        {
                            swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                            swSenderSender.WriteLine("premio/" + jogador_c + "/" + cavalo_ganhador + "/" + Convert.ToString(old_dinheiro) + "/" + Conexao.ReadIni("Conta", "Foto", @".\Contas\" + jogador_c + ".ini") + "/" + nome_corrida);
                            swSenderSender.Flush();
                            swSenderSender = null;

                            //Adicionar Ganhos
                            new_ganhos = Convert.ToInt32(Conexao.ReadIni("Conta", "TG", @".\Contas\" + jogador_c + ".ini")) + old_dinheiro;
                            Conexao.WriteINI("Conta", "TG", Convert.ToString(new_ganhos), @".\Contas\" + jogador_c + ".ini");
                            Autenticar(jogador_c);
                        }
                        else if (controle == 1)
                        {
                            swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                            swSenderSender.WriteLine("premio/Casa" + "/" + cavalo_ganhador + "/" + Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + nome_corrida + ".ini") + "/" + "" + "/" + nome_corrida);
                            swSenderSender.Flush();
                            swSenderSender = null;
                        }
                    }
                    catch // Se houver um problema , o usuário não existe , então remove-o
                    {
                        RemoveUsuario(tcpClientes[i]);
                    }
                }

                // ----- WIN Horse -----
                string antigo_win_horse = Conexao.ReadIni("Cavalo", "Vitorias", @".\Cavalos\" + cavalo_ganhador + ".ini");
                int new_antigo_win_horse = Convert.ToInt32(antigo_win_horse) + 1;
                Conexao.WriteINI("Cavalo", "Vitorias", Convert.ToString(new_antigo_win_horse), @".\Cavalos\" + cavalo_ganhador + ".ini");

                // ----- Rea -----
                black_count = 0;
                Conexao.WriteINI("Jogadores", "Index", "", @".\Corridas\"+ nome_corrida +".ini");
                Conexao.WriteINI("Jogadores", "Nome", "", @".\Corridas\" + nome_corrida + ".ini");
                Conexao.WriteINI("Jogadores", "Cavalo", "", @".\Corridas\" + nome_corrida + ".ini");
                Conexao.WriteINI("Jogadores", "Aposta", "", @".\Corridas\" + nome_corrida + ".ini");
                Conexao.WriteINI("Corrida", "Pote", "0", @".\Corridas\" + nome_corrida + ".ini");
            }
        }

        //Index gerados
        public static int black_count = 0;

        private static void DiminuirSaldo(string nome, string dinheiro)
        {
            string money = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\"+nome+".ini");
            int new_money = 0;

            new_money = Convert.ToInt32(money) - Convert.ToInt32(dinheiro);
            Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(new_money), @".\Contas\"+nome+".ini");
            Autenticar(nome);
        }

        private static void AumentarSaldo(string nome, string dinheiro)
        {
            string money = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + nome + ".ini");
            int new_money = 0;

            new_money = Convert.ToInt32(money) + Convert.ToInt32(dinheiro);
            Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(new_money), @".\Contas\" + nome + ".ini");
            Autenticar(nome);
        }

        //Adicionar Registro
        public static void AdicionarRegistro(string nome, string cavalo, string dinheiro, string corrida)
        {
            string path_rg = @".\Registro.ini";
            string[] teste_codigos = File.ReadAllLines(@".\Registro.txt");
            string salvar_cod = "";
            int id = Convert.ToInt32(Conexao.ReadIni("Registro", "ID", path_rg)) + 1;
            Conexao.WriteINI("Registro", "ID", Convert.ToString(id), path_rg);
            OnStatusChanged(e);
            DateTime agora = DateTime.Now;

            if (Conexao.ReadIni("Registro", "RG", path_rg) == "")
            {
                File.AppendAllText(@".\Registro.txt", id + "|" + nome + "|" + cavalo + "|" + dinheiro + "|" + corrida + "|" + agora + Environment.NewLine);
            }
            else
            {
                for (int c = 0; c < teste_codigos.Length - 1; c++)
                {
                    // Primeiro exibe a mensagem na aplicação
                    e = new StatusChangedEventArgs("Registro >> " + teste_codigos[c]);
                    OnStatusChanged(e);
                    salvar_cod += teste_codigos[c] + ",";
                }

                salvar_cod += id + "|" + nome + "|" + cavalo + "|" + dinheiro + "|" + corrida + "|" + agora + ",";
                File.AppendAllText(@".\Registro.txt", id + "|" + nome + "|" + cavalo + "|" + dinheiro + "|" + corrida + "|" + agora + Environment.NewLine);
            }
        }

        //Adicionar apostador
        public static void AdicionarApostador(string nome, string cavalo, string dinheiro, string corrida)
        {
            string salvar_cod = "";
            string salvar_cod2 = "";
            string salvar_cod3 = "";
            string salvar_cod4 = "";
            string nova1 = "";
            string nova2 = "";
            string nova3 = "";
            string nova4 = "";
            string pote = Conexao.ReadIni("Corrida", "Pote", @".\Corridas\" + corrida + ".ini");
            string TA = Conexao.ReadIni("Conta", "TA", @".\Contas\" + nome + ".ini");
            int index = black_count + 1;
            int new_pote = 0;
            bool permitir = false;
            e = new StatusChangedEventArgs("Nome >> " + nome + "||" + cavalo + "||" + dinheiro + "||" + corrida);
            OnStatusChanged(e);

            //if (corrida == "Black Edition")
            //{
                if(Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + corrida + ".ini") != "")
                {
                    string[] verificar = Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + corrida + ".ini").Split(',');

                    for (int a = 0; a < verificar.Length-1; a++)
                    {
                        if (verificar[a] == nome)
                        {
                            //SENDER-SENDER PROBLEM
                            StreamWriter swSenderSender;

                            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                            // Copia os objetos TcpClient no array
                            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                            //Responder ao usuário que requisitou
                            ICollection MyKeys;
                            int count = 0;
                            MyKeys = htUsuarios.Keys;

                            foreach (object Key in MyKeys)
                            {
                                count++;
                                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                                OnStatusChanged(e);
                                if (Key.ToString() == nome)
                                {
                                    break;
                                }
                            }

                            // 1 - já apostou
                            swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                            swSenderSender.WriteLine("aposta/1");
                            swSenderSender.Flush();
                            swSenderSender = null;

                            permitir = true;
                        }
                    }

                    if(permitir == false)
                    {
                        AdicionarRegistro(nome, cavalo, dinheiro, corrida);
                        //Index
                        if (Conexao.ReadIni("Jogadores", "Index", @".\Corridas\" + corrida + ".ini") == "")
                        {
                            Conexao.WriteINI("Jogadores", "Index", Convert.ToString(index) + ",", @".\Corridas\" + corrida + ".ini");
                        }
                        else
                        {
                            salvar_cod = Conexao.ReadIni("Jogadores", "Index", @".\Corridas\" + corrida + ".ini");
                            nova1 = salvar_cod + index + ",";
                            Conexao.WriteINI("Jogadores", "Index", nova1, @".\Corridas\" + corrida + ".ini");
                        }
                        //Nome
                        if (Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + corrida + ".ini") == "")
                        {
                            Conexao.WriteINI("Jogadores", "Nome", nome + ",", @".\Corridas\" + corrida + ".ini");
                        }
                        else
                        {
                            salvar_cod2 = Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + corrida + ".ini");
                            nova2 = salvar_cod2 + nome + ",";
                            Conexao.WriteINI("Jogadores", "Nome", nova2, @".\Corridas\" + corrida + ".ini");
                        }
                        //Cavalo
                        if (Conexao.ReadIni("Jogadores", "Cavalo", @".\Corridas\" + corrida + ".ini") == "")
                        {
                            Conexao.WriteINI("Jogadores", "Cavalo", cavalo + ",", @".\Corridas\" + corrida + ".ini");
                        }
                        else
                        {
                            salvar_cod3 = Conexao.ReadIni("Jogadores", "Cavalo", @".\Corridas\" + corrida + ".ini");
                            nova3 = salvar_cod3 + cavalo + ",";
                            Conexao.WriteINI("Jogadores", "Cavalo", nova3, @".\Corridas\" + corrida + ".ini");
                        }
                        //Aposta
                        if (Conexao.ReadIni("Jogadores", "Aposta", @".\Corridas\" + corrida + ".ini") == "")
                        {
                            Conexao.WriteINI("Jogadores", "Aposta", dinheiro + ",", @".\Corridas\" + corrida + ".ini");
                        }
                        else
                        {
                            salvar_cod4 = Conexao.ReadIni("Jogadores", "Aposta", @".\Corridas\" + corrida + ".ini");
                            nova4 = salvar_cod4 + dinheiro + ",";
                            Conexao.WriteINI("Jogadores", "Aposta", nova4, @".\Corridas\" + corrida + ".ini");
                        }
                        // ---- Adicionar TA ----
                        int new_ta = Convert.ToInt32(TA) + 1;
                        Conexao.WriteINI("Conta", "TA", Convert.ToString(new_ta), @".\Contas\" + nome + ".ini");

                        //-------------//
                        black_count++;
                        
                        //Pote
                        new_pote = Convert.ToInt32(pote) + Convert.ToInt32(dinheiro);
                        Conexao.WriteINI("Corrida", "Pote", Convert.ToString(new_pote), @".\Corridas\" + corrida + ".ini");

                        //Diminuir saldo do apostador
                        DiminuirSaldo(nome, dinheiro);

                        //SENDER-SENDER PROBLEM
                        StreamWriter swSenderSender;

                        // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                        TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                        // Copia os objetos TcpClient no array
                        ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                        //Responder ao usuário que requisitou
                        ICollection MyKeys;
                        int count = 0;
                        MyKeys = htUsuarios.Keys;

                        foreach (object Key in MyKeys)
                        {
                            count++;
                            e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                            OnStatusChanged(e);
                            if (Key.ToString() == nome)
                            {
                                break;
                            }
                        }

                        // 2 - Aposta realizada
                        swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                        swSenderSender.WriteLine("aposta/2");
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                }
                else
                {
                    AdicionarRegistro(nome, cavalo, dinheiro, corrida);
                    //Index
                    if (Conexao.ReadIni("Jogadores", "Index", @".\Corridas\" + corrida + ".ini") == "")
                    {
                        Conexao.WriteINI("Jogadores", "Index", Convert.ToString(index) + ",", @".\Corridas\" + corrida + ".ini");
                    }
                    else
                    {
                        salvar_cod = Conexao.ReadIni("Jogadores", "Index", @".\Corridas\" + corrida + ".ini");
                        nova1 = salvar_cod + index + ",";
                        Conexao.WriteINI("Jogadores", "Index", nova1, @".\Corridas\" + corrida + ".ini");
                    }
                    //Nome
                    if (Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + corrida + ".ini") == "")
                    {
                        Conexao.WriteINI("Jogadores", "Nome", nome + ",", @".\Corridas\" + corrida + ".ini");
                    }
                    else
                    {
                        salvar_cod2 = Conexao.ReadIni("Jogadores", "Nome", @".\Corridas\" + corrida + ".ini");
                        nova2 = salvar_cod2 + nome + ",";
                        Conexao.WriteINI("Jogadores", "Nome", nova2, @".\Corridas\" + corrida + ".ini");
                    }
                    //Cavalo
                    if (Conexao.ReadIni("Jogadores", "Cavalo", @".\Corridas\" + corrida + ".ini") == "")
                    {
                        Conexao.WriteINI("Jogadores", "Cavalo", cavalo + ",", @".\Corridas\" + corrida + ".ini");
                    }
                    else
                    {
                        salvar_cod3 = Conexao.ReadIni("Jogadores", "Cavalo", @".\Corridas\" + corrida + ".ini");
                        nova3 = salvar_cod3 + cavalo + ",";
                        Conexao.WriteINI("Jogadores", "Cavalo", nova3, @".\Corridas\" + corrida + ".ini");
                    }
                    //Aposta
                    if (Conexao.ReadIni("Jogadores", "Aposta", @".\Corridas\" + corrida + ".ini") == "")
                    {
                        Conexao.WriteINI("Jogadores", "Aposta", dinheiro + ",", @".\Corridas\" + corrida + ".ini");
                    }
                    else
                    {
                        salvar_cod4 = Conexao.ReadIni("Jogadores", "Aposta", @".\Corridas\" + corrida + ".ini");
                        nova4 = salvar_cod4 + dinheiro + ",";
                        Conexao.WriteINI("Jogadores", "Aposta", nova4, @".\Corridas\" + corrida + ".ini");
                    }
                    // ---- Adicionar TA ----
                    int new_ta = Convert.ToInt32(TA) + 1;
                    Conexao.WriteINI("Conta", "TA", Convert.ToString(new_ta), @".\Contas\" + nome + ".ini");

                    //-------------//
                    black_count++;

                    //Pote
                    new_pote = Convert.ToInt32(pote) + Convert.ToInt32(dinheiro);
                    Conexao.WriteINI("Corrida", "Pote", Convert.ToString(new_pote), @".\Corridas\" + corrida + ".ini");

                    //Diminuir saldo do apostador
                    DiminuirSaldo(nome, dinheiro);

                    //SENDER-SENDER PROBLEM
                    StreamWriter swSenderSender;

                    // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                    TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                    // Copia os objetos TcpClient no array
                    ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                    //Responder ao usuário que requisitou
                    ICollection MyKeys;
                    int count = 0;
                    MyKeys = htUsuarios.Keys;

                    foreach (object Key in MyKeys)
                    {
                        count++;
                        e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                        OnStatusChanged(e);
                        if (Key.ToString() == nome)
                        {
                            break;
                        }
                    }

                    // 2 - Aposta realizada
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("aposta/2");
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
            //}
        }

        //Transferir Dinheiro
        public static void Transferir(string nome, string nome2, string quantia, string data)
        {
            /*
             * Para o cliente
             *  1 - usuário não existe
             *  2 - feito
             =================================
             0 - transferir
             1 - Nome de quem ta transferindo
             2 - Nome de quem ta recebendo
             3 - Quantia
             4 - Data (S/N)
             5 - Tipo
             */
                //Quebrar
                DirectoryInfo dirInfo = new DirectoryInfo(@".\TR\ ");
            string[] teste_trs1 = Conexao.ReadIni("Conta", "TR", @".\Contas\" + nome + ".ini").Split(',');
            string[] teste_trs2 = Conexao.ReadIni("Conta", "TR", @".\Contas\" + nome2 + ".ini").Split(',');
            string[] teste_data = data.Split(' '); //0 - data | 1 - horário
            string dinheiro = "";
            string salvar_cod = "";
            string salvar_cod2 = "";
            string arquivo = @".\Contas\" + nome2 + ".ini";
            int novatr = 0;
            int novodinheiro = 0;

            //SENDER-SENDER PROBLEM
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            // Exibe primeiro na aplicação
            e = new StatusChangedEventArgs("Transferência >> " + data);
            OnStatusChanged(e);

            //Responder ao usuário que requisitou
            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;
            
            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if (Key.ToString() == nome)
                {
                    break;
                }
            }

            if (File.Exists(arquivo))
            {
                novatr = Convert.ToInt32(BuscaArquivos(dirInfo)) + 1;

                //Diminuir de quem ta enviando
                dinheiro = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + nome + ".ini");
                novodinheiro = Convert.ToInt32(dinheiro)-Convert.ToInt32(quantia);
                Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(novodinheiro), @".\Contas\" + nome + ".ini");

                //Aumenta de quem ta recebendo
                dinheiro = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + nome2 + ".ini");
                novodinheiro = Convert.ToInt32(dinheiro) + Convert.ToInt32(quantia);
                Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(novodinheiro), @".\Contas\" + nome2 + ".ini");

                //Registrar transação
                Conexao.WriteINI("TR", "Nome", nome, @".\TR\"+Convert.ToString(novatr)+".ini");
                Conexao.WriteINI("TR", "Para", nome2, @".\TR\" + Convert.ToString(novatr) + ".ini");
                Conexao.WriteINI("TR", "Quantia", quantia, @".\TR\" + Convert.ToString(novatr) + ".ini");
                if(data != "Sem registro")
                {
                    Conexao.WriteINI("TR", "Data", teste_data[0].Replace(",", "/")+" "+teste_data[1], @".\TR\" + Convert.ToString(novatr) + ".ini");
                }
                else
                {
                    Conexao.WriteINI("TR", "Data", data, @".\TR\" + Convert.ToString(novatr) + ".ini");
                }

                //Registrar nas contas dos envolvidos [1]
                if (Conexao.ReadIni("Conta", "TR", @".\Contas\" + nome + ".ini") == "")
                {
                    Conexao.WriteINI("Conta", "TR", Convert.ToString(novatr) + ",", @".\Contas\" + nome + ".ini");
                }
                else
                {
                    for (int c = 0; c < teste_trs1.Length - 1; c++)
                    {
                        salvar_cod += teste_trs1[c] + ",";
                    }

                    salvar_cod += Convert.ToString(novatr) + ",";
                    Conexao.WriteINI("Conta", "TR", salvar_cod, @".\Contas\" + nome + ".ini");
                }

                //Registrar nas contas dos envolvidos [2]
                if (Conexao.ReadIni("Conta", "TR", @".\Contas\" + nome2 + ".ini") == "")
                {
                    Conexao.WriteINI("Conta", "TR", Convert.ToString(novatr) + ",", @".\Contas\" + nome2 + ".ini");
                }
                else
                {
                    for (int c = 0; c < teste_trs2.Length - 1; c++)
                    {
                        salvar_cod2 += teste_trs2[c] + ",";
                    }

                    salvar_cod2 += Convert.ToString(novatr) + ",";
                    Conexao.WriteINI("Conta", "TR", salvar_cod2, @".\Contas\" + nome2 + ".ini");
                }

                //Informar tudo OK
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("transferir/2");
                swSenderSender.Flush();
                swSenderSender = null;

                //Autenticar user [1]
                Autenticar(nome);

                //Autenticar user [2] - se estiver online aunteticar ele também
                ICollection MyKeys2;
                int count2 = 0;               //Procurar
                MyKeys2 = htUsuarios.Keys;
                bool online = false;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == nome2)
                    {
                        online = true;
                        break;
                    }
                }

                if (online)
                {
                    Autenticar(nome2);
                }

            }
            else
            {
                //Enviar
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("transferir/1");
                swSenderSender.Flush();
                swSenderSender = null;
            }
        }

        //Ver transações
        public static void VerTR(string nome, string tr)
        {
            //SENDER-SENDER PROBLEM
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            // Exibe primeiro na aplicação
            e = new StatusChangedEventArgs(">> VER TR <<\n"+nome+"-"+tr);
            OnStatusChanged(e);

            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if (Key.ToString() == nome)
                {
                    break;
                }
            }

            //Pegar os valores
            string nome_enviou = Conexao.ReadIni("TR", "Nome", @".\TR\" + tr + ".ini");
            string para = Conexao.ReadIni("TR", "Para", @".\TR\" + tr + ".ini");
            string quantia = Conexao.ReadIni("TR", "Quantia", @".\TR\" + tr + ".ini");
            string data = Conexao.ReadIni("TR", "Data", @".\TR\" + tr + ".ini");

            //Enviar
            swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
            swSenderSender.WriteLine("vertr/"+nome_enviou+"/"+para + "/" +quantia + "/" + data);
            swSenderSender.Flush();
            swSenderSender = null;
        }

        // Autenticar certo jogador
        public static void Autenticar(string mensagem){
            //SENDER-SENDER PROBLEM
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            // Exibe primeiro na aplicação
            e = new StatusChangedEventArgs("Autenticação do user: " + mensagem);
            OnStatusChanged(e);

            ICollection MyKeys;
            int count = 0;
            bool online = false;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if (Key.ToString() == mensagem)
                {
                    online = true;
                    break;
                }
            }

            if(online == true)
            {
                //Autenticar
                string money = Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + mensagem + ".ini");
                string icone = Conexao.ReadIni("Conta", "Foto", @".\Contas\" + mensagem + ".ini");
                string TA = Conexao.ReadIni("Conta", "TA", @".\Contas\" + mensagem + ".ini");
                string TG = Conexao.ReadIni("Conta", "TG", @".\Contas\" + mensagem + ".ini");

                //Enviar
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("Autenticar/" + money + "/" + mensagem + "/" + icone + "/" + TA + "/" + TG);
                swSenderSender.Flush();
                swSenderSender = null;
            }

        }

        // Enviar para certo jogador
        public static void EnviarPrivado(string mensagem)
        {
            string[] quebrar = mensagem.Split('/');
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            // Exibe primeiro na aplicação
            e = new StatusChangedEventArgs(quebrar[1] + "[PM] para " + quebrar[2] + " a mensagem " + quebrar[3]);
            OnStatusChanged(e);

            // 0 -> COD
            // 1 -> Nome de quem enviou
            // 2 -> Nome de quem vai receber
            // 3 -> Quantidade

            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if(Key.ToString() == quebrar[2])
                {
                    break;
                }
            }

            //Enviar
            swSenderSender = new StreamWriter(tcpClientes[count-1].GetStream());
            swSenderSender.WriteLine("privado/"+quebrar[1]+"/"+quebrar[3]);
            swSenderSender.Flush();
            swSenderSender = null;
               
        }

        public static void Acontecimentos(string mensagem, string nome = "NULL")
        {
            StreamWriter swSenderSender;
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            if (nome == "NULL")
            {
                for (int i = 0; i < tcpClientes.Length; i++)
                {
                    // Tenta enviar uma mensagem para cada cliente
                    try
                    {
                        // Se a mensagem estiver em branco ou a conexão for nula sai...
                        if (tcpClientes[i] == null)
                        {
                            continue;
                        }
                        // Envia a mensagem para o usuário atual no laço
                        swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                        swSenderSender.WriteLine("noticia/" + mensagem);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    catch // Se houver um problema , o usuário não existe , então remove-o
                    {
                        RemoveUsuario(tcpClientes[i]);
                    }
                }
            }
            else
            {
                bool permitir = false;
                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs("TESTANDO >> "+Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == nome)
                    {
                        permitir = true;
                        break;
                    }
                }

                if (permitir)
                {
                    // Envia a mensagem para o usuário atual no laço
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("noticia/" + mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
            }
        }

        public static int[] bubbleSort(int[] vetor)
        {
            int tamanho = vetor.Length;
            int comparacoes = 0;
            int trocas = 0;

            for (int i = tamanho - 1; i >= 1; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    comparacoes++;
                    if (vetor[j] > vetor[j + 1])
                    {
                        int aux = vetor[j];
                        vetor[j] = vetor[j + 1];
                        vetor[j + 1] = aux;
                        trocas++;
                    }
                }
            }

            return vetor;
        }

        // -------- Rank --------
        public static void Rank(string nome)
        {
            string pegar = "";

            // manipular de diretorios
            DirectoryInfo dirInfo = new DirectoryInfo(@".\Contas\ ");

            // procurar arquivos
            pegar = BuscaArquivos(dirInfo, 1).Replace("/", ",");

            //Criar a lista
            int[] lista = new int[pegar.Split(',').Length - 1];
            Hashtable tributo = new Hashtable();

            for (int a = 0; a < pegar.Split(',').Length-1; a++) //-1 tira o ultimo que é void
            {
                lista[a] = Convert.ToInt32(Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\"+ pegar.Split(',')[a] + ".ini"));
                tributo[Convert.ToInt32(lista[a])] = Convert.ToString(pegar.Split(',')[a]);
            }

            lista = bubbleSort(lista);

            //Criar posição
            string first = "";
            string second = "";
            string three = "";

            first  = tributo[lista[lista.Length - 1]] + "," + Convert.ToString(lista[lista.Length - 1]);
            second = tributo[lista[lista.Length - 2]] + "," + Convert.ToString(lista[lista.Length - 2]);
            three  = tributo[lista[lista.Length - 3]] + "," + Convert.ToString(lista[lista.Length - 3]);

            e = new StatusChangedEventArgs(" Vetor >> " + first + "||" + second + "||" + three);
            OnStatusChanged(e);

            // ----  SEND TO CLIENT ----
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            bool permitir = false;
            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                if (Key.ToString() == nome)
                {
                    permitir = true;
                    break;
                }
            }

            if (permitir)
            {
                // Envia a mensagem para o usuário atual no laço
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("rank/"+first+"/"+second+"/"+three);
                swSenderSender.Flush();
                swSenderSender = null;
            }
        }
        // ----------------------

        // ----- Mensagens ----- 
        public static void Mensagens(string nome, string nome_msg, string msg)
        {
            if (nome == "@ALLUSERS0101")
            {
                string diretorio = @".\Mensagens\" + nome_msg + ".ini";
                string pegar = "";

                // manipular de diretorios
                DirectoryInfo dirInfo = new DirectoryInfo(@".\Contas\ ");

                // procurar arquivos
                pegar = BuscaArquivos(dirInfo, 1).Replace("/", ",");

                e = new StatusChangedEventArgs("\n\n" + pegar);
                OnStatusChanged(e);

                for (int a = 0; a < pegar.Split(',').Length-1; a++)
                {
                    string[] teste_msgs = Conexao.ReadIni("Conta", "Mensagens", @".\Contas\" + pegar.Split(',')[a] + ".ini").Split(',');
                    string salvar_cod = "";
                    bool exist = false;

                    //Salvar a mensangens na conta
                    if (Conexao.ReadIni("Conta", "Mensagens", @".\Contas\" + pegar.Split(',')[a] + ".ini") == "")
                    {
                        Conexao.WriteINI("Conta", "Mensagens", nome_msg + ",", @".\Contas\" + pegar.Split(',')[a] + ".ini");
                        Acontecimentos("Você recebeu uma mensagem", pegar.Split(',')[a]);
                    }
                    else
                    {
                        for (int c = 0; c < teste_msgs.Length - 1; c++)
                        {
                            // Primeiro exibe a mensagem na aplicação
                            e = new StatusChangedEventArgs(teste_msgs[c]);
                            OnStatusChanged(e);
                            if (teste_msgs[c] == nome_msg)
                            {
                                exist = true;
                                break;
                            }
                            salvar_cod += teste_msgs[c] + ",";
                        }

                        if (exist)
                        {
                            //passa
                        }
                        else
                        {
                            salvar_cod += nome_msg + ",";
                            Conexao.WriteINI("Conta", "Mensagens", salvar_cod, @".\Contas\" + pegar.Split(',')[a] + ".ini");

                            Acontecimentos("Você recebeu uma mensagem", pegar.Split(',')[a]);
                        }
                    }

                    e = new StatusChangedEventArgs("AQUI CARALHO >> " + pegar.Split(',')[a]);
                    OnStatusChanged(e);

                    //Salva a mensagem e/ou muda a mensagem que já existe
                    Conexao.WriteINI("Mensagem", "Texto", msg, diretorio);
                    //Acontecimentos("Você recebeu uma mensagem", pegar.Split(',')[a]);
                }
            }
            else
            {
                string[] teste_msgs = Conexao.ReadIni("Conta", "Mensagens", @".\Contas\" + nome + ".ini").Split(',');
                string diretorio = @".\Mensagens\" + nome_msg + ".ini";
                string salvar_cod = "";
                bool exist = false;

                //Salvar a mensangens na conta
                if (Conexao.ReadIni("Conta", "Mensagens", @".\Contas\" + nome + ".ini") == "")
                {
                    Conexao.WriteINI("Conta", "Mensagens", nome_msg + ",", @".\Contas\" + nome + ".ini");
                    Acontecimentos("Você recebeu uma mensagem", nome);
                }
                else
                {
                    for (int c = 0; c < teste_msgs.Length - 1; c++)
                    {
                        // Primeiro exibe a mensagem na aplicação
                        e = new StatusChangedEventArgs(teste_msgs[c]);
                        OnStatusChanged(e);
                        if (teste_msgs[c] == nome_msg)
                        {
                            exist = true;
                            break;
                        }
                        salvar_cod += teste_msgs[c] + ",";
                    }

                    if (exist)
                    {
                        //passa
                    }
                    else
                    {
                        salvar_cod += nome_msg + ",";
                        Conexao.WriteINI("Conta", "Mensagens", salvar_cod, @".\Contas\" + nome + ".ini");

                        Acontecimentos("Você recebeu uma mensagem", nome);
                    }
                }

                //Salva a mensagem e/ou muda a mensagem que já existe
                Conexao.WriteINI("Mensagem", "Texto", msg, diretorio); //Salva a mensagem
                //Acontecimentos("Você recebeu uma mensagem", nome);
            }
        }

        public static void ConsultarMensagens(string nome_msg, string nome)
        {
            string diretorio = @".\Mensagens\" + nome_msg + ".ini";
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if (Key.ToString() == nome)
                {   
                    break;
                }
            }

            e = new StatusChangedEventArgs(Conexao.ReadIni("Mensagem", "Texto", diretorio));
            OnStatusChanged(e);

            swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
            swSenderSender.WriteLine("conmensagem/" + Conexao.ReadIni("Mensagem", "Texto", diretorio));
            swSenderSender.Flush();
        }

        public static void DeletarMensagem(string nome, string nome_msg)
        {
            string diretorio = @".\Contas\"+nome+".ini";
            string[] quebrar = Conexao.ReadIni("Conta", "Mensagens", diretorio).Split(',');
            string new_msg = "";

            for (int a = 0; a < quebrar.Length-1; a++)
            {
                if (quebrar[a] == nome_msg)
                {
                    quebrar = quebrar.Where(w => w != quebrar[a]).ToArray();
                }
            }

            for (int b = 0; b < quebrar.Length-1; b++)
            {
                new_msg = new_msg + quebrar[b] + ",";
            }

            e = new StatusChangedEventArgs(new_msg);
            OnStatusChanged(e);
            Conexao.WriteINI("Conta", "Mensagens", new_msg, diretorio);
            AttMensagens(nome);
        }

        public static void AttMensagens(string nome)
        {
            string diretorio = @".\Contas\" + nome + ".ini";
            bool permitir = false;
            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if (Key.ToString() == nome)
                {
                    permitir = true;
                    break;
                }
            }

            if (permitir)
            {
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("mensagem/" + Conexao.ReadIni("Conta", "Mensagens", diretorio));
                swSenderSender.Flush();
            }
        }

        // -----  FIM MENSAGENS ----- 

        // Envia mensagens administratias
        public static void EnviaMensagemAdmin(string Mensagem)
        {
            StreamWriter swSenderSender;

            // Exibe primeiro na aplicação
            e = new StatusChangedEventArgs("Administrador: " + Mensagem);
            OnStatusChanged(e);

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
            // Percorre a lista de clientes TCP
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                // Tenta enviar uma mensagem para cada cliente
                try
                {
                    // Se a mensagem estiver em branco ou a conexão for nula sai...
                    if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                    {
                        continue;
                    }

                    // Envia a mensagem para o usuário atual no laço
                    swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine("Administrador: " + Mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // Se houver um problema , o usuário não existe , então remove-o
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }
        }

        //Poker BETA
        public static string[] sala1 = new string[9];
        public static string[] sala2 = new string[9];
        public static string[] sala3 = new string[9];
        public static string[] sala4 = new string[9];
        public static string[] sala5 = new string[9];
        public static string[] sala6 = new string[9];
        public static string[] sala7 = new string[9];
        public static string[] sala8 = new string[9];

        //Baralho
        public static int[] baralho = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
                                        14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
                                        26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37,
                                        38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
                                        50, 51, 52};

        public static int[] baralho1= { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
                                        14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
                                        26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37,
                                        38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
                                        50, 51, 52};

        /*public static int[] RandCartas()
        {
            var rand = new Random();
            int[] valores ;
            int count = 0;

            //Cartas dos jogadores
            for (int a = 0; a < sala1.Length; a++)
            {
                if (sala1[a] != null)
                {
                    count++;
                }
            }

            for (int b = 0; b < count; b++)
            {
                //Primeira carta
                rand.Next(sala1.Length + 1);


            }

            return valores;
        }*/

        public static void DistribuirCartas(int sala)
        {
            int contador = 0;

            switch (sala)
            {
                case 1:
                    //Verificar quantos jogadores
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala1[a] != null)
                        {
                            contador++;
                        }
                    }

                    //
                    break;
            }
        }

        public static void ApostarMesa(string nome, string dinheiro, int sala)
        {
            string poker = Environment.CurrentDirectory + @"\Poker\" + Convert.ToString(sala) + ".ini";
            string jogador = Environment.CurrentDirectory + @"\Contas\" + nome + ".ini";
            string salvar_cod = "";
            string salvar_cod2 = "";
            string[] teste_codigos1 = Conexao.ReadIni("Sala", "Index", poker).Split(',');
            string[] teste_codigos2 = Conexao.ReadIni("Sala", "Aposta", poker).Split(',');
            int novo_dinheiro = Convert.ToInt32(Conexao.ReadIni("Conta", "Dinheiro", jogador)) - Convert.ToInt32(dinheiro);
            int novo_pote = Convert.ToInt32(Conexao.ReadIni("Sala", "Pote", poker)) + Convert.ToInt32(dinheiro);

            //Descontar do jogador
            Conexao.WriteINI("Conta", "Dinheiro", Convert.ToString(novo_dinheiro), jogador);

            //Definir index
            if (Conexao.ReadIni("Sala", "Index", poker) == "")
            {
                Conexao.WriteINI("Sala", "Index", nome + ",", poker);
            }
            else
            {
                for (int c = 0; c < teste_codigos1.Length - 1; c++)
                {
                    // Primeiro exibe a mensagem na aplicação
                    e = new StatusChangedEventArgs(teste_codigos1[c]);
                    OnStatusChanged(e);
                    salvar_cod += teste_codigos1[c] + ",";
                }

                salvar_cod += nome + ",";
                Conexao.WriteINI("Sala", "Index", salvar_cod, poker);
            }

            //Definir aposta
            if (Conexao.ReadIni("Sala", "Aposta", poker) == "")
            {
                Conexao.WriteINI("Sala", "Aposta", dinheiro + ",", poker);
            }
            else
            {
                for (int c = 0; c < teste_codigos2.Length - 1; c++)
                {
                    // Primeiro exibe a mensagem na aplicação
                    e = new StatusChangedEventArgs(teste_codigos2[c]);
                    OnStatusChanged(e);
                    salvar_cod2 += teste_codigos2[c] + ",";
                }

                salvar_cod2 += dinheiro + ",";
                Conexao.WriteINI("Conta", "Codigos", salvar_cod2, poker);
            }

            //Definir pote
            Conexao.WriteINI("Sala", "Pote", Convert.ToString(novo_pote), poker);

            //Att
            DadosMesa(nome, sala);
        }

        public static void DadosMesa(string nome, int sala)
        {
            string poker = Environment.CurrentDirectory + @"\Poker\"+Convert.ToString(sala)+".ini";
            string pote, index, aposta, status, dealer, blind;
            bool permitir = false;

            /*
            * Pote
             * Index
            * Aposta
             * Status
            * Dealer
             * Blind
            */

            pote = Conexao.ReadIni("Sala", "Pote", poker);
            index = Conexao.ReadIni("Sala", "Index", poker);
            aposta = Conexao.ReadIni("Sala", "Aposta", poker);
            status = Conexao.ReadIni("Sala", "Status", poker);
            dealer = Conexao.ReadIni("Sala", "Dealer", poker);
            blind = Conexao.ReadIni("Sala", "Blind", poker);

            StreamWriter swSenderSender;

            // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            ICollection MyKeys;
            int count = 0;
            MyKeys = htUsuarios.Keys;

            foreach (object Key in MyKeys)
            {
                count++;
                e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                OnStatusChanged(e);
                if (Key.ToString() == nome)
                {
                    permitir = true;
                    break;
                }
            }

            if (permitir)
            {
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("infosala/"+ pote + "/" + index + "/" + aposta + "/" + status + "/" + dealer + "/" +  blind);
                swSenderSender.Flush();
            }
        }

        public static void SairMesa(string nome, int sala)
        {
            switch (sala)
            {
                case 1:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala1[a] == nome)
                        {
                            sala1[a] = null;
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int b = 0; b < 9; b++)
                    {
                        if (sala2[b] == nome)
                        {
                            sala2[b] = null;
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int c = 0; c < 9; c++)
                    {
                        if (sala3[c] == nome)
                        {
                            sala3[c] = null;
                            break;
                        }
                    }
                    break;
                case 4:
                    for (int d = 0; d < 9; d++)
                    {
                        if (sala4[d] == nome)
                        {
                            sala4[d] = null;
                            break;
                        }
                    }
                    break;
                case 5:
                    for (int e = 0; e < 9; e++)
                    {
                        if (sala5[e] == nome)
                        {
                            sala5[e] = null;
                            break;
                        }
                    }
                    break;
                case 6:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala6[a] == nome)
                        {
                            sala6[a] = null;
                            break;
                        }
                    }
                    break;
                case 7:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala7[a] == nome)
                        {
                            sala7[a] = null;
                            break;
                        }
                    }
                    break;
                case 8:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala8[a] == nome)
                        {
                            sala8[a] = null;
                            break;
                        }
                    }
                    break;
            }
        }

        public static void EntrarMesa(string nome, int sala)
        {
            switch (sala)
            {
                case 1:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala1[a] == null)
                        {
                            sala1[a] = nome;
                            break;
                        }
                    }
                    break;
                case 2:
                    for (int b = 0; b < 9; b++)
                    {
                        if (sala2[b] == null)
                        {
                            sala2[b] = nome;
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int c = 0; c < 9; c++)
                    {
                        if (sala3[c] == null)
                        {
                            sala3[c] = nome;
                            break;
                        }
                    }
                    break;
                case 4:
                    for (int d = 0; d < 9; d++)
                    {
                        if (sala4[d] == null)
                        {
                            sala4[d] = nome;
                            break;
                        }
                    }
                    break;
                case 5:
                    for (int e = 0; e < 9; e++)
                    {
                        if (sala5[e] == null)
                        {
                            sala5[e] = nome;
                            break;
                        }
                    }
                    break;
                case 6:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala6[a] == null)
                        {
                            sala6[a] = nome;
                            break;
                        }
                    }
                    break;
                case 7:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala7[a] == null)
                        {
                            sala7[a] = nome;
                            break;
                        }
                    }
                    break;
                case 8:
                    for (int a = 0; a < 9; a++)
                    {
                        if (sala8[a] == null)
                        {
                            sala8[a] = nome;
                            break;
                        }
                    }
                    break;
            }
        }

        // Envia mensagens de um usuário para todos os outros
        public static void EnviaMensagem(string Origem, string Mensagem)
        {
            string[] texto = Origem.Split('/');
            string[] teste = Mensagem.Split('/');
            //CODIGO
            string dinheiro = "";
            int valor1 = 0;
            int valor2 = 0;

            StreamWriter swSenderSender;

            // Primeiro exibe a mensagem na aplicação
            e = new StatusChangedEventArgs(texto[0] + " disse : " + Mensagem);
            OnStatusChanged(e);

            if (teste[0] == "privado")
            {
                ChatServidor.EnviarPrivado(Mensagem);
            } else if (teste[0] == "chat")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
                // Percorre a lista de clientes TCP
                for (int i = 0; i < tcpClientes.Length; i++)
                {
                    // Tenta enviar uma mensagem para cada cliente
                    try
                    {
                        // Se a mensagem estiver em branco ou a conexão for nula sai...
                        if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                        {
                            continue;
                        }
                        // Envia a mensagem para o usuário atual no laço
                        swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                        swSenderSender.WriteLine("CHAT/" + texto[0] + "/" + teste[1]);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    catch // Se houver um problema , o usuário não existe , então remove-o
                    {
                        RemoveUsuario(tcpClientes[i]);
                    }
                }
            } else if (teste[0] == "autenticar")
            {
                Autenticar(teste[1]);
            } else if (teste[0] == "suporte")
            {
                int sup = 0;
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> SUPORTE <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == teste[1])
                    {
                        break;
                    }
                }

                // 0 - Nome
                // 3 - Assunto
                // 4 - Mensagem
                // 5 - Data
                DirectoryInfo dirInfo = new DirectoryInfo(@".\Suporte\ ");
                sup = Convert.ToInt32(ChatServidor.BuscaArquivos(dirInfo)) + 1;

                Conexao.WriteINI("Suporte", "Nome", teste[3], @".\Suporte\" + sup + ".ini");
                Conexao.WriteINI("Suporte", "Assunto", teste[1], @".\Suporte\" + sup + ".ini");
                Conexao.WriteINI("Suporte", "Mensagem", teste[2], @".\Suporte\" + sup + ".ini");
                Conexao.WriteINI("Suporte", "Data", Convert.ToString(DateTime.Now), @".\Suporte\" + sup + ".ini");

                // 7 => mensagem enviada
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("suporte/7");
                swSenderSender.Flush();

            } else if (teste[0] == "corridas")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> CORRIDA <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == teste[1])
                    {
                        break;
                    }
                }

                // Corridas
                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Black Edition/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Black Edition.ini") + "/" + Program.abrir.label1.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Fossa Party/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Fossa Party.ini") + "/" + Program.abrir.label2.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/PlayBoy Only/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\PlayBoy Only.ini") + "/" + Program.abrir.label3.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Deluxe/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Deluxe.ini") + "/" + Program.abrir.label4.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Legend/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Legend.ini") + "/" + Program.abrir.label5.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/United/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\United.ini") + "/" + Program.abrir.label6.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Rushback/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Rushback.ini") + "/" + Program.abrir.label7.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Never/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Never.ini") + "/" + Program.abrir.label8.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Vortex/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Vortex.ini") + "/" + Program.abrir.label9.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Hunted/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Hunted.ini") + "/" + Program.abrir.label10.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Blood/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Blood.ini") + "/" + Program.abrir.label11.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/River/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\River.ini") + "/" + Program.abrir.label12.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Flawless/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Flawless.ini") + "/" + Program.abrir.label13.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Destiny/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Destiny.ini") + "/" + Program.abrir.label14.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Fun/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Fun.ini") + "/" + Program.abrir.label15.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Chaos/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Chaos.ini") + "/" + Program.abrir.label16.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Die/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Die.ini") + "/" + Program.abrir.label17.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Sacrifice/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Sacrifice.ini") + "/" + Program.abrir.label18.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Heaven/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Heaven.ini") + "/" + Program.abrir.label19.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Law/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Law.ini") + "/" + Program.abrir.label20.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Dragons/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Dragons.ini") + "/" + Program.abrir.label21.Text);
                swSenderSender.Flush();

                swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                swSenderSender.WriteLine("corridas/Soul/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Soul.ini") + "/" + Program.abrir.label22.Text);
                swSenderSender.Flush();


            } else if (teste[0] == "admadd")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> ADM ADICIONAR <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == teste[3])
                    {
                        break;
                    }
                }

                if (File.Exists(@".\Contas\" + teste[1] + ".ini"))
                {
                    AumentarSaldo(teste[1], teste[2]);

                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("adm/Você adicionou " + teste[2] + " de dinheiro para " + teste[1]);
                    swSenderSender.Flush();
                }
                else
                {
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("adm/Conta não existe");
                    swSenderSender.Flush();
                }
            }
            else if (teste[0] == "kick") {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> KICK <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys)
                {
                    count++;
                    if (Key.ToString() == teste[1])
                    {
                        permitir = true;
                        break;
                    }
                }

                ICollection MyKeys2;
                int count2 = 0;
                MyKeys2 = htUsuarios.Keys;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    if (Key.ToString() == teste[2])
                    {
                        break;
                    }
                }

                if (permitir)
                {
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("kick/");
                    swSenderSender.Flush();

                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Usuário foi expulso");
                    swSenderSender.Flush();
                }
                else
                {
                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Usuário não esta online");
                    swSenderSender.Flush();
                }
            } else if (teste[0] == "trademsg")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> TRADE <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys)
                {
                    count++;
                    if (Key.ToString() == teste[3])
                    {
                        permitir = true;
                        break;
                    }
                }
            }else if (teste[0] == "trade") {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> TRADE << >>Nome de quem ta enviando>> " +teste[1]+"/Dinheiro>> "+teste[2]+"|||Quem vai receber> "+ teste[3]);
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys)
                {
                    count++;
                    if (Key.ToString() == teste[3])
                    {
                        permitir = true;
                        break;
                    }
                }

                if (permitir)
                {
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("trade/perguntar/"+teste[2]+"/"+teste[1]);
                    swSenderSender.Flush();
                }
                else
                {
                    e = new StatusChangedEventArgs(">> TRADE SAIU << ");
                    OnStatusChanged(e); 

                    ICollection MyKeys2;
                    int count2 = 0;
                    MyKeys2 = htUsuarios.Keys;
                    bool permitir2 = false;

                    foreach (object Key in MyKeys2)
                    {
                        count2++;
                        if (Key.ToString() == teste[1])
                        {
                            permitir2 = true;
                            break;
                        }
                    }

                    if (permitir2)
                    {
                        swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                        swSenderSender.WriteLine("trade/saiu");
                        swSenderSender.Flush();
                    }
                }
            }else if (teste[0] == "traderesposta")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> TRADE RESPOSTA << ");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys)
                {
                    count++;
                    if (Key.ToString() == teste[2])
                    {
                        permitir = true;
                        break;
                    }
                }

                if (permitir)
                {
                    if (teste[1] == "aceitou")
                    {
                        //Remover fundos de quem enviou
                        ChatServidor.DiminuirSaldo(teste[2], teste[4]);

                        //Adicionar fundos
                        ChatServidor.AumentarSaldo(teste[3], teste[4]);

                        e = new StatusChangedEventArgs(">> TRADE RESPOSTA ssss<< "+teste[2]+"///"+teste[3]+"|||||"+teste[4]);
                        OnStatusChanged(e);

                        Autenticar(teste[2]);
                        Autenticar(teste[4]);

                        swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                        swSenderSender.WriteLine("traderesposta/aceitou");
                        swSenderSender.Flush();
                    }
                    else
                    {
                        swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                        swSenderSender.WriteLine("traderesposta/recusou");
                        swSenderSender.Flush();
                    }
                }
            }
            else if (teste[0] == "banido")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> BANIDO <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys)
                {
                    count++;
                    if (Key.ToString() == teste[1])
                    {
                        permitir = true;
                        break;
                    }
                }

                ICollection MyKeys2;
                int count2 = 0;
                MyKeys2 = htUsuarios.Keys;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    if (Key.ToString() == teste[2])
                    {
                        break;
                    }
                }

                if (File.Exists(@".\Contas\" + teste[1] + ".ini"))
                {
                    Conexao.WriteINI("Conta", "Acesso", "0", @".\Contas\" + teste[1] + ".ini");

                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Usuário foi banido");
                    swSenderSender.Flush();
                }
                else
                {
                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Conta não existe");
                    swSenderSender.Flush();
                }

                if (permitir)
                {
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("banido/");
                    swSenderSender.Flush();
                }
            } else if (teste[0] == "desbanir")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> DESBANIDO <<\n\n");
                OnStatusChanged(e);


                ICollection MyKeys2;
                int count2 = 0;
                MyKeys2 = htUsuarios.Keys;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    if (Key.ToString() == teste[2])
                    {
                        break;
                    }
                }

                if (File.Exists(@".\Contas\" + teste[1] + ".ini"))
                {
                    Conexao.WriteINI("Conta", "Acesso", "1", @".\Contas\" + teste[1] + ".ini");

                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Usuário foi desbanido");
                    swSenderSender.Flush();
                }
                else
                {
                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Conta não existe");
                    swSenderSender.Flush();
                }
            }
            else if (teste[0] == "admdeletesup")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> PEGANDO DADOS DO ARQUIVO DO SUPORTE <<\n\n");
                OnStatusChanged(e);

                string diretorio = @".\Suporte\" + teste[1] + ".ini";

                ICollection MyKeys2;
                int count2 = 0;
                MyKeys2 = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    if (Key.ToString() == teste[2])
                    {
                        permitir = true;
                        break;
                    }
                }

                if (permitir)
                {
                    File.Delete(diretorio);

                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("adm/Mensagem do suporte foi deletada");
                    swSenderSender.Flush();

                }
            }
            else if (teste[0] == "admdadossup")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> PEGANDO DADOS DO ARQUIVO DO SUPORTE <<\n\n");
                OnStatusChanged(e);

                string diretorio = @".\Suporte\" + teste[1] + ".ini";

                ICollection MyKeys2;
                int count2 = 0;
                MyKeys2 = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    if (Key.ToString() == teste[2])
                    {
                        permitir = true;
                        break;
                    }
                }

                string nome = Conexao.ReadIni("Suporte", "Nome", diretorio);
                string assunto = Conexao.ReadIni("Suporte", "Assunto", diretorio);
                string mensagem = Conexao.ReadIni("Suporte", "Mensagem", diretorio);
                string data = Conexao.ReadIni("Suporte", "Data", diretorio).Replace("/", ",");

                if (permitir)
                {
                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("admdadossup/" + nome + "/" + data + "/" + assunto + "/" + mensagem);
                    swSenderSender.Flush();
                }
            }
            else if (teste[0] == "rank")
            {
                Rank(teste[1]);
            }
            else if (teste[0] == "admsup")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> PEGANDO ARQUIVOS DO SUPORTE <<\n\n");
                OnStatusChanged(e);

                string pegar = "";

                ICollection MyKeys2;
                int count2 = 0;
                MyKeys2 = htUsuarios.Keys;
                bool permitir = false;

                foreach (object Key in MyKeys2)
                {
                    count2++;
                    if (Key.ToString() == teste[1])
                    {
                        permitir = true;
                        break;
                    }
                }

                if (permitir)
                {
                    // manipular de diretorios
                    DirectoryInfo dirInfo = new DirectoryInfo(@".\Suporte\ ");

                    // procurar arquivos
                    pegar = BuscaArquivos(dirInfo, 1).Replace("/", ",");

                    e = new StatusChangedEventArgs("\n\n" + pegar);
                    OnStatusChanged(e);

                    swSenderSender = new StreamWriter(tcpClientes[count2 - 1].GetStream());
                    swSenderSender.WriteLine("admsup/" + pegar);
                    swSenderSender.Flush();
                }

            }
            else if (teste[0] == "admfall")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // Exibe primeiro na aplicação
                e = new StatusChangedEventArgs(">> ADM DIMINUIR <<\n\n");
                OnStatusChanged(e);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == teste[3])
                    {
                        break;
                    }
                }

                if (File.Exists(@".\Contas\" + teste[1] + ".ini"))
                {
                    DiminuirSaldo(teste[1], teste[2]);

                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("adm/Você diminuiu em " + teste[2] + " de dinheiro do " + teste[1]);
                    swSenderSender.Flush();
                }
                else
                {
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("adm/Conta não existe");
                    swSenderSender.Flush();
                }
            }
            else if (teste[0] == "noticia")
            {
                ChatServidor.Acontecimentos(teste[1]);
            } else if (teste[0] == "mensagem")
            {
                Mensagens(teste[1], teste[2], teste[3]);
            } else if (teste[0] == "conmensagem")
            {
                ConsultarMensagens(teste[1], teste[2]);
            } else if (teste[0] == "attmsg")
            {
                AttMensagens(teste[1]);
            } else if (teste[0] == "deletarmensagem")
            {
                DeletarMensagem(teste[1], teste[2]);
            }else if (teste[0] == "boleto")
            {
                string dado = @".\Dados.ini";
                bool permitir = false;
                string[] data = Convert.ToString(DateTime.Now).Split(' ');
                int novo_documento = Convert.ToInt32(Conexao.ReadIni("Boleto", "Numero", dado))+1;
                Conexao.WriteINI("Boleto", "Numero", Convert.ToString(novo_documento), dado);

                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == teste[1])
                    {
                        permitir = true;
                        break;
                    }
                }

                if (permitir)
                {
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("boleto/" + data[0].Replace("/", ",") + "/" + novo_documento);
                    swSenderSender.Flush();
                }
            }
            else if (teste[0] == "codigo")
            {
                string[] teste_codigos = Conexao.ReadIni("Conta", "Codigos", @".\Contas\" + teste[1] + ".ini").Split(',');
                string salvar_cod = "";
                bool permitir = true;

                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                ICollection MyKeys;
                int count = 0;
                MyKeys = htUsuarios.Keys;

                foreach (object Key in MyKeys)
                {
                    count++;
                    e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                    OnStatusChanged(e);
                    if (Key.ToString() == teste[1])
                    {
                        break;
                    }
                }

                if (File.Exists(@".\Codigos\" + teste[2] + ".ini"))
                {
                    // Exibe primeiro na aplicação
                    e = new StatusChangedEventArgs(">> RESPOTA CÓDIGO <<\n\n");
                    OnStatusChanged(e);

                    for (int b = 0; b < teste_codigos.Length; b++)
                    {
                        if (teste_codigos[b] == teste[2])
                        {
                            permitir = false;
                            // 1 -:> Já utilizou este código
                            swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                            swSenderSender.WriteLine("codigo/1");
                            swSenderSender.Flush();
                            swSenderSender = null;
                        }
                    }

                    if (permitir)
                    {
                        //Aplicar o código
                        valor1 = Convert.ToInt32(Conexao.ReadIni("Conta", "Dinheiro", @".\Contas\" + teste[1] + ".ini"));
                        valor2 = Convert.ToInt32(Conexao.ReadIni("Codigo", "Dinheiro", @".\Codigos\" + teste[2] + ".ini"));
                        dinheiro = Convert.ToString(valor1 + valor2);

                        //Salvar a aplicação do código
                        Conexao.WriteINI("Conta", "Dinheiro", dinheiro, @".\Contas\" + teste[1] + ".ini");
                        if (Conexao.ReadIni("Conta", "Codigos", @".\Contas\" + teste[1] + ".ini") == "")
                        {
                            Conexao.WriteINI("Conta", "Codigos", teste[2] + ",", @".\Contas\" + teste[1] + ".ini");
                        }
                        else
                        {
                            for (int c = 0; c < teste_codigos.Length - 1; c++)
                            {
                                // Primeiro exibe a mensagem na aplicação
                                e = new StatusChangedEventArgs(teste_codigos[c]);
                                OnStatusChanged(e);
                                salvar_cod += teste_codigos[c] + ",";
                            }

                            salvar_cod += teste[2] + ",";
                            Conexao.WriteINI("Conta", "Codigos", salvar_cod, @".\Contas\" + teste[1] + ".ini");
                        }

                        // 2 -:> Código aplicado
                        swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                        swSenderSender.WriteLine("codigo/2");
                        swSenderSender.Flush();
                        swSenderSender = null;

                        //Autenticar
                        Autenticar(teste[1]);
                    }//SE NÃO -:> SÓ PASSA
                }
                else
                {
                    // 3 -:> Código não existe
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("codigo/3");
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
            } else if (teste[0] == "aposta")
            {
                //Nome cavalo dinheiro corrida
                AdicionarApostador(teste[1], teste[2], teste[3], teste[4]);
            } else if (teste[0] == "procurar")
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

                // 1 - Cavalo | 2 - nome do user
                if (File.Exists(@".\Cavalos\" + teste[1] + ".ini"))
                {
                    ICollection MyKeys;
                    int count = 0;
                    MyKeys = htUsuarios.Keys;

                    foreach (object Key in MyKeys)
                    {
                        count++;
                        e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                        OnStatusChanged(e);
                        if (Key.ToString() == teste[2])
                        {
                            break;
                        }
                    }
                    //Corrigir nome
                    string nome = "";

                    if (teste.Length == 2)
                    {
                        string[] separar = teste[1].Split(' ');

                        var upper1 = char.ToUpper(separar[0][0]) + separar[0].Substring(1).ToLower();
                        var upper2 = char.ToUpper(separar[1][0]) + separar[1].Substring(1).ToLower();
                        nome = upper1 + " " + upper2;
                    }
                    else
                    {
                        var upper1 = char.ToUpper(teste[1][0]) + teste[1].Substring(1).ToLower();
                        nome = upper1;
                    }

                    // 3 - nome do cavalo | 4 -  peso do cavalo | 5 - vitorias do cavalo | 6 - foto (Nome do cavalo)
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("procurar/2/" + nome + "/" + Conexao.ReadIni("Cavalo", "Peso", @".\Cavalos\" + teste[1] + ".ini") + "/" + Conexao.ReadIni("Cavalo", "Vitorias", @".\Cavalos\" + teste[1] + ".ini") + "/" + nome);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                else
                {
                    ICollection MyKeys;
                    int count = 0;
                    MyKeys = htUsuarios.Keys;

                    foreach (object Key in MyKeys)
                    {
                        count++;
                        e = new StatusChangedEventArgs(Convert.ToString(count - 1) + Key.ToString());
                        OnStatusChanged(e);
                        if (Key.ToString() == teste[2])
                        {
                            break;
                        }
                    }

                    // 1 -:> Cavalo não existe
                    swSenderSender = new StreamWriter(tcpClientes[count - 1].GetStream());
                    swSenderSender.WriteLine("procurar/1");
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
            }
            else
            {
                // Cria um array de clientes TCPs do tamanho do numero de clientes existentes
                TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
                // Copia os objetos TcpClient no array
                ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
                // Percorre a lista de clientes TCP
                for (int i = 0; i < tcpClientes.Length; i++)
                {
                    // Tenta enviar uma mensagem para cada cliente
                    try
                    {
                        // Se a mensagem estiver em branco ou a conexão for nula sai...
                        if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                        {
                            continue;
                        }
                        // Envia a mensagem para o usuário atual no laço
                        swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                        swSenderSender.WriteLine("Administrador: " + texto[0]);
                        swSenderSender.Flush();
                        swSenderSender = null;
                    }
                    catch // Se houver um problema , o usuário não existe , então remove-o
                    {
                        RemoveUsuario(tcpClientes[i]);
                    }
                }
            }

            /*/ Cria um array de clientes TCPs do tamanho do numero de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[ChatServidor.htUsuarios.Count];
            // Copia os objetos TcpClient no array
            ChatServidor.htUsuarios.Values.CopyTo(tcpClientes, 0);
            // Percorre a lista de clientes TCP
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                // Tenta enviar uma mensagem para cada cliente
                try
                {
                    // Se a mensagem estiver em branco ou a conexão for nula sai...
                    if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                    {
                        continue;
                    }
                    // Envia a mensagem para o usuário atual no laço
                    swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine("Administrador: " + texto[0]);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // Se houver um problema , o usuário não existe , então remove-o
                {
                    RemoveUsuario(tcpClientes[i]);
                }
            }*/
        }

        public void IniciaAtendimento()
        {
            try
            {
                // Pega o IP do primeiro dispostivo da rede
                IPAddress ipaLocal = enderecoIP;
                
                // Cria um objeto TCP listener usando o IP do servidor e porta definidas
                tlsCliente = new TcpListener(ipaLocal, 3389);

                // Inicia o TCP listener e escuta as conexões
                tlsCliente.Start();

                // O laço While verifica se o servidor esta rodando antes de checar as conexões
                ServRodando = true;

                // Inicia uma nova tread que hospeda o listener
                thrListener = new Thread(MantemAtendimento);
                thrListener.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MantemAtendimento()
        {
            // Enquanto o servidor estiver rodando
            while (ServRodando == true)
            {
                // Aceita uma conexão pendente
                tcpCliente = tlsCliente.AcceptTcpClient();
                // Cria uma nova instância da conexão
                Conexao newConnection = new Conexao(tcpCliente);
            }
        }
    }

    // Esta classe trata as conexões, serão tantas quanto as instâncias do usuários conectados
    class Conexao
    {
        TcpClient tcpCliente;
        // A thread que ira enviar a informação para o cliente
        private Thread thrSender;
        private StreamReader srReceptor;
        private StreamWriter swEnviador;
        private string usuarioAtual;
        private string strResposta;

        //Importar dll
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string re, StringBuilder retval, int size, string filePath);
        

        //Ler arquivo
        public static string ReadIni(string section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, String.Empty, sb, 255, path);
            return sb.ToString();
        }

        //Escrever arquivo
        public static void WriteINI(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        // O construtor da classe que que toma a conexão TCP
        public Conexao(TcpClient tcpCon)
        {
            tcpCliente = tcpCon;
            // A thread que aceita o cliente e espera a mensagem
            thrSender = new Thread(AceitaCliente);
            // A thread chama o método AceitaCliente()
            thrSender.Start();
        }

        private void FechaConexao()
        {
            // Fecha os objetos abertos
            tcpCliente.Close();
            srReceptor.Close();
            swEnviador.Close();
        }

        // Ocorre quando um novo cliente é aceito
        private void AceitaCliente()
        {
            srReceptor = new System.IO.StreamReader(tcpCliente.GetStream());
            swEnviador = new System.IO.StreamWriter(tcpCliente.GetStream());
            string[] nome = new string[2];
            string lugar;
            int sup;

            // Lê a informação da conta do cliente
            usuarioAtual = srReceptor.ReadLine();
            nome = usuarioAtual.Split('/');
            lugar = @".\Contas\" + nome[0] + ".ini";

            //Suporte 'IMPORTANTE'
            if (nome[2] == "suporte")
            {
                // 0 - Nome
                // 3 - Assunto
                // 4 - Mensagem
                // 5 - Data
                DirectoryInfo dirInfo = new DirectoryInfo(@".\Suporte\ ");
                sup = Convert.ToInt32(ChatServidor.BuscaArquivos(dirInfo)) + 1;

                Conexao.WriteINI("Suporte", "Nome", nome[0], @".\Suporte\"+ sup +".ini");
                Conexao.WriteINI("Suporte", "Assunto", nome[3], @".\Suporte\" + sup + ".ini");
                Conexao.WriteINI("Suporte", "Mensagem", nome[4], @".\Suporte\" + sup + ".ini");
                Conexao.WriteINI("Suporte", "Data", Convert.ToString(DateTime.Now), @".\Suporte\" + sup + ".ini");

                // 7 => mensagem enviada
                swEnviador.WriteLine("7");
                swEnviador.Flush();
                FechaConexao();
                return;
            }

            //Cadastrar 'IMPORTANTE'
            if(nome[2] == "cadastrar")
            {
                if (File.Exists(lugar))
                {
                    // 5 => conta já cadastrada
                    swEnviador.WriteLine("5");
                    swEnviador.Flush();
                    FechaConexao();
                    return;
                }
                else
                {
                    
                    //Cadastrar .ini
                    WriteINI("Conta", "Senha", nome[1], lugar);
                    WriteINI("Conta", "Dinheiro", "0", lugar);
                    WriteINI("Conta", "Foto", "icon1", lugar);
                    WriteINI("Conta", "TA", "0", lugar);
                    WriteINI("Conta", "TG", "0", lugar);
                    WriteINI("Conta", "Codigos", "", lugar);
                    WriteINI("Conta", "Mensagens", "Bem-Vindo,", lugar);
                    WriteINI("Conta", "Acesso", "1", lugar);

                    //AdicionarRegistro
                    int contas = Convert.ToInt32(Conexao.ReadIni("Dados", "Contas", @".\Dados.ini")) + 1;
                    Conexao.WriteINI("Dados", "Online", Convert.ToString(contas), @".\Dados.ini");

                    // 6 => cadastro concluído
                    swEnviador.WriteLine("6");
                    swEnviador.Flush();
                    FechaConexao();
                    return;
                }
            }

            // temos uma resposta do cliente
            if (usuarioAtual != "")
            {
                if (File.Exists(lugar))
                {
                    if (ReadIni("Conta", "Acesso", lugar) == "0")
                    {
                        // 2 => acesso negado
                        swEnviador.WriteLine("2");
                        swEnviador.Flush();
                        FechaConexao();
                        return;
                    }
                    else
                    {
                        if (nome[1] != ReadIni("Conta", "Senha", @".\Contas\"+nome[0]+".ini"))
                        {
                            // 3 => senha errada
                            swEnviador.WriteLine("3");
                            swEnviador.Flush();
                            FechaConexao();
                            return;
                        }
                        else
                        {
                            if (ChatServidor.htUsuarios.Contains(nome[0]) == true)
                            {
                                // 4 => usuário já está conectado
                                swEnviador.WriteLine("4");
                                swEnviador.Flush();
                                FechaConexao();
                                return;
                            }
                            else
                            {
                                //Autenticar
                                string money       = ReadIni("Conta", "Dinheiro", @".\Contas\" + nome[0] + ".ini");
                                string icone       = ReadIni("Conta", "Foto", @".\Contas\" + nome[0] + ".ini");
                                string TA          = ReadIni("Conta", "TA", @".\Contas\" + nome[0] + ".ini");
                                string TG          = ReadIni("Conta", "TG", @".\Contas\" + nome[0] + ".ini");

                                // 1 => conectou com sucesso
                                swEnviador.WriteLine("1");
                                swEnviador.Flush();

                                // First Authentication
                                swEnviador.WriteLine("Autenticar/"+ money + "/" + nome[0] + "/" + icone + "/" + TA + "/" + TG);
                                swEnviador.Flush();

                                // Criador Noticia
                                if (nome[0] == "Singelo" || nome[0] == "Richardson" || nome[0] == "Giovanni")
                                {
                                    ChatServidor.Acontecimentos("Criador " + nome[0] + " entrou");
                                }

                                // Corridas
                                swEnviador.WriteLine("corridas/Black Edition/"+ ReadIni("Corrida", "Blind", @".\Corridas\Black Edition.ini") + "/" + Program.abrir.label1.Text);
                                swEnviador.Flush();
                                swEnviador.WriteLine("corridas/Fossa Party/" + ReadIni("Corrida", "Blind", @".\Corridas\Fossa Party.ini") +"/" +Program.abrir.label2.Text);
                                swEnviador.Flush();
                                swEnviador.WriteLine("corridas/PlayBoy Only/" + ReadIni("Corrida", "Blind", @".\Corridas\PlayBoy Only.ini") + "/" +  Program.abrir.label3.Text);
                                swEnviador.Flush();
                                //Novos
                                swEnviador.WriteLine("corridas/Deluxe/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Deluxe.ini") + "/" + Program.abrir.label4.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Legend/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Legend.ini") + "/" + Program.abrir.label5.Text);
                                swEnviador.Flush();

                                swEnviador.WriteLine("corridas/United/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\United.ini") + "/" + Program.abrir.label6.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Rushback/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Rushback.ini") + "/" + Program.abrir.label7.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Never/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Never.ini") + "/" + Program.abrir.label8.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Vortex/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Vortex.ini") + "/" + Program.abrir.label9.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Hunted/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Hunted.ini") + "/" + Program.abrir.label10.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Blood/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Blood.ini") + "/" + Program.abrir.label11.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/River/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\River.ini") + "/" + Program.abrir.label12.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Flawless/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Flawless.ini") + "/" + Program.abrir.label13.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Destiny/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Destiny.ini") + "/" + Program.abrir.label14.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Fun/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Fun.ini") + "/" + Program.abrir.label15.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Chaos/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Chaos.ini") + "/" + Program.abrir.label16.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Die/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Die.ini") + "/" + Program.abrir.label17.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Sacrifice/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Sacrifice.ini") + "/" + Program.abrir.label18.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Heaven/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Heaven.ini") + "/" + Program.abrir.label19.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Law/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Law.ini") + "/" + Program.abrir.label20.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Dragons/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Dragons.ini") + "/" + Program.abrir.label21.Text);
                                swEnviador.Flush();
                                
                                swEnviador.WriteLine("corridas/Soul/" + Conexao.ReadIni("Corrida", "Blind", @".\Corridas\Soul.ini") + "/" + Program.abrir.label22.Text);
                                swEnviador.Flush();

                                // Inclui o usuário na hash table e inicia a escuta de suas mensagens
                                ChatServidor.IncluiUsuario(tcpCliente, usuarioAtual);
                            }
                        }
                    }
                }
                else
                {
                    // 0 => significa que a conta não existe
                    swEnviador.WriteLine("0");
                    swEnviador.Flush();
                    FechaConexao();
                    return;
                }
            }
            else
            {
                FechaConexao();
                return;
            }
            //
            try
            {
                // Continua aguardando por uma mensagem do usuário
                while ((strResposta = srReceptor.ReadLine()) != "")
                {
                    // Se for inválido remove-o
                    if (strResposta == null)
                    {
                        ChatServidor.RemoveUsuario(tcpCliente);
                    }
                    else
                    {
                        ChatServidor.EnviaMensagem(nome[0], strResposta);
                        //ChatServidor.EnviaMensagem(usuarioAtual, strResposta);
                    }
                }
            }
            catch
            {
                // Se houve um problema com este usuário desconecta-o
                ChatServidor.RemoveUsuario(tcpCliente);
            }
        }
    }
}
