using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace wtf
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CoolTexture testTex;
        Entity player;
        PadHelper left, right;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            testTex = new CoolTexture(
                AnimType.Loop,
                Content.Load<Texture2D>("square"),
                new Rectangle[] {new Rectangle(0,0,80,80), new Rectangle(80, 0, 80, 80), new Rectangle(0, 80, 160, 80), },
                3,
                0.1f,
                new Point[] {new Point(40,40),new Point(40,40),new Point(80,40)},
                "lololol");
            left = new PadHelper(Buttons.RightThumbstickLeft);
            right = new PadHelper(Buttons.RightThumbstickRight);
            player = new Entity(new Vector2(300, 300), new CoolTexture[1] { testTex }, 100, 0, 0, new Vector2(100, 10));
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            //initialize all logic
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);

            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            testTex.Update(elapsedSeconds);
            left.Update(GamePad.GetState(PlayerIndex.One));
            right.Update(GamePad.GetState(PlayerIndex.One));
            Vector2 index = Vector2.Zero;
            if (left.isPressesd())
                index.X = -1;
            else if (right.isPressesd())
                index.X = 1;
            player.Move(index, (float)gameTime.ElapsedGameTime.TotalSeconds);
            player.MultMov((float)gameTime.ElapsedGameTime.TotalSeconds);
            player.Update((float)gameTime.ElapsedGameTime.TotalSeconds);


            //! update pipeline !
            //move

            //mult movement

            //collisions

            //update
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
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
    public enum AnimType { Loop, Once }
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
    public class FRectangle{
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
    public class Entity : Sprite
    {
        public CoolTexture[] texes;
        public Vector2 mov, maxMov;
        float xVel, yVel, yVelLoss;
        public Entity(Vector2 a_pos, CoolTexture[] a_texes, float xVel_, float yVel_, float yVelLoss_, Vector2 a_maxMov): base(a_pos, a_texes[0])
        {
            texes = a_texes;
            xVel = xVel_;
            yVel = yVel_;
            yVelLoss = yVelLoss_;
            maxMov = a_maxMov;
        }
        void SelectTexture(string name)
        {
            foreach(CoolTexture ct in texes)
            {
                if(ct.name == name) { tex = ct; }
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