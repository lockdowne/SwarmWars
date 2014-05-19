using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;

namespace SwarmWars.Objects
{
    public class User
    {
        private Color cColor;

        private float fRegen;
        private float fPower;
        private float fSpeed;

        private int iLevel;


        public Color Tint
        {
            get { return cColor; }
            set { cColor = value; }
        }

        public float Regen
        {
            get { return fRegen; }
            set { fRegen = value; }
        }

        public float Power
        {
            get { return fPower; }
            set { fPower = value; }
        }

        public float Speed
        {
            get { return fSpeed; }
            set { fSpeed = value; }
        }        

        public int Level
        {
            get { return iLevel; }
        }

        public User(int regenLevel, int powerLevel, int speedLevel, Color color)
        {

            SetRegen(regenLevel);
            SetPower(powerLevel);
            SetSpeed(speedLevel);

            cColor = color;

            iLevel = regenLevel + powerLevel + speedLevel;

        }

        public void SetPower(int level)
        {
            fPower = 100 + (level - 1) * 5;
        }

        public void SetRegen(int level)
        {
            fRegen = 300 + (level - 1) * 100;
        }

        public void SetSpeed(int level)
        {
            fSpeed = 1 + (level - 1) * .3f;
        }
        

    }
}
