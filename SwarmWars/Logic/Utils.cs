using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic
{
    public class Utils
    {
        private Random random;

        public Utils()
        {
            random = new Random();
        }

        public bool IsClick(Vector2 mouse, Rectangle rect)
        {
            if (mouse.X > rect.Left && mouse.X < rect.Right 
                && mouse.Y > rect.Top && mouse.Y < rect.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsClick(Vector2 mouse, Vector2 pos, float radius)
        {
             Vector2 vector = mouse - pos;
             float radii = radius;

             if (vector.LengthSquared() < radii * radii)
             {
                 return true;
             }
             else
             {
                 return false;
             }
        }

        public bool IsIntersect(Vector2 mouse, Texture2D texture, Vector2 position)
        {
            if (mouse.X > position.X && mouse.X < position.X + texture.Width
                && mouse.Y > position.Y && mouse.Y < position.Y + texture.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float VectorToAngle(Vector2 vector1, Vector2 vector2)
        {
            return (float)Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X);
        }

        public float RandomBetween(double min, double max)
        {
            return (float)(min + (float)random.NextDouble() * (max - min));
        }

        public bool Chance(int percentage)
        {
            double chance = percentage + random.Next(1, 100);

            if (chance >= 100)
            {
                return true;
            }

            return false;
        }

        public float NextDouble()
        {
            float num = (float)random.NextDouble();

            return num;
        }


    }
}
