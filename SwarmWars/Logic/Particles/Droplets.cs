using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;

namespace SwarmWars.Logic.Particles
{
    public class Droplets
    {
        private Texture2D t2dTexture;
        private Utils utils;

        public Droplets()
        {
            t2dTexture = Resources.Textures["Circle"];

            utils = new Utils();
        }

        public void Emitter()
        {
            Vector2 velocity = new Vector2(
               0,
               utils.NextDouble());

            float angle = 0;
            float angularVelocity = 0.1f * (utils.NextDouble() * 2 - 1);

            Color color = new Color(
                 utils.NextDouble(),
                 utils.NextDouble(),
                 utils.NextDouble(),
                  1);

            float size = 1f;// (float)Utils.NextDouble() * .8f;

            int lifeSpan = 40 + (int)utils.RandomBetween(0, 100);

            Vector2 pos = new Vector2(600, 200);
            
            
            Resources.Particles.Add(new Particle(t2dTexture, pos, velocity, angle, angularVelocity, size, lifeSpan, color));
            
        }
    }
}
