using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wtf
{
    public class KeyHelper
    {
        Keys key;
        bool wasPressed;
        bool isPressed;

        public KeyHelper(Keys key_)
        {
            key = key_;
        }

        public void Update(KeyboardState kbs_)
        {
            if (kbs_.IsKeyDown(key))
            {
                if (isPressed) { wasPressed = true; }
                if (!isPressed) { isPressed = true; }
            }
            else
            {
                wasPressed = false;
                isPressed = false;
            }
        }
        public bool isPressesd()
        {
            return isPressed;
        }
        public bool justPressed()
        {
            return isPressed && !wasPressed;
        }
    }
}
