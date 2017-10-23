using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace wtf
{
    public class TypeNigga
    {
        List<KeyHelper> keys;
        string str;

        public TypeNigga()
        {
            keys = new List<KeyHelper>();
            str = String.Empty;

            keys.Add(new KeyHelper(Keys.NumPad1, "1"));
            keys.Add(new KeyHelper(Keys.NumPad2, "2"));
            keys.Add(new KeyHelper(Keys.NumPad3, "3"));
            keys.Add(new KeyHelper(Keys.NumPad4, "4"));
            keys.Add(new KeyHelper(Keys.NumPad5, "5"));
            keys.Add(new KeyHelper(Keys.NumPad6, "6"));
            keys.Add(new KeyHelper(Keys.NumPad7, "7"));
            keys.Add(new KeyHelper(Keys.NumPad8, "8"));
            keys.Add(new KeyHelper(Keys.NumPad9, "9"));
            keys.Add(new KeyHelper(Keys.NumPad0, "0"));
            keys.Add(new KeyHelper(Keys.Decimal, "."));
        }

        public void Update(KeyboardState kbs)
        {
            foreach(KeyHelper kh in keys)
            {
                kh.Update(kbs);
                if (kh.justPressed()) { str += kh.returnString(); }
            }
        }
        public void Reset()
        {
            str = string.Empty; //dunno the difference between String and string lol
        }

        public string getString()
        {
            return str;
        }
    }
}
