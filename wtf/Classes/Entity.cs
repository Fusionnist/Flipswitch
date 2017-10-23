using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace wtf
{
    public class Entity : Sprite
    {
        public CoolTexture[] texes;
        public Vector2 mov, maxMov;
        float xVel, yVel, yVelLoss;
        public Entity(Vector2 a_pos, CoolTexture[] a_texes, float xVel_, float yVel_, float yVelLoss_, Vector2 a_maxMov) : base(a_pos, a_texes[0])
        {
            texes = a_texes;
            xVel = xVel_;
            yVel = yVel_;
            yVelLoss = yVelLoss_;
            maxMov = a_maxMov;
        }
        public void Input(Vector2 i_)
        {
            pos += i_;
        }
        void SelectTexture(string name)
        {
            foreach (CoolTexture ct in texes)
            {
                if (ct.name == name) { tex = ct; }
            }
        }
        public void Move(Vector2 a_input, float a_es)
        {
            ChangeMov(a_input);
        }
        public void MultMov(float es_)
        {
            if (mov.X > maxMov.X) { mov.X = maxMov.X; }
            if (mov.X < -maxMov.X) { mov.X = -maxMov.X; }
            if (mov.Y < -maxMov.Y) { mov.Y = -maxMov.Y; }
            if (mov.Y > maxMov.Y) { mov.Y = maxMov.Y; }
            mov *= es_;
        }
        public void ChangeMov(Vector2 a_input)
        {
            mov.X += xVel * a_input.X;
        }
        public override void Update(float es_)
        {
            pos += mov;
            mov = Vector2.Zero;
            base.Update(es_);
        }
        public override void Draw(SpriteBatch sb_)
        {
            base.Draw(sb_);
        }
    }
}
