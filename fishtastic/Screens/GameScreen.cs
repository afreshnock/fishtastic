using fishtastic.GameObjects;
using fishtastic.Particles;
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
        private float counter = 0;
        private BubbleParticleSystem _bubbles;

        public override void Initialize()
        {
            _bubbles = new BubbleParticleSystem(500);
            _objectList.Add(_bubbles);
        }

        public override void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>("bigsea");
            _hitSound = content.Load<SoundEffect>("hit");
            _font = content.Load<SpriteFont>("File");
            _objectList.Add(new Spawner(new Vector2(300, 2575), ColorArray.RandomColor(), 1.5f));
            _objectList.Add(new Spawner(new Vector2(800, 2750), ColorArray.RandomColor(), 2));
            _objectList.Add(new Spawner(new Vector2(1700, 2550), ColorArray.RandomColor(),  2.5f));
            _objectList.Add(new Spawner(new Vector2(5700, 3550), ColorArray.RandomColor(), 3));
            _objectList.Add(new Spawner(new Vector2(3350, 3200), ColorArray.RandomColor(), 4));
            _objectList.Add(new Spawner(new Vector2(6000, 2550), ColorArray.RandomColor(), 2));
            _player = new Player(new Vector2(1000, 2000), ColorArray.RandomColor(), 1);
            _objectList.Add(_player);

            
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
           
            foreach (var obj in _objectList)
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
            counter +=(float) gameTime.ElapsedGameTime.TotalSeconds;
            if (_player.velocity.Length() > 250 && counter > .05)
            {
                _bubbles.PlaceBubble(_player.posistion);

                counter = 0;
            }
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            
            
            

            float playerX = MathHelper.Clamp(_player.posistion.X, 900, 6500);
            float playerY = MathHelper.Clamp(_player.posistion.Y, 500, 3800);

            float offsetX = 900 - playerX;
            float offsetY = 500 - playerY;

            Matrix transform = Matrix.CreateTranslation(offsetX, offsetY,0);

            spriteBatch.Begin(transformMatrix:transform);
            spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(_font, _jellyCount.ToString(), new Vector2(50, 50), Color.SandyBrown);
            base.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
