using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic.Screens
{
    public class WorldScreen : GameScreen
    {
        private Texture2D t2dBackground;
        private Texture2D t2dMapEntry;

        private Vector2 v2Position;
        private Vector2 v2Origin;

        private float fRotation;
        private float fScale;

        private Utils utils;

        public WorldScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(3);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            v2Position = new Vector2(1200 / 4, 768 / 4);
            
            t2dBackground = Resources.Textures["WorldMap"];
            t2dMapEntry = Resources.Textures["Nest"];


            v2Origin = new Vector2(t2dMapEntry.Width / 4, t2dMapEntry.Height / 4);

            utils = new Utils();

            fRotation = 0;

            fScale = 1f;
        }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);

            if (input.Click())
            {
                if (utils.IsIntersect(input.MousePosition, t2dMapEntry, v2Position))
                {
                    ScreenManager.AddScreen(new GameplayScreen("Content/Levels/Level " + Resources.Completed_Levels + ".xml"));
                }
            }

            
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            spriteBatch.Draw(t2dBackground, new Rectangle(0,0,t2dBackground.Width, t2dBackground.Height), Color.White);

            spriteBatch.Draw(t2dMapEntry, v2Position, new Rectangle(0, 0, t2dMapEntry.Width, t2dMapEntry.Height), Color.Black, fRotation, Vector2.Zero, fScale, SpriteEffects.None, 0f);

            spriteBatch.End();

        }
    }
}
