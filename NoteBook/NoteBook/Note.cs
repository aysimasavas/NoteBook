using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NoteBook
{
     class Note
    {
        public Bitmap Image { get; set; }
        public User user { get; set; }
        public string text { get; set; }



        public Note()
        {

        }
        public Note(Bitmap image, User User, string Text)
        {
            image = Image;
            User = user;
            Text = text;

        }


    }
}
