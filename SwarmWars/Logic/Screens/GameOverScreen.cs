using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic.Screens
{
    
    public class GameOverScreen : GameScreen
    {

        private Texture2D t2dBackground;

        public GameOverScreen()
        {
            
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            t2dBackground = Resources.Textures["GameoverScreen"];
        }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);

            if (input.Click())
            {
                ExitScreen();
                //ScreenManager.AddScreen(new WorldMapScreen());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
           

            // Fade the popup alpha during transitions.
            Color color = new Color(255, 255, 255, TransitionAlpha);

            spriteBatch.Begin();

            // Draw the background rectangle.          
            spriteBatch.Draw(t2dBackground, new Vector2(Consts.Screen_Width / 2, Consts.Screen_Height / 2), new Rectangle(0, 0, t2dBackground.Width, t2dBackground.Height), color, 0f, new Vector2(t2dBackground.Width / 2, t2dBackground.Height / 2), 1, SpriteEffects.None, 0);            

            spriteBatch.End();
        }
    }
}
