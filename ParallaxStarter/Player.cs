using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace ParallaxStarter
{
    public class Player : ISprite
    {
        /// <summary>
        /// A spritesheet containing a helicopter image
        /// </summary>
        Texture2D spritesheet;


        /// <summary>
        /// The portion of the spritesheet that is the helicopter
        /// </summary>
        Rectangle sourceRect = new Rectangle
        {
            X = 0,
            Y = 0,
            Width = 131,
            Height = 54
        };

        public Rectangle retSrcRect()
        {
            return sourceRect;
        }

        /// <summary>
        /// The origin of the helicopter sprite
        /// </summary>
        Vector2 origin = new Vector2(66, 1);

        /// <summary>
        /// The angle the helicopter should tilt
        /// </summary>
        float angle = 0;

        /// <summary>
        /// The player's position in the world
        /// </summary>
        public Vector2 Position { get; set; }

        public Game1 game;

        /// <summary>
        /// How fast the player moves
        /// </summary>
        public float Speed { get; set; } = 500;

        /// <summary>
        /// Constructs a player
        /// </summary>
        /// <param name="spritesheet">The player's spritesheet</param>
        public Player(Texture2D spritesheet, Game1 game)
        {
            this.spritesheet = spritesheet;
            this.Position = new Vector2(200, 200);
            this.game = game;
        }

        /// <summary>
        /// Updates the player position based on GamePad or Keyboard input
        /// </summary>
        /// <param name="gameTime">The GameTime object</param>
        public void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;
            
            // Use GamePad for input
            var gamePad = GamePad.GetState(0);

            // The thumbstick value is a vector2 with X & Y between [-1f and 1f] and 0 if no GamePad is available
            direction.X = gamePad.ThumbSticks.Left.X;

            // We need to inverty the Y axis
            direction.Y = -gamePad.ThumbSticks.Left.Y;

            // Override with keyboard input
            var keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A))
            {
                //direction.X -= 1;
            }
            if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D)) 
            {
                //direction.X += 1;
            }
            if(keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W))
            {
                direction.Y -= 1;
                angle = -.5f;
            }
            else if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S))
            {
                direction.Y += 1;
                angle = .5f;
            }
            else angle = 0;

            // Caclulate the tilt of the helicopter
            //angle = 0.5f * direction.X;
           
            // Move the helicopter
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * direction;

            //keep ship on screen
            if (Position.X <= (float)gameTime.TotalGameTime.TotalSeconds * 80 + 70)
            {
                Position = new Vector2((float)gameTime.TotalGameTime.TotalSeconds * 80 + 70, Position.Y);
            }
            if (Position.X >= (float)gameTime.TotalGameTime.TotalSeconds * 80 + 800)
            {
                Position = new Vector2((float)gameTime.TotalGameTime.TotalSeconds * 80 + 800, Position.Y);
            }
            if (Position.Y <= 10) Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * new Vector2(0, 1);
            if (Position.Y >= 440) Position += (float)gameTime.ElapsedGameTime.TotalSeconds * Speed * new Vector2(0, -1);
        }

        /// <summary>
        /// Draws the player sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Render the helicopter, rotating about the rotors
            spriteBatch.Draw(spritesheet, Position, sourceRect, Color.White, angle, origin, 1f, SpriteEffects.None, 0.7f);
        }

    }
}
