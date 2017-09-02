using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wtf
{

    public class PadHelper
    {
        Buttons butt;
        bool wasPressed;
        bool isPressed;

        public PadHelper(Buttons butt_)
        {
            butt = butt_;
        }

        public void Update(GamePadState butts_)
        {
            if (butts_.IsButtonDown(butt))
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
