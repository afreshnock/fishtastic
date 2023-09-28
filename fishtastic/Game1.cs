using fishtastic.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace fishtastic
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Song _backgroundSong;
        private Screen[] _screenArray= new Screen[] {new MainMenuScreen(), new GameScreen() }; 
        private int _screenCount =0;
        private KeyboardState _current;
        private KeyboardState _prev;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _backgroundSong = Content.Load<Song>("mountain-path");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundSong);
            _screenArray[0].LoadContent(Content);
            _screenArray[1].LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            _prev = _current; 
            _current = Keyboard.GetState();
            if (_prev.IsKeyUp(Keys.Escape) && _current.IsKeyDown(Keys.Escape))
            {
                if(_screenCount == 0)
                {
                    Exit();
                }
                else
                {
                    _screenCount = 0;
                }
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _screenCount = 1;
            }
            _screenArray[_screenCount].Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _screenArray[_screenCount].Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}