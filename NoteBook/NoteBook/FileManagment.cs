using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class FileManagmet
    {
        private string FilePath = @"c:\NoteBook"; //Kullanıcının c belleğinde NoteBook diye bir kullanıcı klasör açmak için
        private FileStream stream;
        private const string UsersFile = "users";
        private const string UsersFileFormat = "txt";
        public FileManagmet()
        {
            if (Directory.Exists(FilePath)) //Eğer klasör daha önceden varsa 
            {
                CreateFile(UsersFile, UsersFileFormat);
            }
            else//Klasör daha önceden yoksa
                Directory.CreateDirectory(FilePath); //NoteBook diye bir klasör oluşturuyoruz C belleğinde
        }


        /// <summary>
        /// Dosya oluşturmaya yarayan fonksiyon
        /// </summary>
        /// <param name="fileName">Dosya adı  </param>
        /// <param name="app">Dosya Uzantısı txt, csv, doc , vs.</param>
        /// <returns></returns>
        private bool CreateFile(string fileName, string app)
        {
            string path = FilePath + "/" + fileName + "." + app;
            if (!File.Exists(path))
            {

                stream = File.Create(path);
                stream.Close();
                if (File.Exists(path))
                    return true;
                else
                    return false;
            }
            else return true;
        }
        /// <summary>
        /// Bu fonksiyon Bir dosyaya satır satır yazmanı sağlar ve eğer yazdıysa true döner fakat yazmadıysa ve dosya oluşturduysa false döner
        /// </summary>
        /// <param name="text">Bir satırea yazılacak yazı</param>
        /// <param name="fileName">Dosya adı</param>
        /// <param name="app">Dosya Uzantısı txt, csv, doc , vs. </param>
        /// <returns></returns>
        private bool WriteLineFile(string text, string fileName, string app)
        {
            string path = FilePath + "/" + fileName + "." + app;
            StreamWriter sw;
            if (File.Exists(path))
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Write);
                stream.Close();
                // sw = new StreamWriter(stream);
                sw = File.AppendText(path);
                sw.Write(text + ":");
                sw.Flush();
                sw.Close();
                //stream.Close();
                return true;
            }
            else
            {
                CreateFile(fileName, app);
                return false;
            }
        }
        /// <summary>
        /// Bu fonksiyon Dosyadaki bütün texti okur
        /// </summary>
        /// <param name="fileName">Dosya Adı</param>
        /// <param name="app">Dosya Uzantısı txt, csv, doc , vs.</param>
        /// <returns></returns>
        private string ReadFile(string fileName, string app)
        {
            string text = "";
            string path = FilePath + "/" + fileName + "." + app;
            StreamReader sr;
            if (File.Exists(path))
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(stream);
                text = sr.ReadToEnd();
                sr.Close();
                stream.Close();
                return text;
            }
            else
            {
                CreateFile(fileName, app);
                return "Dosya Bulunamadı";
            }
        }

        // Bu iki fonksiyon Kriptolamaya yarıyor biri şifreliyor öbürü şifreyi çözüyor 
        private string Crypt(string text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private string Derypt(string text)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(text);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        /// <summary>
        /// Kişi Kaydetmeye Yarayan Fonksiyon \n alınan kullanıcı adı ve şifreyi random bir id ile kaydeder 
        /// </summary>
        /// <param name="username">Kullanıcı Adı</param>
        /// <param name="password">Şifre</param>
        /// <returns></returns>
        public bool SaveUser(string username, string password)
        {
            //User save de iki kuralımız var açarken kolay olması için birincisi username ve password aynı kullanıcı için , ile ayrılır ve bir de başına random id eklenir kısaca 
            //id,useername,şifrelipassword olur öğrneğin 14523,mert,SAAFD123F131AKS792ASPDADD==
            Random rand = new Random();
            string id = rand.Next(10000, 99999).ToString();
            string userText = id + "," + username + "," + Crypt(password);
            if (WriteLineFile(userText, UsersFile, UsersFileFormat))
                return true;
            else
                return false;
        }
        /// <summary>
        /// User Sorgulamaya yarayan Fonksiyon
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <param name="password">Şifre</param>
        /// <returns></returns>
        public bool GetUser(string username, string password)
        {
            string[] userInfo = new string[3];// 3 çünkü 3 bilgimiz var id , username , password
            string[] allFile = ReadFile(UsersFile, UsersFileFormat).Split(':');
            foreach (var item in allFile)
            {
                if (item != "")
                {
                    userInfo = item.Split(',');
                    if (username == userInfo[1] && password == Derypt(userInfo[2]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
