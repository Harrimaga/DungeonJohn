using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    static public Vector2 CameraPosition = new Vector2(0, 0);



    static public void Reset()
    {
        CameraPosition = Vector2.Zero;
    }
}


