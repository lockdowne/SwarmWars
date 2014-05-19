using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SwarmWars.Logic
{
    public class Camera
    {
        private Vector2 v2Position;
        private float fZoom;
        private float fRotation;

        public Vector2 Position
        {
            get { return v2Position; }
            set { v2Position = value; }
        }

        public float Zoom
        {
            get { return fZoom; }
            set { fZoom = value; }
        }

        public float Rotation
        {
            get { return fRotation; }
            set { fRotation = value; }
        }

        public Camera()
        {
            v2Position = Vector2.Zero;
            fZoom = 1f;
        }

        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateRotationZ(fRotation) * Matrix.CreateScale(fZoom) * Matrix.CreateTranslation(v2Position.X, v2Position.Y, 0);
            }
        }

        public void LockToVector(Vector2 position)
        {
            v2Position.X = position.X - (Consts.Screen_Width / 2);
            v2Position.Y = position.Y - (Consts.Screen_Height / 2);

        }
    }
}
