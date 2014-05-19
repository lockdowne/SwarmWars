using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SwarmWars.Logic
{
    /// <summary>
    /// Line Batch
    /// For drawing lines in a spritebatch
    /// </summary>
    static public class LineBatch
    {
        static public void DrawLine(SpriteBatch batch, Texture2D texture,  Color color,
                                    Vector2 point1, Vector2 point2)
        {

            DrawLine(batch, texture, color, point1, point2, 0);
        }

        /// <summary>
        /// Draw a line into a SpriteBatch
        /// </summary>
        /// <param name="batch">SpriteBatch to draw line</param>
        /// <param name="color">The line color</param>
        /// <param name="point1">Start Point</param>
        /// <param name="point2">End Point</param>
        /// <param name="Layer">Layer or Z position</param>
        static public void DrawLine(SpriteBatch batch, Texture2D texture, Color color, Vector2 point1,
                                    Vector2 point2, float Layer)
        {

            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = (point2 - point1).Length();

            batch.Draw(texture, point1, null, new Color(color.R, color.G, color.B, 0.5f),
                       angle, Vector2.Zero, new Vector2(length / texture.Width, 1),
                       SpriteEffects.None, Layer);
        }
    }
}
