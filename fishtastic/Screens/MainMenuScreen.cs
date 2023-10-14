using fishtastic.GameObjects;
using fishtastic.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishtastic.Screens
{
    public class MainMenuScreen : Screen
    {
        private Texture2D _title; 
        private Texture2D _background;
        private SpriteFont _font;
        public int menuCounter { get; private set; } = 0;
        private Crab crab;
        private float[] scales = new float[] {1,1,1,1};
        private KeyboardState prev , current;
        private bool scaling = true;
        private BubbleParticleSystem bubbleParticleSystem;
        public override void LoadContent(ContentManager content)
        {
            bubbleParticleSystem = new BubbleParticleSystem(5000);
            _objectList.Add(bubbleParticleSystem);
            _title = content.Load<Texture2D>("fishtastic");
            _background = content.Load<Texture2D>("sea");
            _font = content.Load<SpriteFont>("File");
            crab = new Crab(new Vector2(200, 700), new Vector2(70, 0));
            _objectList.Add(crab);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            if(crab.position.X > 2000)
            {
                crab.position.X = -200;
            }
            prev = current;
            current = Keyboard.GetState();
            if ((prev.IsKeyUp(Keys.Down) && current.IsKeyDown(Keys.Down)) || (prev.IsKeyUp(Keys.S) && current.IsKeyDown(Keys.S)))
            {
                menuCounter++;
                if(menuCounter == 4) menuCounter = 3;
                scales = new float[] { 1, 1, 1, 1 };
            }
            else if((prev.IsKeyUp(Keys.Up) && current.IsKeyDown(Keys.Up)) || (prev.IsKeyUp(Keys.W) && current.IsKeyDown(Keys.W)))
            {
                menuCounter--;
                if (menuCounter == -1) menuCounter = 0;
                scales = new float[] { 1, 1, 1, 1 };
            }
            if(scales[menuCounter] > 1.5) scaling = false; 
            else if (scales[menuCounter] < 1)scaling = true;
            if(scaling) scales[menuCounter] += .005f;
            else scales[menuCounter] -= .005f;
            bubbleParticleSystem.PlaceBubble(new Rectangle(0, 0, 1920, 1080));

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background,Vector2.Zero, Color.White);
            spriteBatch.Draw(_title, new Vector2(200, 100),null, Color.White,0,Vector2.Zero,2 ,SpriteEffects.None,0);
            spriteBatch.DrawString(_font, "Play Game", new Vector2(1920 / 2 - 100, 500), Color.Navy, 0, Vector2.Zero, scales[0],SpriteEffects.None,0);
            spriteBatch.DrawString(_font, "Controls", new Vector2(1920 / 2 - 80, 600), Color.Navy, 0, Vector2.Zero, scales[1], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Credits", new Vector2(1920 / 2 - 70, 700), Color.Navy, 0, Vector2.Zero, scales[2], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Exit", new Vector2(1920 / 2 - 50, 800), Color.Navy, 0, Vector2.Zero, scales[3], SpriteEffects.None, 0);
            
            base.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
