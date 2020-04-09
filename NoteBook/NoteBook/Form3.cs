using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteBook
{
    public partial class Form3 : Form
    {
        Form2 formm2;
        public Form3()
        {
            InitializeComponent();
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formm2 = new Form2();
            formm2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
