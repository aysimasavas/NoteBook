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
        private FileManagmet FileManagmet;
        // user:
        // user[0] -> user id
        // user[1] -> username
        // user[2] -> password
        public string[] user;
        public Form3()
        {
            InitializeComponent();
            FileManagmet = new FileManagmet();
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

        private void Form3_Load(object sender, EventArgs e)
        {// burada form açılırken dosyayı okuyacağız id göre bunun için burada da  bir user oluşturduk id ye erişmek için
         // sonra file managment nesnesi oluşturuyoruz 
         //şimdi burada comboxı doldurmak için kullanıcı klasörünü okicaz

            comboBox1.Items.AddRange(FileManagmet.GetFiles(user[0]));



        }

        //Comboboxtaki seçim değiştiğinde yapılacak işlemler burda olcak sadece seçilen dosyaya göre içeriği getircez
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = FileManagmet.ReadNote(user[0],comboBox1.SelectedItem.ToString().Substring(0,10)); // uzantıyı almamak için substring ile ilk 10 karakteri aldık
        }
    }
}
