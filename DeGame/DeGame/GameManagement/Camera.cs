using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

    public class Camera
    {
        /// <summary>
        /// Gets or sets the zoom level
        /// </summary>
        public float Zoom
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the camera position
        /// </summary>
        public static Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rotation of the camera
        /// </summary>
        public float Rotation
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the bounds
        /// </summary>
        private Rectangle Bounds
        {
            get; set;
        }
        
        /// <summary>
        /// Calculates the transformation matrix
        /// </summary>
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
        

        /// <summary>
        /// Camera constructor
        /// </summary>
        /// <param name="viewport">The viewport</param>
        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
        }

        /// <summary>
        /// Resets the camera
        /// </summary>
        public static void reset()
        {
            Position = new Vector2(GameEnvironment.Dimensions.X / 2, GameEnvironment.Dimensions.Y / 2);
        }
    }