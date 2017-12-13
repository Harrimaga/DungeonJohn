using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    static public Vector2 CameraPosition = new Vector2(0, 0);

    static public void CameraPlacement(GameTime gameTime, int roomwidth, int roomheight)
    {
        CameraPosition.X = MathHelper.Clamp(PlayingState.player.position.X - 800, 0, 500 - 800);
        CameraPosition.Y = MathHelper.Clamp(PlayingState.player.position.Y - 480 / 2, 0, 400 - 480);
    }

    static public void Reset()
    {
        CameraPosition = Vector2.Zero;
    }
}


