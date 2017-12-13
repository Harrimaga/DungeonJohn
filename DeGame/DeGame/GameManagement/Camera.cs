using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    static public Vector2 CameraPosition = new Vector2(0, 0);

    static public void CameraPlacement(GameTime gameTime, Player player, int roomwidth, int roomheight)
    {
        CameraPosition.X = MathHelper.Clamp(player.position.X /*- GameEnvironment.graphics.PreferredBackBufferWidth / 2*/, 0, roomwidth/* - GameEnvironment.graphics.PreferredBackBufferWidth*/);
        CameraPosition.Y = MathHelper.Clamp(player.position.Y /*- GameEnvironment.graphics.PreferredBackBufferHeight / 2*/, 0, roomheight/* - GameEnvironment.graphics.PreferredBackBufferHeight*/);
    }

    static public void Reset()
    {
        CameraPosition = Vector2.Zero;
    }
}


