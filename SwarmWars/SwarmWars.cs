using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using SwarmWars.Logic;
using SwarmWars.Logic.Screens;

namespace SwarmWars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SwarmWars : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ScreenManager screenManager;

        Texture2D[] textures = new Texture2D[20];

        SoundEffect[] sounds = new SoundEffect[10];

        SpriteFont[] fonts = new SpriteFont[10];

        public SwarmWars()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1200;

            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textures[0] = Content.Load<Texture2D>(@"Textures/Line");
            textures[1] = Content.Load<Texture2D>(@"Textures/MainScreen");
            textures[2] = Content.Load<Texture2D>(@"Textures/Nest");
            textures[3] = Content.Load<Texture2D>(@"Textures/WorldMap");
            textures[4] = Content.Load<Texture2D>(@"Textures/GameoverScreen");
            textures[5] = Content.Load<Texture2D>(@"Textures/MapEntry");
            textures[6] = Content.Load<Texture2D>(@"Textures/VictoryScreen");
            textures[7] = Content.Load<Texture2D>(@"Textures/PowerButton");
            textures[8] = Content.Load<Texture2D>(@"Textures/RegenButton");
            textures[9] = Content.Load<Texture2D>(@"Textures/SpeedButton");
            textures[10] = Content.Load<Texture2D>(@"Textures/Ice");
            textures[11] = Content.Load<Texture2D>(@"Textures/NestOver");
            textures[12] = Content.Load<Texture2D>(@"Textures/GameBackground1");
            textures[13] = Content.Load<Texture2D>(@"Textures/Smoke");
            textures[14] = Content.Load<Texture2D>(@"Textures/Smoke2");
            textures[15] = Content.Load<Texture2D>(@"Textures/Slice 3");
            textures[16] = Content.Load<Texture2D>(@"Textures/HiveSlow");
            textures[17] = Content.Load<Texture2D>(@"Textures/HiveSelect");
            textures[18] = Content.Load<Texture2D>(@"Textures/star");

            Resources.Textures.Add("Line", textures[0]);
            Resources.Textures.Add("MainScreen", textures[1]);
            Resources.Textures.Add("Nest", textures[2]);
            Resources.Textures.Add("WorldMap", textures[3]);
            Resources.Textures.Add("GameoverScreen", textures[4]);
            Resources.Textures.Add("MapEntry", textures[5]);
            Resources.Textures.Add("VictoryScreen", textures[6]);
            Resources.Textures.Add("PowerButton", textures[7]);
            Resources.Textures.Add("RegenButton", textures[8]);
            Resources.Textures.Add("SpeedButton", textures[9]);
            Resources.Textures.Add("Ice", textures[10]);
            Resources.Textures.Add("NestOver", textures[11]);
            Resources.Textures.Add("GameBackground1", textures[12]);
            Resources.Textures.Add("Smoke", textures[13]);
            Resources.Textures.Add("Smoke2", textures[14]);
            Resources.Textures.Add("Slice3", textures[15]);
            Resources.Textures.Add("HiveSlow", textures[16]);
            Resources.Textures.Add("HiveSelect", textures[17]);
            Resources.Textures.Add("Star", textures[18]);

            sounds[0] = Content.Load<SoundEffect>(@"Sounds/Intro");
            sounds[1] = Content.Load<SoundEffect>(@"Sounds/NestSelect");
            sounds[2] = Content.Load<SoundEffect>(@"Sounds/NestSpawn");
            sounds[3] = Content.Load<SoundEffect>(@"Sounds/SwarmiteDeath");

            Resources.Sounds.Add("Intro", sounds[0]);
            Resources.Sounds.Add("NestSelect", sounds[1]);
            Resources.Sounds.Add("NestSpawn", sounds[2]);
            Resources.Sounds.Add("SwarmiteDeath", sounds[3]);

            fonts[0] = Content.Load<SpriteFont>(@"Fonts/MenuFont");
            fonts[1] = Content.Load<SpriteFont>(@"Fonts/DisplayFont");

            Resources.Fonts.Add("MenuFont", fonts[0]);
            Resources.Fonts.Add("DisplayFont", fonts[1]);

            screenManager.AddScreen(new BackgroundScreen());
            screenManager.AddScreen(new MainMenuScreen());
     
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
           
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
