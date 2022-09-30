using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BetHorse
{
    class Arquivo
    {
        //Importar dll
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string re, StringBuilder retval, int size, string filePath);

        //Escrever no arquivo
        public static void WriteINI(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        //Ler o arquivo
        public static string ReadIni(string section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, String.Empty, sb, 255, path);
            return sb.ToString();
        }
    }
}
