using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private int _menuCounter = 0;
        public override void LoadContent(ContentManager content)
        {
            _title = content.Load<Texture2D>("fishtastic");
            _background = content.Load<Texture2D>("sea");
            _font = content.Load<SpriteFont>("File");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background,Vector2.Zero, Color.White);
            spriteBatch.Draw(_title, new Vector2(1920 / 2-400, 200),null, Color.White,0,Vector2.Zero,5 ,SpriteEffects.None,0);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

    }
}
