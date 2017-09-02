using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wtf
{
    public class FRectangle
    {
        float x, y, w, h;
        public FRectangle(float x_, float y_, float w_, float h_)
        {
            w = w_;
            h = h_;
            x = x_;
            y = y_;
        }
        public void Offset(Vector2 o_)
        {
            x += o_.X;
            y += o_.Y;
        }
    }
}
