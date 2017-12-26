using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Camera
    {
        public float Zoom
        {
            get; set;
        }
        public static Vector2 Position
        {
            get;
            set;
        }
        public float Rotation
        {
            get; set;
        }
        private 
        Bounds
        {
            get; set;
        }

        public Matrix TransformMatrix
        {
            get
            {
                return
                    Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateScale(Zoom) *
                    Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));
            }
        }

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
        }

        public static void reset()
        {
            Position = new Vector2(GameEnvironment.Dimensions.X / 2, GameEnvironment.Dimensions.Y / 2);
        }
    }