using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetHorse
{
    static class Program
    {
        //Instanciar Form's
        public static Form1 loginForm;
        public static Form2 cadastroForm;
        public static Form3 painelForm;
        public static Form4 consultaForm;
        public static Form5 lojaForm;
        public static Form6 codigoForm;
        public static Form7 premioForm;
        public static Form8 admForm;
        public static Form9 supForm;
        public static Form10 msgForm;
        public static Form11 regrasForm;
        public static Form12 rankForm;
        public static Form13 msgshowForm;
        public static Form14 tradeForm;
        public static Form15 configForm;
        //Poker [Beta]
        public static Form16 listaForm;
        public static Form17 salaForm;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loginForm = new Form1();
            Application.Run(loginForm);
        }
    }
}
