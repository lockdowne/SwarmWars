using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;

namespace SwarmWars.Logic.Screens
{
    public class VictoryScreen : GameScreen
    {

        private Texture2D t2dBackground;
        private Texture2D t2dRegen;
        private Texture2D t2dPower;
        private Texture2D t2dSpeed;

        private Utils utils;


        public VictoryScreen()
        {
            
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);

            utils = new Utils();
        }

        public override void LoadContent()
        {
            t2dBackground = Resources.Textures["VictoryScreen"];
            t2dRegen = Resources.Textures["RegenButton"];
            t2dPower = Resources.Textures["PowerButton"];
            t2dSpeed = Resources.Textures["SpeedButton"];
        }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);

            if (utils.IsIntersect(input.MousePosition, t2dRegen, new Vector2((Consts.Screen_Width / 2) - 200, (Consts.Screen_Height / 2) + 100)))
            {
                if(input.Click())
                {
                    Resources.Player_Regen++;
                    Resources.Completed_Levels++;
                    
                    //ScreenManager.AddScreen(new WorldMapScreen());
                    ExitScreen();
                }
            }
            else if (utils.IsIntersect(input.MousePosition, t2dPower, new Vector2((Consts.Screen_Width / 2), (Consts.Screen_Height / 2) + 100)))
            {
                if (input.Click())
                {
                    Resources.Player_Power++;
                    Resources.Completed_Levels++;
                    
                    //ScreenManager.AddScreen(new WorldMapScreen());
                    ExitScreen();
                }
            }
            else if (utils.IsIntersect(input.MousePosition, t2dSpeed, new Vector2((Consts.Screen_Width / 2) + 200, (Consts.Screen_Height / 2) + 100)))
            {
                if (input.Click())
                {
                    Resources.Player_Speed++;
                    Resources.Completed_Levels++;
                    
                    //ScreenManager.AddScreen(new WorldMapScreen());
                    ExitScreen();   
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = Resources.Fonts["DisplayFont"];

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
           

            // Fade the popup alpha during transitions.
            Color color = new Color(255, 255, 255, TransitionAlpha);

            spriteBatch.Begin();

            // Draw the background rectangle.          
            spriteBatch.Draw(t2dBackground, new Vector2(Consts.Screen_Width / 2, Consts.Screen_Height / 2), new Rectangle(0, 0, t2dBackground.Width, t2dBackground.Height), color, 0f, new Vector2(t2dBackground.Width / 2, t2dBackground.Height / 2), 1, SpriteEffects.None, 0);
            spriteBatch.Draw(t2dRegen, new Vector2((Consts.Screen_Width / 2) - 200, (Consts.Screen_Height / 2) + 100), new Rectangle(0, 0, t2dRegen.Width, t2dRegen.Height), color, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(t2dPower, new Vector2((Consts.Screen_Width / 2), (Consts.Screen_Height / 2) + 100), new Rectangle(0, 0, t2dPower.Width, t2dPower.Height), color, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(t2dSpeed, new Vector2((Consts.Screen_Width / 2) + 200, (Consts.Screen_Height / 2) + 100), new Rectangle(0, 0, t2dSpeed.Width, t2dSpeed.Height), color, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, "Level: " + Resources.Player_Regen.ToString(), new Vector2((Consts.Screen_Width / 2) - 200, (Consts.Screen_Height / 2) + 70), color);
            spriteBatch.DrawString(font, "Level: " + Resources.Player_Power.ToString(), new Vector2((Consts.Screen_Width / 2), (Consts.Screen_Height / 2) + 70), color);
            spriteBatch.DrawString(font, "Level: " + Resources.Player_Speed.ToString(), new Vector2((Consts.Screen_Width / 2) + 200, (Consts.Screen_Height / 2) + 70), color);

            spriteBatch.DrawString(font, "Regen", new Vector2((Consts.Screen_Width / 2) - 194, (Consts.Screen_Height / 2) + 165), color);
            spriteBatch.DrawString(font, "Power", new Vector2((Consts.Screen_Width / 2) + 6, (Consts.Screen_Height / 2) + 165), color);
            spriteBatch.DrawString(font, "Speed", new Vector2((Consts.Screen_Width / 2) + 206, (Consts.Screen_Height / 2) + 165), color);

            spriteBatch.End();
        }
    }
}
