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
    public partial class Form1 : Form
    {
        Form2 form2;


        private FileManagmet fileManagmet;


        public Form1()
        {
            InitializeComponent();
            form2 = new Form2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Form Loadda Fİle Managment Nesnemin Kurucusunu çağırıyorum
            fileManagmet = new FileManagmet();
        }


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                if (textBox2.Text == textBox3.Text)
                {
                    if (fileManagmet.SaveUser(textBox1.Text, textBox2.Text))

                        label8.Text = "kaydınız başarıyla oluşturuldu";
                    else
                        label8.Text = "kaydınız başarısız \n tekrar deneyin..";
                }
                else
                {
                    MessageBox.Show("şifreler uyusmuyor", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("bosluk bırakmayın", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (fileManagmet.GetUser(textBox5.Text, textBox4.Text))
            {
                form2.Show();

            }
            else
            {
                MessageBox.Show("şifre ve kullanıcı adı uyuşmuyor", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Hide();
        }

 
        public void veriEkle()
        {
            fileManagmet.SaveUser(textBox1.Text, textBox2.Text);
        }
    }
}
