﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallaxStarter
{
    /// <summary>
    /// A class representing a texture to render with a SpriteBatch
    /// </summary>
    class StaticSprite : ISprite
    {
        public Vector2 position = Vector2.Zero;
        Texture2D texture;

        /// <summary>
        /// Creates a new static sprite
        /// </summary>
        /// <param name="texture">The texture to use</param>
        public StaticSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        /// <summary>
        /// Creates a new static sprite
        /// </summary>
        /// <param name="texture">the texture to use</param>
        /// <param name="position">the upper-left hand corner of the sprite</param>
        public StaticSprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        /// <summary>
        /// Draws the sprite using the provided SpriteBatch.  This
        /// method should be invoked between SpriteBatch.Begin() 
        /// and SpriteBatch.End() calls.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
