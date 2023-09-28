using fishtastic.GameObjects;
using fishtastic.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace fishtastic.Screens
{
    public class GameScreen : Screen
    {
        private Player _player;
        private Texture2D _background;
        private SoundEffect _hitSound;
        private SpriteFont _font;
        private ushort _jellyCount = 0;



        public override void LoadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("coral");
            Texture2D jellyTexture = content.Load<Texture2D>("jelly2");
            _background = content.Load<Texture2D>("sea");
            _hitSound = content.Load<SoundEffect>("hit");
            _font = content.Load<SpriteFont>("File");
            _objectList.Add(new Spawner(new Vector2(300, 575), ColorArray.RandomColor(), 1.5f));
            _objectList.Add(new Spawner(new Vector2(800, 750), ColorArray.RandomColor(), 2));
            _objectList.Add(new Spawner(new Vector2(1700, 550), ColorArray.RandomColor(),  1));
            _player = new Player(new Vector2(100, 500), ColorArray.RandomColor(), 1);
            _objectList.Add(_player);
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var obj in _objectList)
            {
                if (obj is Spawner s)
                {
                    List<Jelly> trash = new List<Jelly>();
                    foreach (Jelly j in s.jellies)
                    {
                        if (_player.bounds.CollidesWith(j.bounds))
                        {
                            trash.Add(j);
                        }
                    }
                    foreach (Jelly j in trash)
                    {
                        s.jellies.Remove(j);
                        _player.color = j.color;
                        _hitSound.Play();
                        _jellyCount++;
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(_font, _jellyCount.ToString(), new Vector2(50, 50), Color.SandyBrown);
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
