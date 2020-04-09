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

namespace NoteBook
{
    public partial class Form2 : Form
    {
        Form3 form3;
        FileManagmet fileManagmet;

        public Form2()
        {
            InitializeComponent();
            
            
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            fileManagmet = new FileManagmet();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.Show();
            this.Hide();
            
          
        }
    }
}
