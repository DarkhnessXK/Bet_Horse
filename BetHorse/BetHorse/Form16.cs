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
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form17>().Count() > 0)
            {
                Program.salaForm.Visible = false;
                Program.salaForm.Visible = true;
            }
            else
            {
                Program.salaForm = new Form17();
                Program.salaForm.Visible = false;
                Program.salaForm.Visible = true;
            }
        }
    }
}
