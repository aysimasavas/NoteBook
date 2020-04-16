using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string eMail { get; set; }

        public User()
        {

        }

        public User(string Username, string Password, string Email)
        {
            Username = username;
            Password = password;
            Email = eMail;


        }

    }
}
