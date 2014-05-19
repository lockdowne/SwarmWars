using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic.Particles
{
    public class Cloud
    {
        private Texture2D t2dTexture;

        private Vector2 v2Original;
        private Vector2 v2Position;
        private Vector2 v2Target;

        private Vector2 v2Origin;

        private float fRadius;

        
        private float fSize;

        private Color cColor;

        private bool isActive;
        private bool isVisible;

        private Utils utils;

        public Cloud(Vector2 position)
        {
            t2dTexture = Resources.Textures["Smoke"];

            v2Original = position;
            v2Position = position;

            v2Origin = new Vector2(t2dTexture.Width / 2, t2dTexture.Height / 2);

            fRadius = 5;
            fSize = 1f;

            isActive = true;
            isVisible = true;

            v2Target = new Vector2(position.X - 2200, position.Y);

            cColor = Color.White;

            utils = new Utils();

            fSize = utils.RandomBetween(2f, 4f);
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                float pullDistance = Vector2.Distance(v2Target, v2Position);

                if (pullDistance > 1)
                {

                    Vector2 pull = (v2Target - v2Position) * (1 / pullDistance); //the target tries to 'pull us in'
                    Vector2 totalPush = Vector2.Zero;

                    int contenders = 0;

                    for (int i = 0; i < Resources.Swarmites.Count; i++)
                    {

                        //draw a vector from the obstacle to the ship, that 'pushes the ship away'
                        Vector2 push = v2Position - Resources.Swarmites[i].Center;

                        //calculate how much we are pushed away from this obstacle, the closer, the more push
                        float distance = (Vector2.Distance(v2Position, Resources.Swarmites[i].Center) - Resources.Swarmites[i].Radius) - fRadius;
                        //only use push force if this object is close enough such that an effect is needed
                        if (distance < fRadius * 25)
                        {
                            ++contenders; //note that this object is actively pushing

                            if (distance < 0.0001f) //prevent division by zero errors and extreme pushes
                            {
                                distance = 0.0001f;
                            }
                            float weight = 1 / distance;

                            totalPush += push * weight;
                        }
                    }



                    pull *= Math.Max(1, 4 * contenders); //4 * contenders gives the pull enough force to pull stuff trough (tweak this setting for your game!)
                    pull += totalPush;

                    pull.Normalize();

                    v2Position += (pull * (0.5f));


                }
                else
                {
                    v2Position = v2Original;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                spriteBatch.Draw(t2dTexture, v2Position, null, new Color(cColor.R, cColor.G, cColor.B, 100), 0f, v2Origin, fSize, SpriteEffects.None, 0f);
            }
        }
    }
}
