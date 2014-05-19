using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using SwarmWars.Logic;
using SwarmWars.Logic.Particles;

namespace SwarmWars.Objects
{
    public class Swarmite : SpriteBase
    {
        private Texture2D t2dEffects;

        private Vector2 v2Target;

        private Nest destination;
        private Nest begin;        

        private bool isBeingSlowed;

        private Trail effect;

        public bool IsBeingSlowed
        {
            get { return isBeingSlowed; }
            set { isBeingSlowed = value; }
        }

        public Swarmite(User user, Nest start, Nest end, Vector2 position)
            :base(Resources.Textures["Ice"])
        {
            Player = user;

            Tint = Player.Tint;

            IsActive = true;
            IsVisible = true;

            Radius = 5;
            Scale = 1f;

            Position = position;

            v2Target = end.Center;
            begin = start;
            destination = end;

            Direction = Vector2.Zero;

            isBeingSlowed = false;

            effect = new Trail();

            
        }

        public override void Update(GameTime gameTime)
        {    
            if (IsActive)
            {
                DetectCollision();
                MovementAndSteering();

                effect.Emitt(Position, this, gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                //spriteBatch.Draw(Texture, Position, null, Player.Tint, 0f, Origin, Scale, SpriteEffects.None, 0f);
            }
        }

        private void DetectCollision()
        {
            Vector2 vector = Center - destination.Center;

            float radii = Radius + destination.Radius - 10f;

            if (vector.LengthSquared() < radii * radii)
            {
                // Check if controlled by player
                if (destination.Player == Player)
                {
                    destination.Population++;
                }
                // Check if nuetral 
                else if (destination.Player == Resources.Users["Neutral"])
                {
                    destination.GiveControl(Player);
                    destination.Population++;
                }
                // Check if controlled by computer
                else
                {
                    DoDamage();
                }                
                
                // Remove
                Remove();
                

            }

            
        }

        private void MovementAndSteering()
        {
            float pullDistance = Vector2.Distance(v2Target, Position);

            if (pullDistance > 1)
            {
               
                Vector2 pull = (v2Target - Position) * (1 / pullDistance); //the target tries to 'pull us in'
                Vector2 totalPush = Vector2.Zero;

                int contenders = 0;

                foreach (Nest n in Resources.Nests)
                {
                    if (n != begin && n != destination)
                    {
                        //draw a vector from the obstacle to the ship, that 'pushes the ship away'
                        Vector2 push = Position - n.Center;

                        //calculate how much we are pushed away from this obstacle, the closer, the more push
                        float distance = (Vector2.Distance(Position, n.Center) - n.Radius) - Radius;
                        //only use push force if this object is close enough such that an effect is needed
                        if (distance < Radius * 3)
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
                }

                pull *= Math.Max(1, 10 * contenders); //4 * contenders gives the pull enough force to pull stuff trough (tweak this setting for your game!)
                pull += totalPush;

                pull.Normalize();
                //Set the ships new position;
                if (isBeingSlowed)
                {
                    Position += (pull * (Player.Speed  + Consts.Speed_Base))/ 2;
                }
                else
                {
                    Position += (pull * (Player.Speed + Consts.Speed_Base));
                }
            }
        }

        private void DoDamage()
        {
            float dm = (Player.Power / Consts.Power_Base);

            destination.Population -= dm;
        }

        public void Remove()
        {
            Resources.Sounds["SwarmiteDeath"].Play();
            effect.Explode(Position, this);
            IsActive = false;
            IsVisible = false;
            Resources.Swarmites.Remove(this);
        }


        

    }
}
