//
// GameplayScreen will handle all game play for swarmites
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using SwarmWars.Objects;
using SwarmWars.Logic;
using SwarmWars.Logic.Particles;

namespace SwarmWars.Logic.Screens
{
    public class GameplayScreen : GameScreen
    {
        private Camera camera;

        private Vector2 v2MousePos;

        private Texture2D background;

        private Utils utils;

        private bool isSelecting;
        private bool isPaused;
        private bool isGameOver;
        private bool isGameWin;
        private bool isCameraShake;

        private string sUri;

        private float fElapsedTime;
        private float fCameraShakeTime = 0;
        private float fCameraShakeTimeEnd = 0;

        private Resources resourceManager;

        public GameplayScreen(string uri)
        {
            TransitionOnTime = TimeSpan.FromSeconds(3);
            TransitionOffTime = TimeSpan.FromSeconds(2);

            isSelecting = false;
            isPaused = false;
            isGameWin = false;
            isGameOver = false;
            isCameraShake = false;

            sUri = uri;

            camera = new Camera();

            utils = new Utils();
            
            GenerateGameplay(sUri);

            resourceManager = new Resources();

            Resources.Sounds["Intro"].Play();

            background = Resources.Textures["GameBackground1"];

            CreateClouds();

         }

        public override void HandleInput(InputManager input)
        {
            base.HandleInput(input);

            // Check if paused
            if (isPaused) return;

            v2MousePos = input.MousePosition;


            if (input.IsLeftDown())
            {
                isSelecting = true;
                // Check if mouse clicks nests
                foreach (Nest nests in Resources.Nests)
                {
                    if (nests.Player == Resources.Users["Player"])
                    {
                        nests.IsMouseOver(input);
                    }

                    Vector2 vector = input.MousePosition - nests.Center;
                    float radii = nests.Radius;

                    if (vector.LengthSquared() < radii * radii)
                    {
                        if (nests.Player == Resources.Users["Player"])
                        {
                            if (!nests.IsSelected)
                            {
                                nests.Select();
                            }
                        }
                    }
                }
            }
            else if (input.IsLeftUp() && isSelecting)
            {
                isSelecting = false;

                foreach (Nest nests in Resources.Nests)
                {
                    Vector2 vector = input.MousePosition - nests.Center;
                    float radii = nests.Radius;

                    if (vector.LengthSquared() < radii * radii)
                    {
                        foreach (Nest n in Resources.Nests)
                        {
                            if (n.IsSelected)
                            {
                                n.DeSelect();

                                if (nests != n)
                                {
                                    n.SpawnSwarm(nests);
                                }
                            } 
                        }
                    }
                }
            }
            else if (input.IsLeftUp())
            {
                if (!isSelecting)
                {
                    foreach (Nest n in Resources.Nests)
                    {
                        if (n.Player == Resources.Users["Player"])
                        {
                            n.IsMouseOver(input);
                        }
                        n.DeSelect();
                    }
                }
            }       
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (!isPaused)
            {
                resourceManager.Update(gameTime);

                
            }


            // Check if is won
            CheckIfGameWin(gameTime);
            CheckIfGameOver(gameTime);

            // Check for camera shake
            if (isCameraShake)
            {
                CameraShake(gameTime);
            }


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
                        
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.None, camera.TransformMatrix);

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(-10, -10, viewport.Width + 10, viewport.Height + 10);

            byte fade = TransitionAlpha;

            spriteBatch.Draw(background, fullscreen, new Color(fade, fade, fade));

            resourceManager.Draw(spriteBatch);           

            foreach (Nest n in Resources.Nests)
            {
                if (n.IsSelected)
                {
                    // x = cx + r * cos(a)
                    // y = cy + r * sin(a)

                    float angle = utils.VectorToAngle(n.Position, v2MousePos);
                    Vector2 pos = new Vector2((n.Position.X + n.Radius * (float)Math.Cos(angle)), (n.Position.Y + n.Radius * (float)Math.Sin(angle)));

                
                    Vector2 vector = v2MousePos - n.Position;
                    float radii = n.Radius;

                    if (vector.LengthSquared() > radii * radii)
                    {
                        LineBatch.DrawLine(spriteBatch, Resources.Textures["Line"], Color.Gray, pos, v2MousePos);
                    }
                }
            }

            spriteBatch.End();
            
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }

        private void GenerateGameplay(string xmlDoc)
        {
            // Read xml file
            XDocument xml = XDocument.Load(xmlDoc);

            // Create neutral and player
            Resources.Users.Add("Neutral", new User(1, 1, 1, Color.Gray));
            Resources.Users.Add("Player", new User(Resources.Player_Regen, Resources.Player_Power, Resources.Player_Speed, Color.DarkCyan));

            // Create ai
            int numAI = 1;

            foreach (var user in xml.Descendants("User"))
            {
                int regen = Convert.ToInt32(user.Element("Regen").Value);
                int power = Convert.ToInt32(user.Element("Power").Value);
                int speed = Convert.ToInt32(user.Element("Speed").Value);

                Color c = new Color((float)utils.NextDouble(), (float)utils.NextDouble(), (float)utils.NextDouble());

                while (c == Color.White)
                {
                    c = new Color(utils.NextDouble(), utils.NextDouble(), utils.NextDouble(), 1f);
                }

                Resources.Users.Add("Computer" + numAI, new User(regen, power, speed, c));
                numAI++;
            }
                        
            foreach (var nest in xml.Descendants("Nest"))
            {
                Vector2 position = new Vector2(Convert.ToInt32(nest.Element("X").Value), Convert.ToInt32(nest.Element("Y").Value));

                int index = Convert.ToInt32(nest.Element("Index").Value);

                int population = Convert.ToInt32(nest.Element("Population").Value);

                float scale = Convert.ToSingle(nest.Element("Scale").Value);

                if (index == 0)
                {
                    Resources.Nests.Add(new Nest(position, Resources.Users["Neutral"], population, scale));
                }
                else if(index == 1)
                {
                    Resources.Nests.Add(new Nest(position, Resources.Users["Player"], population, scale));
                }
                else if (index == 2)
                {
                    Resources.Nests.Add(new Nest(position, Resources.Users["Computer1"], population, scale));
                }
                else
                {
                    Resources.Nests.Add(new Nest(position, Resources.Users["Computer2"], population, scale));
                }
            }       
        }

        private void ClearGameplay()
        {
            Resources.Nests.Clear();
            Resources.Swarmites.Clear();
            Resources.Users.Clear();
            Resources.Clouds.Clear();
            Resources.Particles.Clear();
        }

        private void CheckIfGameOver(GameTime gameTime)
        {
            if (!isGameWin)
            {
                int i = 0;

                foreach (Nest n in Resources.Nests)
                {
                    if (n.Player == Resources.Users["Player"])
                    {
                        i++;
                    }
                }

                if (i <= 0)
                {
                    isGameOver = true;


                    fElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (fElapsedTime > 2000)
                    {
                        
                        ClearGameplay();
                        ScreenManager.RemoveScreen(this);
                        ScreenManager.AddScreen(new GameOverScreen());
                        fElapsedTime = 0;
                    }

                }
            }
        }

        private void CheckIfGameWin(GameTime gameTime)
        {
            if (!isGameOver)
            {
                int i = 0;

                foreach (Nest n in Resources.Nests)
                {
                    if (n.Player == Resources.Users["Neutral"] || n.Player == Resources.Users["Player"])
                    {
                        i++;
                    }
                }

                if (i >= Resources.Nests.Count)
                {
                    isGameWin = true;


                    fElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (fElapsedTime > 2000)
                    {
                        ClearGameplay();
                        ScreenManager.RemoveScreen(this);
                        ScreenManager.AddScreen(new VictoryScreen());
                        fElapsedTime = 0;
                    }
                }
            }
        }

        private void CameraShake(GameTime gameTime)
        {

            fCameraShakeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            fCameraShakeTimeEnd += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (fCameraShakeTime > 5)
            {
                fCameraShakeTime = 0;
                Vector2 shake = new Vector2(((utils.NextDouble() * 2) - 1), ((utils.NextDouble() * 2) - 1));

                camera.Position += shake;
            }

            if (fCameraShakeTimeEnd > 1500)
            {
                fCameraShakeTimeEnd = 0;

                camera.Position = Vector2.Zero;

                isCameraShake = false;
            }
        }

        private void CreateClouds()
        {
            for (int i = 0; i < 10; i++)
            {
                CreateCloud(new Vector2(utils.RandomBetween(1300, 2300), utils.RandomBetween(100, 700)));
            }
        }

        private void CreateCloud(Vector2 position)
        {
            for (int i = 0; i < 50; i++)
            {
                Resources.Clouds.Add(new Cloud(new Vector2(position.X + utils.RandomBetween(-75, 75), position.Y + utils.RandomBetween(-35, 35))));
            }
        }

    }
}
