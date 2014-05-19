using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic.Screens
{
    public class MenuEntry
    {
        private Vector2 v2Position;
        private Rectangle rectBounds;

        private Utils utils;

        private string sText;

        private int iIndex;

        private float fSelectionFade;

        public int Index
        {
            get { return iIndex; }
            set { iIndex = value; }
        }

        public Vector2 Position
        {
            get { return v2Position; }
        }

        public string Text
        {
            get { return sText; }
            set { sText = value; }
        }

        public Rectangle Bounds
        {
            get { return rectBounds; }
        }
       

        public event EventHandler<EventArgs> Selected;

        protected internal virtual void OnSelectEntry()
        {
            if (Selected != null)
                Selected(this, EventArgs.Empty);
        }

        public MenuEntry(string text)
        {
            sText = text;

            utils = new Utils();
        }

        public virtual void Update(MenuScreen screen, bool isSelected,
                                                   GameTime gameTime)
        {
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                fSelectionFade = Math.Min(fSelectionFade + fadeSpeed, 1);
            else
                fSelectionFade = Math.Max(fSelectionFade - fadeSpeed, 0);
        }

        public virtual void Draw(MenuScreen screen, Vector2 position,
                               bool isSelected, GameTime gameTime)
        {           
            Color color = Color.White;
            

            v2Position = position;

            // Draw texture, centered on the screen
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = Resources.Fonts["MenuFont"];
            float scale = 1f;

            

            Vector2 size = screenManager.Font.MeasureString(sText);
            rectBounds.Height = (int)size.Y;
            rectBounds.Width = (int)size.X;
            rectBounds.Location = new Point((int)position.X, (int)position.Y);

            if (utils.IsClick(screenManager.Input.MousePosition, rectBounds))
            {
                color = Color.Yellow;
            }

            Vector2 origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, sText, position, color, 0,
                                   origin, scale, SpriteEffects.None, 0);

            

        }

        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing + 20;
        }
    }
}
