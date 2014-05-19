using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;
using SwarmWars.Objects;

namespace SwarmWars.Logic.Particles
{
    public class Trail
    {
        private Texture2D t2dTexture;
    
        private Utils utils;

        private float fTime;
        private float fAngle;

        private bool trail;

        public Trail()
        {
            t2dTexture = Resources.Textures["Smoke"];

            fTime = 0;

            utils = new Utils();

            fAngle = utils.RandomBetween(0.45f, 0.55f);
        }

        

        public void Emitt(Vector2 position, Swarmite swarmite, GameTime gameTime)
        {
            fTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;



            if (fAngle > 0.55f)
                trail = true;
            if (fAngle < 0.45f)
                trail = false;
           

            if (trail)
            {
                fAngle -= 0.01f;
            }
            else
            {
                fAngle += 0.01f;
            }

           

            Vector2 velocity = new Vector2(
              (float)(fAngle * 2 - 1),
              (float)(fAngle * 2 - 1)); 

          // Vector2 velocity = Vector2.Zero;

          

            float angle = 0;
            float angularVelocity = 0.1f * (float)(utils.NextDouble() * 2 - 1);

            Color color = new Color(
                    swarmite.Tint.R,
                    swarmite.Tint.G,
                    swarmite.Tint.B,
                    100);

            float size = 1f;

            int lifeSpan = 25;

            Resources.Particles.Add(new Particle(t2dTexture, position, velocity, angle, angularVelocity, size, lifeSpan, color));

            if (fTime > 500)
            {
                fTime = 0;

                Vector2 velocity2 = new Vector2(
                  (float)(utils.NextDouble() * 2 - 1),
                  (float)(utils.NextDouble() * 2 - 1));

                float size2 = 1f;

                Resources.Particles.Add(new Particle(t2dTexture, position, velocity2, angle, angularVelocity, size2, lifeSpan, color));
            }

        }

        public void Explode(Vector2 position, Swarmite swarmite)
        {
            for (int i = 0; i < 25; i++)
            {
                Vector2 velocity = new Vector2(
                  (float)(utils.NextDouble() * 2 - 1),
                  (float)(utils.NextDouble() * 2 - 1));

                float angle = 0;
                float angularVelocity = 0.1f * (float)(utils.NextDouble() * 2 - 1);

               
                   Color color = new Color(
                      swarmite.Tint.R,
                      swarmite.Tint.G,
                      swarmite.Tint.B,
                      100);


               float size = utils.RandomBetween(1f, 1.5f);

                int lifeSpan = 30 + (int)utils.RandomBetween(0, 10);

                Resources.Particles.Add(new Particle(t2dTexture, position, velocity, angle, angularVelocity, size, lifeSpan, color));
            }
        }

    }
}
