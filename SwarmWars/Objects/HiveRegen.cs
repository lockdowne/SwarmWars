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
    public class HiveRegen : Nest
    {
        private float fTime;
        private Fire effect;

        public HiveRegen(Vector2 position, User player, float population, float size)
            : base(position, player, population, size)
        {
            effect = new Fire();
        }

        public override void SpawnSwarmites(GameTime gameTime)
        {
            fTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (fTime > ((Consts.Regen_Base - Player.Regen) / 2))
            {
                Population++;

                fTime = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            effect.Emitt(gameTime, this);
        }
    }
}
