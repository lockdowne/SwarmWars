using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;

namespace SwarmWars.Logic.Particles
{
    public class Particle
    {
        private Texture2D t2dTexture;

        private Vector2 v2Position;
        private Vector2 v2Velocity;
        private Vector2 v2Origin;

        private Rectangle rectRectangle;

        private float fAngle;
        private float fAngularVelocity;
        private float fSize;
        private float fLifeSpan;
        private byte bOpacity;

        private Color cColor;

        private bool isActive;
        private bool isVisible;


        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, float angle, float angularVelocity, float size, float lifeSpan, Color color)
        {
            t2dTexture = texture;

            v2Position = position;
            v2Velocity = velocity;
            v2Origin = new Vector2(t2dTexture.Width / 2, t2dTexture.Height / 2);

            fAngle = angle;
            fAngularVelocity = angularVelocity;
            fSize = size;
            fLifeSpan = lifeSpan;
            bOpacity = color.A;

            cColor = color;

            rectRectangle = new Rectangle(0, 0, t2dTexture.Width, t2dTexture.Height);

            isActive = true;
            isVisible = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(t2dTexture, v2Position, rectRectangle, new Color(cColor.R, cColor.G, cColor.B, bOpacity), fAngle, v2Origin, fSize, SpriteEffects.None, 0f);
            }
        }

        public void Update(GameTime gameTime)
        {
            //fLifeSpan -= gameTime.ElapsedGameTime.Milliseconds;
            if (isActive)
            {
                fLifeSpan--;

                if (bOpacity > 0)
                {
                    bOpacity--;
                }
                if (fSize > 0)
                {
                    fSize -= .01f;
                }

                v2Position += v2Velocity;
                fAngle += fAngularVelocity;



                //cColor = new Color(cColor.R, cColor.G, cColor.B, fOpacity);
                //fOpacity -=  ((float)random.NextDouble() * .025f);

                if (fLifeSpan <= 0)
                    isActive = false;
            }
            else
            {
                Remove();
            }
        }

        private void Remove()
        {
            Resources.Particles.Remove(this);
        }
    }
}
