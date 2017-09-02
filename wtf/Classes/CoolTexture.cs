using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wtf
{
    public class CoolTexture
    {
        public string name;
        public AnimType animType;
        public Texture2D src;
        public Rectangle[] srcRects;
        public int frameCount, frameCounter;
        public float frameTime, frameTimer;
        public Point[] centers;
        public bool animEnded;
        public CoolTexture(AnimType at_, Texture2D src_, Rectangle[] srcRects_, int fc_, float ft_, Point[] ct_, string name_)
        {
            name = name_;
            animType = at_;
            src = src_;
            srcRects = srcRects_;
            frameCount = fc_;
            frameCounter = 1;
            frameTime = ft_;
            frameTimer = ft_;
            centers = ct_;
        }
        public void Update(float es_)
        {
            frameTimer -= es_;
            if (frameTimer < 0)
            {
                frameTimer = frameTime;
                frameCounter++;
                if (frameCounter > frameCount)
                {
                    if (animType == AnimType.Loop)
                    {
                        frameCounter = 1;
                    }
                    if (animType == AnimType.Once)
                    {
                        frameCounter = frameCount;
                        animEnded = true;
                    }
                }
            }
        }
        public void Draw(SpriteBatch sb_, Vector2 pos_)
        {
            sb_.Draw(
                texture: src,
                position: pos_ - centers[frameCounter - 1].ToVector2(),
                sourceRectangle: srcRects[frameCounter - 1]);
        }
        public FRectangle GetBounds(Vector2 pos_)
        {
            FRectangle r = new FRectangle(
                pos_.X - centers[frameCounter - 1].X,
                pos_.X - centers[frameCounter - 1].X,
                srcRects[frameCounter - 1].Width,
                srcRects[frameCounter - 1].Height);
            r.Offset(centers[frameCounter - 1].ToVector2());
            return r;
        }
    }
}
