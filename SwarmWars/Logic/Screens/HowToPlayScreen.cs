using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic.Screens
{
    public class HowToPlayScreen : GameScreen
    {
         private Texture2D t2dBackground;
         private float fTime;

         public HowToPlayScreen()
        {
            fTime = 0;

            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            t2dBackground = content.Load<Texture2D>(@"Textures/Screens/Info");
        }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);

            if (input.Click())
            {
                if (fTime > 2)
                {
                    ExitScreen();
                    //ScreenManager.AddScreen(new MainMenuScreen());
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            fTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
           

            // Fade the popup alpha during transitions.
            Color color = new Color(255, 255, 255, TransitionAlpha);

            spriteBatch.Begin();

            // Draw the background rectangle.          
            spriteBatch.Draw(t2dBackground, Vector2.Zero, new Rectangle(0, 0, t2dBackground.Width, t2dBackground.Height), color, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);            

            spriteBatch.End();
        }
    }
}
