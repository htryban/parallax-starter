using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ParallaxStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;

        Rectangle meteorRect1;
        Rectangle meteorRect2;
        Rectangle meteorRect3;
        Rectangle meteorRect4;
        Rectangle meteorRect5;
        Rectangle playerRect;
        Texture2D rock;
        Random rand = new Random();
        float progression = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            /*
            List<Rectangle> rocks = new List<Rectangle>()
            {
                meteorRect1,
                meteorRect2,
                meteorRect3,
                meteorRect4,
                meteorRect5
            };

            for(int i = 0; i < rocks.Count; i++)
            {
                rocks[i] = 

            }
            */
            meteorRect1.X = rand.Next(900, 1550);
            meteorRect1.Y = rand.Next(0,450);
            meteorRect1.Width = 50;
            meteorRect1.Height = 50;

            meteorRect2.X = rand.Next(900, 1400);
            meteorRect2.Y = rand.Next(0, 450);
            meteorRect2.Width = 50;
            meteorRect2.Height = 50;

            meteorRect3.X = rand.Next(900, 2000);
            meteorRect3.Y = rand.Next(0, 450);
            meteorRect3.Width = 50;
            meteorRect3.Height = 50;

            meteorRect4.X = rand.Next(900, 1300);
            meteorRect4.Y = rand.Next(0, 450);
            meteorRect4.Width = 50;
            meteorRect4.Height = 50;

            meteorRect5.X = rand.Next(900, 1150);
            meteorRect5.Y = rand.Next(0, 450);
            meteorRect5.Width = 50;
            meteorRect5.Height = 50;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var spritesheet = Content.Load<Texture2D>("helicopter");
            player = new Player(spritesheet, this);
            rock = Content.Load<Texture2D>("rock no trail");

            var backgroundTexture = Content.Load<Texture2D>("starBackgroundCropped");
            var backgroundSprite = new StaticSprite(backgroundTexture);
            var backgroundLayer = new ParallaxLayer(this);
            backgroundLayer.Sprites.Add(backgroundSprite);
            backgroundLayer.DrawOrder = 0;
            Components.Add(backgroundLayer);

            var playerLayer = new ParallaxLayer(this);
            playerLayer.Sprites.Add(player);
            playerLayer.DrawOrder = 2;
            Components.Add(playerLayer);

            var midgroundTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("greenMountainForeground"),
                Content.Load<Texture2D>("greenMountainForeground"),
                Content.Load<Texture2D>("greenMountainForeground"),
                Content.Load<Texture2D>("greenMountainForeground"),
                Content.Load<Texture2D>("greenMountainForeground"),
            };

            var midgroundSprites = new StaticSprite[]
            {
                new StaticSprite(midgroundTextures[0], new Vector2(0, 45)),
                new StaticSprite(midgroundTextures[1], new Vector2(2048, 45)),
                new StaticSprite(midgroundTextures[1], new Vector2(2048 * 2, 45)),
                new StaticSprite(midgroundTextures[1], new Vector2(2048 * 3, 45)),
                new StaticSprite(midgroundTextures[1], new Vector2(2048 * 4, 45))
            };

            var midgroundLayer = new ParallaxLayer(this);
            midgroundLayer.Sprites.AddRange(midgroundSprites);
            midgroundLayer.DrawOrder = 1;

            var midgroundScrollController = midgroundLayer.ScrollController as AutoScrollController;
            midgroundScrollController.Speed = 70f;
            Components.Add(midgroundLayer);

            var foregroundTextures = new List<Texture2D>() ;
            for(int i = 0; i < 50; i++) foregroundTextures.Add(Content.Load<Texture2D>("bushes"));
            
            var foregroundSprites = new List<StaticSprite>();
            for (int i = 0; i < foregroundTextures.Count; i++)
            {
                var position = new Vector2(i * 743, 375);
                var sprite = new StaticSprite(foregroundTextures[i], position);
                foregroundSprites.Add(sprite);
            }

            var foregroundLayer = new ParallaxLayer(this);
            foreach (var sprite in foregroundSprites)
            {
                foregroundLayer.Sprites.Add(sprite);
            }

            foregroundLayer.DrawOrder = 4;
            var foregroundScrollController = foregroundLayer.ScrollController as AutoScrollController;
            foregroundScrollController.Speed = 220f;
            Components.Add(foregroundLayer);

            var playerScrollController = playerLayer.ScrollController as AutoScrollController;
            playerScrollController.Speed = 80f;

            //for scrolling with the player
            //backgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.1f);
            //midgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.4f);
            //playerLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            //foregroundLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            //

        }

        public int getBounds()
        {
            var xBound = GraphicsDevice.Viewport.X;
            
            return xBound;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            progression = (float)(gameTime.TotalGameTime.TotalSeconds * .1);
            meteorRect1.X -= (int)(7 + progression * .9);
            meteorRect2.X -= (int)(7 + progression * 1.1);
            meteorRect3.X -= (int)(7 + progression);
            meteorRect4.X -= (int)(7 + progression * .5);
            meteorRect5.X -= (int)(7 + progression * .8);

            float currentX = (float)gameTime.TotalGameTime.TotalSeconds;
            if (meteorRect1.X < currentX - 50)
            {
                meteorRect1.X = rand.Next((int)(currentX + 700), (int)(currentX + 1200));
                meteorRect1.Y = rand.Next(0, 475);
            }
            if (meteorRect2.X < currentX - 50)
            {
                meteorRect2.X = rand.Next((int)(currentX + 700), (int)(currentX + 1200));
                meteorRect2.Y = rand.Next(0, 475);
            }
            if (meteorRect3.X < currentX - 50)
            {
                meteorRect3.X = rand.Next((int)(currentX + 700), (int)(currentX + 1200));
                meteorRect3.Y = rand.Next(0, 475);
            }
            if (meteorRect4.X < currentX - 50)
            {
                meteorRect4.X = rand.Next((int)(currentX + 700), (int)(currentX + 1200));
                meteorRect4.Y = rand.Next(0, 475);
            }
            if (meteorRect5.X < currentX - 50)
            {
                meteorRect5.X = rand.Next((int)(currentX + 700), (int)(currentX + 1200));
                meteorRect5.Y = rand.Next(0, 475);
            }

            playerRect = new Rectangle((int)(float)(gameTime.TotalGameTime.TotalSeconds - 75), (int)player.Position.Y, 131, 54);

            if(playerRect.Intersects(meteorRect1) || playerRect.Intersects(meteorRect2) || playerRect.Intersects(meteorRect3) || playerRect.Intersects(meteorRect4) || playerRect.Intersects(meteorRect5))
            {
                Exit();
            }

            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(rock, meteorRect1, Color.White);
            spriteBatch.Draw(rock, meteorRect2, Color.White);
            spriteBatch.Draw(rock, meteorRect3, Color.White);
            spriteBatch.Draw(rock, meteorRect4, Color.White);
            spriteBatch.Draw(rock, meteorRect5, Color.White);
            spriteBatch.End();

        }
    }
}
