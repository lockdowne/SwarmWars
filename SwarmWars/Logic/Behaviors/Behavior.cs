using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Objects;

namespace SwarmWars.Logic.Behaviors
{
    public class Behavior
    {
        public List<Nest> ControlledNests = new List<Nest>();

        private User player;

        private float fDecideTime;
        private float fTotalTime;
        private float fElapsedTime;

        private bool isActive;

        private Nest destination;

        public User Player
        {
            get { return player; }
        }
        public float DecideTime
        {
            get { return fDecideTime; }
            set { fDecideTime = value; }
        }

        public float TotalTime
        {
            get { return fElapsedTime; }
            set { fElapsedTime = value; }
        }

        public float ElapsedTime
        {
            get { return fTotalTime; }
            set { fTotalTime = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public Nest Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        
        public Behavior(User user)
        {
            fDecideTime = 0;
            fTotalTime = 0;
            fElapsedTime = 0;

            isActive = true;

            player = user;
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateList();
        }

        public void UpdateList()
        {
            ControlledNests.Clear();

            foreach (Nest n in Resources.Nests)
            {
                if (n.Player == player)
                {
                    ControlledNests.Add(n);
                }
            }
        }


    }
}
