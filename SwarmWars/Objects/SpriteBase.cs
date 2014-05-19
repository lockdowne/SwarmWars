#region File Description
/*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\
 * This is the base class for all sprites in the game  *
\*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;

namespace SwarmWars.Objects
{
    public class SpriteBase
    {
        #region Properties      
        

        private User user;

        private Texture2D t2dTexture;

        private Vector2 v2Position;
        private Vector2 v2Direction;

        private Color cColor;                
        
        private float fRadius;
        private float fScale;
        private float fReactionDistance;

        private bool isActive;
        private bool isVisible;
        private bool isEffects;

        /// <summary>
        /// Gets and Sets the current user of sprite
        /// </summary>
        public User Player
        {
            get { return user; }
            set { user = value; }
        }

        /// <summary>
        /// Gets the texture
        /// </summary>
        public Texture2D Texture
        {
            get { return t2dTexture; }
            set { t2dTexture = value; }
        }

        /// <summary>
        /// Gets and Sets the position of the sprite 
        /// </summary>
        public Vector2 Position
        {
            get { return v2Position; }
            set { v2Position = value; }
        }

        /// <summary>
        /// Direction of sprite
        /// </summary>
        public Vector2 Direction
        {
            get { return v2Direction; }
            set { v2Direction = value; }
        }

        /// <summary>
        /// Gets the origin of the sprite
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(t2dTexture.Width / 2, t2dTexture.Height / 2); }
        }
        
        /// <summary>
        /// Calculates the center point of the sprite
        /// </summary>
        public Vector2 Center
        {
            get { return new Vector2((v2Position.X), (v2Position.Y)); }
        }

        /// <summary>
        /// Gets and Sets the color of the sprite
        /// </summary>
        public Color Tint
        {
            get { return cColor; }
            set { cColor = value; }
        }

        /// <summary>
        /// Gets and Sets the radius of the sprite
        /// </summary>
        public float Radius
        {
            get { return fRadius; }
            set { fRadius = value; }
        }

        /// <summary>
        /// Sets and Gets the scale of the sprite
        /// </summary>
        public float Scale
        {
            get { return fScale; }
            set { fScale = value; }
        }

        /// <summary>
        /// Reaction Distance
        /// </summary>
        public float ReactionDistance
        {
            get { return fReactionDistance; }
            set { fReactionDistance = value; }
        }

        /// <summary>
        /// Calculates the width of the sprite texture
        /// </summary>
        public int Width
        {
            get { return t2dTexture.Width; }
        }

        /// <summary>
        /// Calculates the height of the sprite texture
        /// </summary>
        public int Height
        {
            get { return t2dTexture.Height; }
        }

        /// <summary>
        /// When active sprite may update
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// When visible the sprite may be drawn
        /// </summary> 
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// When effects draw particles
        /// </summary> 
        public bool IsEffects
        {
            get { return isEffects; }
            set { isEffects = value; }
        }
        #endregion

        public SpriteBase(Texture2D texture)
        {
            t2dTexture = texture;
        }

        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        { }

    }
}
