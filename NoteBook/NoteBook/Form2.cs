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
        private FileManagmet fileManagmet;
        // giriş formundan kullanıcı biligierini almak icin user diye string dizisi kulalancığız ; user public olamlı çünkğ form 1 ve 3 ten erişmemiz gerekiyor
        // user:
        // user[0] -> user id
        // user[1] -> username
        // user[2] -> password
        public string[] user;

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
            //Save butonu tıklanıdğında eğer rich textbox boş değilse dosyayı kaydetmeye başlamalıyız.
            if (richTextBox1.Text != "")
            {
                //şimdi file managment dosyamızı kullanacığız user ile id mizi almıştık
                if (fileManagmet.SaveNoteToDatFile(richTextBox1.Text, user[0])) //burada save etme fonk çağırıyoruz 
                {   //eğer kaydolduysa 
                    MessageBox.Show("Not Başarıyla Kaydedildi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //kaydettikten sonra textboxu temizlememiz gerek çünkü  kullanıcı tekrar basarsa program buga girebilir
                    richTextBox1.Clear();

                }
                else
                {
                    //eğer kaydolmadı ise
                    MessageBox.Show("Not Kaydedilemedi", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form3 = new Form3();
            form3.user = user;
            form3.Show();
            this.Hide();


        }
    }
}
