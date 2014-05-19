using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Objects;

namespace SwarmWars.Logic.Particles
{
    public class Ice
    {
        private Texture2D t2dTexture;
        private Utils utils;

        public Ice()
        {
            t2dTexture = Resources.Textures["Ice"];

            utils = new Utils();  
        }

        public void Emitt(GameTime gameTime, Nest nest)
        {
            float degrees = utils.RandomBetween(0, 360);

            float angle = MathHelper.ToRadians(degrees);

            Vector2 pos = new Vector2((nest.Position.X + (nest.Radius) * (float)Math.Cos(angle)), (nest.Position.Y + (nest.Radius) * (float)Math.Sin(angle)));

            Vector2 velocity = new Vector2(
                (float)(utils.NextDouble() * 2 - 1),
                (float)(utils.NextDouble() * 2 - 1));

            //Vector2 velocity = Vector2.Zero;

            float angularVelocity = 0.1f * (utils.NextDouble() * 2 - 1);

            float size = utils.RandomBetween(0.35f, 0.5f);// (float)Utils.NextDouble() * .8f;

            int lifeSpan = 100 + (int)utils.RandomBetween(0, 50);

            Resources.Particles.Add(new Particle(t2dTexture, pos, velocity, angle, angularVelocity, size, lifeSpan, Color.LightBlue));           

        }

        public void Emitt(Swarmite swarmite)
        {
            Vector2 pos = swarmite.Position;

            Vector2 velocity = new Vector2(
                (float)(utils.NextDouble() * 2 - 1),
                (float)(utils.NextDouble() * 2 - 1));

            //Vector2 velocity = Vector2.Zero;

            float angle = 0;

            float angularVelocity = 0.1f * (utils.NextDouble() * 2 - 1);

            float size = utils.RandomBetween(0.35f, 0.5f);// (float)Utils.NextDouble() * .8f;

            int lifeSpan = 10 + (int)utils.RandomBetween(0, 25);

            Resources.Particles.Add(new Particle(t2dTexture, pos, velocity, angle, angularVelocity, size, lifeSpan, Color.LightBlue));

        }
    }
}
