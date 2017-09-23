using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        Point vdims;
        Rectangle drawDestination;
        int scale; float zoom;
        RenderTarget2D mainTarget;
        Entity player;
        PadHelper left, right;
        OnlineStuff onlineHelper;
        int helperReturn;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 1080 / 2;
            graphics.PreferredBackBufferWidth = 1920 / 2;
            Window.IsBorderless = true;
            graphics.ApplyChanges();
            SetupDrawVariables();
            base.Initialize();
        }
        void SetupDrawVariables()
        {
            vdims = new Point(400, 400);
            mainTarget = new RenderTarget2D(GraphicsDevice, vdims.X, vdims.Y);
            int scalex = GraphicsDevice.Viewport.Width / vdims.X;
            int scaley = GraphicsDevice.Viewport.Height / vdims.Y;
            scale = Math.Min(scalex, scaley);
            drawDestination = new Rectangle(
                (GraphicsDevice.Viewport.Width - (vdims.X * scale))/2,
                (GraphicsDevice.Viewport.Height - (vdims.Y * scale))/2,
                vdims.X * scale,
                vdims.Y * scale);
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
            onlineHelper = new OnlineStuff();
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
            onlineHelper.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            helperReturn = onlineHelper.HandleWebConnections();

            //! update pipeline !
            //move

            //mult movement

            //collisions

            //update
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(mainTarget);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            if (helperReturn == 1)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("square"), new Vector2(200));
            }
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(mainTarget, destinationRectangle: drawDestination);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }   
    public enum AnimType { Loop, Once }
}