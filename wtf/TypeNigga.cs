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

            keys.Add(new KeyHelper(Keys.Decimal, "A"));
            keys.Add(new KeyHelper(Keys.Decimal, "B"));
            keys.Add(new KeyHelper(Keys.Decimal, "C"));
            keys.Add(new KeyHelper(Keys.Decimal, "D"));
            keys.Add(new KeyHelper(Keys.Decimal, "E"));
            keys.Add(new KeyHelper(Keys.Decimal, "F"));
            keys.Add(new KeyHelper(Keys.Decimal, "G"));
            keys.Add(new KeyHelper(Keys.Decimal, "H"));
            keys.Add(new KeyHelper(Keys.Decimal, "I"));
            keys.Add(new KeyHelper(Keys.Decimal, "J"));
            keys.Add(new KeyHelper(Keys.Decimal, "K"));
            keys.Add(new KeyHelper(Keys.Decimal, "L"));
            keys.Add(new KeyHelper(Keys.Decimal, "M"));
            keys.Add(new KeyHelper(Keys.Decimal, "N"));
            keys.Add(new KeyHelper(Keys.Decimal, "O"));
            keys.Add(new KeyHelper(Keys.Decimal, "P"));
            keys.Add(new KeyHelper(Keys.Decimal, "Q"));
            keys.Add(new KeyHelper(Keys.Decimal, "R"));
            keys.Add(new KeyHelper(Keys.Decimal, "S"));
            keys.Add(new KeyHelper(Keys.Decimal, "T"));
            keys.Add(new KeyHelper(Keys.Decimal, "U"));
            keys.Add(new KeyHelper(Keys.Decimal, "V"));
            keys.Add(new KeyHelper(Keys.Decimal, "W"));
            keys.Add(new KeyHelper(Keys.Decimal, "XXX"));
            keys.Add(new KeyHelper(Keys.Decimal, "why"));
            keys.Add(new KeyHelper(Keys.Decimal, "zee"));
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
