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
    public class Nest : SpriteBase
    {
        private Utils utils;

        private Texture2D t2dSelected;

        private SpriteFont font;

        private Vector2 v2Origin;

        private string sNumberOfSwarmites;

        private float fPopulation;
        private float fTime;
        private float fElapsedTime;

        private float fRotation;

        private double dTime;
        

        private bool isOwned;
        private bool isSelected;
        private bool isMouseOver;

        private Smoke effect;

        public float Population
        {
            get { return fPopulation; }
            set
            {
                fPopulation = value;

                if (fPopulation < 1)
                {
                    isOwned = false;
                    fPopulation = 0;
                    GiveControl(Resources.Users["Neutral"]);
                }
                else
                {
                    isOwned = true;
                }
            }
        }

        public Nest(Vector2 position, User player, float population, float size)
            :base(Resources.Textures["Nest"])
        {
            t2dSelected = Resources.Textures["NestOver"];
            Texture = Resources.Textures["Nest"];

            Position = position;
            Population = population;

            Player = player;

            IsActive = true;
            IsVisible = true;
            isMouseOver = false;

            Tint = Player.Tint;
            Scale = 1f * size;
            Radius = (Texture.Height / 2) * Scale;

            v2Origin = new Vector2(t2dSelected.Width / 2, t2dSelected.Height / 2);

            font = Resources.Fonts["MenuFont"];

            utils = new Utils();

            fRotation = utils.RandomBetween(0, 0.04f);

            effect = new Smoke();

            dTime = utils.RandomBetween(0, 4);

        }

        public bool IsOwned
        {
            get { return isOwned;}
            set
            {
                isOwned = value;
            }

        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public void GiveControl(User user)
        {
            Player = user;
            Tint = Player.Tint;

            if (user != Resources.Users["Neutral"])
            {
                isOwned = true;
                effect.Capture(this);
            }
        }
        
        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (isOwned)
                {
                    SpawnSwarmites(gameTime);  
                }

                dTime = gameTime.TotalGameTime.TotalSeconds;

                fElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (fElapsedTime > 100 + utils.RandomBetween(0, 20))
                {
                    fElapsedTime = 0;
                    
                }
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            if (IsVisible)
            {
                //Pulsate the nest        
                float pulsate = (float)Math.Sin((dTime) * 0.9f) + 1;
            
                float scale = (Scale - .05f) + pulsate * 0.04f;

                fRotation += 0.001f;

                spriteBatch.Draw(Texture, Position, new Rectangle(0, 0 ,Width, Height), Tint, fRotation, Origin, scale, SpriteEffects.None, 0);

                if (isOwned)
                {
                    
                    sNumberOfSwarmites = Population.ToString("###");

                    Vector2 origin = font.MeasureString(sNumberOfSwarmites);

                    spriteBatch.DrawString(font, sNumberOfSwarmites , Center, Color.White, 0, new Vector2(origin.X / 2, origin.Y / 2), 1, SpriteEffects.None, 0);

                    if (isMouseOver || isSelected)
                    {
                        if (Player == Resources.Users["Player"])
                        {
                            Color color = Color.Gray;

                            spriteBatch.Draw(t2dSelected, Position, new Rectangle(0, 0, Width, Height), new Color(color.R, color.G, color.B, .75f), 0f, v2Origin, Scale * 1.15f, SpriteEffects.None, 0);
                        }
                    }
                }                
            }
        }

        public virtual void SpawnSwarmites(GameTime gameTime)
        {
            fTime += (float)gameTime.ElapsedGameTime.Milliseconds;

            // Regen rate algorithm
            if (Scale == 1)
            {
                if (fTime > Consts.Regen_Base - Player.Regen)
                {
                    Population++;

                    fTime = 0;
                }
            }
            else
            {
                if (fTime > (Consts.Regen_Base + 1000) - Player.Regen)
                {
                    Population++;

                    fTime = 0;
                }
            }
        }

        public void SpawnSwarm(Nest targetNest)
        {
            if (Population < 2) return;

            
            Resources.Sounds["NestSpawn"].Play();
            

            int swarmSize = (int)Math.Round(Population / 2);

            
           
            Population -= swarmSize;


            for (int i = 0; i < swarmSize; i++)
            {
                float degrees = utils.RandomBetween(0, 360);

                float angle = MathHelper.ToRadians(degrees);

                Vector2 pos = new Vector2((Position.X + (Radius * utils.NextDouble()) * (float)Math.Cos(angle)), (Position.Y + (Radius * utils.NextDouble()) * (float)Math.Sin(angle)));

                Resources.Swarmites.Add(new Swarmite(Player, this, targetNest, pos));                
            }
        }

        public void IsMouseOver(InputManager input)
        {
            Vector2 vector = input.MousePosition - Center;
            float radii = Radius;

            if (vector.LengthSquared() < radii * radii)
            {
                isMouseOver = true;
            }
            else
            {
                isMouseOver = false;
            }
        }

        public void Select()
        {
            isSelected = true;

            Resources.Sounds["NestSelect"].Play();
            
        }

        public void DeSelect()
        {
            isSelected = false;
        }

    }
}
