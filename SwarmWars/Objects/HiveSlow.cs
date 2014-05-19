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
    public class HiveSlow : Nest
    {
        private Ice effect;

        public HiveSlow(Vector2 position, User player, float population, float size)
            : base(position, player, population, size)
        {
            effect = new Ice();

        }

        public override void SpawnSwarmites(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Slow();
            effect.Emitt(gameTime, this);

            
        }

        private void Slow()
        {
            for (int i = 0; i < Resources.Swarmites.Count; i++)
            {
                if (Resources.Swarmites[i].Player != Player)
                {
                    // Check for collision
                    Vector2 vector = Position - Resources.Swarmites[i].Position;

                    float radii = (Radius * 4) + Resources.Swarmites[i].Radius;

                    if (vector.LengthSquared() < radii * radii)
                    {
                        Resources.Swarmites[i].IsBeingSlowed = true;
                        effect.Emitt(Resources.Swarmites[i]); 
                    }
                    else
                    {
                        Resources.Swarmites[i].IsBeingSlowed = false;
                    }
                }
            }
        }
    }
}
