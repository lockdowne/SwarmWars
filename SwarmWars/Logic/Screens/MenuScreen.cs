using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SwarmWars.Logic.Particles;

namespace SwarmWars.Logic.Screens
{
    public class MenuScreen : GameScreen
    {
        private Utils utils;

        private int iSelectIndex = 0;

        protected IList<MenuEntry> MenuEntries
        {
            get { return Resources.MenuEntries; }
        }

        public MenuScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            utils = new Utils();
        }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);

            if (input.IsLeftDown())
            {
                foreach (MenuEntry me in MenuEntries)
                {
                    if (utils.IsClick(input.MousePosition, me.Bounds))
                    {
                        iSelectIndex = me.Index;
                        OnSelectEntry(iSelectIndex);
                    }
                }
            }
           
        }

        protected virtual void OnSelectEntry(int entryIndex)
        {
            Resources.MenuEntries[iSelectIndex].OnSelectEntry();
        }

        protected virtual void OnCancel()
        {
            ExitScreen();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
                      
            // Update each nested MenuEntry object.
            for (int i = 0; i < Resources.MenuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == iSelectIndex);

                Resources.MenuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            Vector2 position = new Vector2(100, 150);

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            if (ScreenState == ScreenState.TransitionOn)
                position.X -= transitionOffset * 256;
            else
                position.X += transitionOffset * 512;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < Resources.MenuEntries.Count; i++)
            {
                MenuEntry menuEntry = Resources.MenuEntries[i];

                bool isSelected = IsActive && (i == iSelectIndex);

                menuEntry.Draw(this, position, isSelected, gameTime);

                position.Y += menuEntry.GetHeight(this);
            }

            spriteBatch.End();
        }

        
    }
}
