using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wtf
{
    public class Sprite
    {
        public Vector2 pos;
        public CoolTexture tex;
        public FRectangle hb;

        public Sprite(Vector2 a_pos, CoolTexture a_tex)
        {
            pos = a_pos;
            tex = a_tex;
            hb = new FRectangle(pos.X, pos.Y, a_tex.srcRects[a_tex.frameCounter - 1].Width, a_tex.srcRects[a_tex.frameCounter - 1].Height);
        }

        public FRectangle GetHB()
        {
            hb = tex.GetBounds(pos);
            return hb;
        }

        public virtual void Update(float es_)
        {
            tex.Update(es_);
        }

        public virtual void Draw(SpriteBatch sb_)
        {
            tex.Draw(sb_, pos);
        }
    }
}
