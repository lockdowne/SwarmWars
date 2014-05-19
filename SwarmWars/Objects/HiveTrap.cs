using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;
using SwarmWars.Logic.Particles;

namespace SwarmWars.Objects
{
    public class HiveTrap : Nest
    {
        private float fTime;
        private Utils utils;

        private Thorn effect;

         public HiveTrap(Vector2 position, User player, float population, float size)
            : base(position, player, population, size)
        {
            fTime = 0;
            utils = new Utils();

            effect = new Thorn();
        }

        public override void SpawnSwarmites(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            effect.Emitt(gameTime, this);

            Kill(gameTime);
           
        }

        public void Kill(GameTime gameTime)
        {
            fTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;




            for (int i = 0; i < Resources.Swarmites.Count; i++)
            {
                if (Resources.Swarmites[i].Player != Player)
                {
                    // Check for collision
                    Vector2 vector = Position - Resources.Swarmites[i].Position;

                    float radii = (Radius * 4) + Resources.Swarmites[i].Radius;

                    if (vector.LengthSquared() < radii * radii)
                    {
                        effect.Emitt(Resources.Swarmites[i]);
                        if (fTime >= 1000)
                        {
                            fTime = 0;

                            if (utils.Chance(45))
                            {

                                
                                Resources.Swarmites[i].Remove();

                            }
                        }
                    }
                }
            }
        }
    }
}
