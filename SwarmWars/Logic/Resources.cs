using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

using SwarmWars.Objects;
using SwarmWars.Logic.Particles;
using SwarmWars.Logic.Screens;

namespace SwarmWars.Logic
{
    public class Resources
    {
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        public static Dictionary<string, Song> Songs = new Dictionary<string, Song>();
        public static Dictionary<string, User> Users = new Dictionary<string, User>();
        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();

        public static List<Nest> Nests = new List<Nest>();
        public static List<Swarmite> Swarmites = new List<Swarmite>();
        public static List<MenuEntry> MenuEntries = new List<MenuEntry>();
        public static List<Particle> Particles = new List<Particle>();
        public static List<Cloud> Clouds = new List<Cloud>();
    

        public static int Completed_Levels = 0;
        public static int Player_Regen = 1;
        public static int Player_Power = 1;
        public static int Player_Speed = 1;

        public Resources()
        { }

        /// <summary>
        /// Update Sprites
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {            
              #region Clouds
            for(int iClouds = 0; iClouds < Clouds.Count; iClouds++)
            {
                Clouds[iClouds].Update(gameTime);
            }
            #endregion
            #region Nests
            for (int iNest = 0; iNest < Nests.Count; iNest++)
            {
                Nests[iNest].Update(gameTime);
            }
            #endregion
            #region Swarm
            for (int iSwarmite = 0; iSwarmite < Swarmites.Count; iSwarmite++)
            {
                Swarmites[iSwarmite].Update(gameTime);
            }
            #endregion           
            #region Particles
            for (int iParticle = 0; iParticle < Particles.Count; iParticle++)
            {
                Particles[iParticle].Update(gameTime);
            }
            #endregion  
          
           
        }

        /// <summary>
        /// Draw Sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            #region Clouds
            for(int iClouds = 0; iClouds < Clouds.Count; iClouds++)
            {
                Clouds[iClouds].Draw(spriteBatch);
            }
            #endregion
            #region Nests
            for (int iNest = 0; iNest < Nests.Count; iNest++)
            {
                Nests[iNest].Draw(spriteBatch);
            }
            #endregion
            #region Swarm
            for (int iSwarmite = 0; iSwarmite < Swarmites.Count; iSwarmite++)
            {
                Swarmites[iSwarmite].Draw(spriteBatch);
            }
            #endregion 
            #region Particles
            for (int iParticle = 0; iParticle < Particles.Count; iParticle++)
            {
                Particles[iParticle].Draw(spriteBatch);
            }
            #endregion  
           

        }
    }
}
