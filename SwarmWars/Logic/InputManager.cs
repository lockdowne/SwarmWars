using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SwarmWars.Logic
{
    public class InputManager
    {
        private MouseState ms;

        private Vector2 v2MousePosition;

        private bool isClicked;

        public InputManager()
        {
            ms = new MouseState();
        }

        public Vector2 MousePosition
        {
            get { return v2MousePosition; }
        }

        public bool IsLeftDown()
        {
            if (ms.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsLeftUp()
        {
            if (ms.LeftButton == ButtonState.Released)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRightDown()
        {
            if (ms.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Click()
        {
            if (!isClicked)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void Update()
        {
            ms = Mouse.GetState();

            v2MousePosition = new Vector2(ms.X, ms.Y);

            if (ms.LeftButton == ButtonState.Released)
            {
                isClicked = false;
            }
        }
    }
}
