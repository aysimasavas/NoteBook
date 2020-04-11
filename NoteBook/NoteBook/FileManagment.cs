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
        private FileStream stream;
        //isimlerini buyuk yaptım çünkü const olan değerler genelde her harfi büyük yazılır belli olsun diye
        private const string FILE_PATH = @"c:\NoteBook"; //Kullanıcının c belleğinde NoteBook diye bir kullanıcı klasör açmak için
        private const string USER_FILE_NAME = "users";
        private const string USER_FILE_FORMAT = "txt";
        private const string DATE_FORMAT = "dd-MM-yyyy";
        // burada notları dosyaya kaydeteceğimiz formatı dat olarak belirliyorum çünkü dat dosyaları 1 ve 0 lardan oluşur notlarımızı öyle saklayacağız erişilmesin diye istersek değiştirebiliriz
        private const string NOTE_FILE_FORMAT = "bin";
        public FileManagmet()
        {
            if (Directory.Exists(FILE_PATH)) //Eğer klasör daha önceden varsa 
            {
                CreateFile(USER_FILE_NAME, USER_FILE_FORMAT);
            }
            else//Klasör daha önceden yoksa
                Directory.CreateDirectory(FILE_PATH); //NoteBook diye bir klasör oluşturuyoruz C belleğinde
        }


        /// <summary>
        /// Dosya oluşturmaya yarayan fonksiyon
        /// </summary>
        /// <param name="fileName">Dosya adı  </param>
        /// <param name="app">Dosya Uzantısı txt, csv, doc , vs.</param>
        /// <returns></returns>
        private bool CreateFile(string fileName, string app)
        {
            string path = FILE_PATH + "/" + fileName + "." + app;
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
            string path = FILE_PATH + "/" + fileName + "." + app;
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
            string path = FILE_PATH + "/" + fileName + "." + app;
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
        public bool SaveNoteToDatFile(string note, string userId)
        {

            string dirPath = FILE_PATH + "/" + userId;
            StreamWriter sw;
            if (Directory.Exists(dirPath))
            {
                string path = dirPath + "/" + DateTime.Now.ToString(DATE_FORMAT) + "." + NOTE_FILE_FORMAT;
                try
                {
                    stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    sw = new StreamWriter(stream);
                    sw.Write(StringToBinary(note));
                    sw.Flush();
                    sw.Close();
                    stream.Close();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }

            }
            else
            {
                Directory.CreateDirectory(dirPath);
                SaveNoteToDatFile(note, userId);
                return false;
            }
        }
        /// <summary>
        /// Notu okuyan fonksiyon
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="noteDate"></param>
        /// <returns></returns>
        public string ReadNote(string userid, string noteDate)
        {
            string text = ReadFile(userid + "/" + noteDate, NOTE_FILE_FORMAT);
            return BinaryToString(text);
        }


        //alttaki iki metd bizim için stringi 0,1 haline getircek
        private string StringToBinary(string data)
        {
            string res = String.Empty;
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            foreach (char item in bytes)
            {
                res += Convert.ToString((int)item, 2).PadLeft(8, '0');
            }
            return res;
        }
        private string BinaryToString(string data)
        {


            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.UTF8.GetString(byteList.ToArray());
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
            if (WriteLineFile(userText, USER_FILE_NAME, USER_FILE_FORMAT))
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
            string[] allFile = ReadFile(USER_FILE_NAME, USER_FILE_FORMAT).Split(':');
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

        //ÖNEMLİ NOT şimdilik bu metodu kullanacığız çünkü GetUSer ile aynı ismi taşıyıp override kullanacağım ama şimdilik böyle anlaması daha kolay bitanem
        public string[] GetUserInfo(string username, string password)
        {
            string[] userInfo = new string[3];// 3 çünkü 3 bilgimiz var id , username , password
            string[] allFile = ReadFile(USER_FILE_NAME, USER_FILE_FORMAT).Split(':');
            foreach (var item in allFile)
            {
                if (item != "")
                {
                    userInfo = item.Split(',');
                    if (username == userInfo[1] && password == Derypt(userInfo[2]))
                    {
                        return userInfo;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// ismi girilen klasördeki bütün dosyaları getiren fonksiyon
        /// </summary>
        /// <param name="dirPath">Dosya yolu veya adı</param>
        /// <returns></returns>
        public string[] GetFiles(string dirPath)
        {
            string path = FILE_PATH + "/" + dirPath; //Dosya yolumuzu oluşturduk
            string[] files; //files diye setring dizimi belirledik
            int index = 0; // döngüde index kontrolü için index değişkeni tanımladık
            DirectoryInfo df = new DirectoryInfo(path); // klasörümüzü bulduk
            FileInfo[] fi = df.GetFiles(); //içindeki dosyaları aldık
            files = new string[fi.Length]; // dosya sayısı büyüklüğünde dizimizi oluşturduk
            foreach (var item in fi) //döngü ile atadık
            {
                files[index] = item.Name;
                index++;
            }

            return files;
        }
    }
}
