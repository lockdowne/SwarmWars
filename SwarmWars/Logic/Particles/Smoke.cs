using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Objects;

namespace SwarmWars.Logic.Particles
{
    public class Smoke
    {

        private Texture2D t2dTexture;
        private Utils utils;

        public Smoke()
        {
            t2dTexture = Resources.Textures["Smoke"];

            utils = new Utils();
        }

        public void Emitt(Vector2 position)
        {
            Vector2 velocity = new Vector2(
              (float)(utils.NextDouble() * 2 - 1),
              0);

            float angle = 0;
            float angularVelocity = 0.1f * (utils.NextDouble() * 2 - 1);

            Color color = Color.White;

            float size = 1f;// (float)Utils.NextDouble() * .8f;

            int lifeSpan = 40 + (int)utils.RandomBetween(0, 100);


            Resources.Particles.Add(new Particle(t2dTexture, position, velocity, angle, angularVelocity, size, lifeSpan, color));
        }

        public void Capture(Nest nest)
        {
            for (int i = 0; i < 30; i++)
            {
                float degrees = i * 10;

                float angle = MathHelper.ToRadians(degrees);

                Vector2 pos = new Vector2((nest.Position.X + (nest.Radius * utils.NextDouble()) * (float)Math.Cos(angle)), (nest.Position.Y + (nest.Radius * utils.NextDouble()) * (float)Math.Sin(angle)));

                /*Vector2 velocity = new Vector2(
                    (float)(utils.NextDouble() * 2 - 1),
                    (float)(utils.NextDouble() * 2 - 1));*/

                Vector2 velocity = Vector2.Zero;
               


                float angularVelocity = 0.1f * (utils.NextDouble() * 2 - 1);

                float size = utils.RandomBetween(4f, 5f);// (float)Utils.NextDouble() * .8f;

                int lifeSpan = 250 + (int)utils.RandomBetween(0, 5);

                Resources.Particles.Add(new Particle(t2dTexture, pos, velocity, angle, angularVelocity, size, lifeSpan, nest.Tint));
            }
            
        }
    }
}
