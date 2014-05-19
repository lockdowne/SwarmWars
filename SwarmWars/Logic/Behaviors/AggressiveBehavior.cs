using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Objects;

namespace SwarmWars.Logic.Behaviors
{
    public class AggressiveBehavior : Behavior
    {
        private int iBaseDecideTime;

        public AggressiveBehavior(User user)
            : base(user)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update Times
            TotalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

           
        }

        
    }
}
