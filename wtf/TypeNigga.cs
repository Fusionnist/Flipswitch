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

            keys.Add(new KeyHelper(Keys.A, "A"));
            keys.Add(new KeyHelper(Keys.B, "B"));
            keys.Add(new KeyHelper(Keys.C, "C"));
            keys.Add(new KeyHelper(Keys.D, "D"));
            keys.Add(new KeyHelper(Keys.E, "E"));
            keys.Add(new KeyHelper(Keys.F, "F"));
            keys.Add(new KeyHelper(Keys.G, "G"));
            keys.Add(new KeyHelper(Keys.H, "H"));
            keys.Add(new KeyHelper(Keys.I, "I"));
            keys.Add(new KeyHelper(Keys.J, "J"));
            keys.Add(new KeyHelper(Keys.K, "K"));
            keys.Add(new KeyHelper(Keys.L, "L"));
            keys.Add(new KeyHelper(Keys.M, "M"));
            keys.Add(new KeyHelper(Keys.N, "N"));
            keys.Add(new KeyHelper(Keys.O, "O"));
            keys.Add(new KeyHelper(Keys.P, "P"));
            keys.Add(new KeyHelper(Keys.Q, "Q"));
            keys.Add(new KeyHelper(Keys.R, "R"));
            keys.Add(new KeyHelper(Keys.S, "S"));
            keys.Add(new KeyHelper(Keys.T, "T"));
            keys.Add(new KeyHelper(Keys.U, "U"));
            keys.Add(new KeyHelper(Keys.V, "V"));
            keys.Add(new KeyHelper(Keys.W, "W"));
            keys.Add(new KeyHelper(Keys.X, "XXX"));
            keys.Add(new KeyHelper(Keys.Y, "why"));
            keys.Add(new KeyHelper(Keys.Z, "zee"));
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
