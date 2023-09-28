using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fishtastic.GameObjects
{
    public class Crab : IGameObject
    {
        private float frameTimer=0;

        private int frame;

        public Vector2 position;

        private Vector2 velocity;

        private Texture2D texture;

        public Crab(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("crab");
        }

        public void Update(GameTime gameTime)
        {
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(frameTimer > .1 )
            {
                frame++;
                if(frame > 6)
                {
                    frame = 0;
                }
                frameTimer = 0;
            }
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, new Rectangle(128 * frame, 0, 128, 128), Color.White);
        }
    }
}
