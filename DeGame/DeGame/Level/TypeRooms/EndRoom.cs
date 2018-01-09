using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class EndRoom : Room
{
    public bool cleared = false;
    int currentlevel;

    public EndRoom(int roomListIndex, int a, int b, int layer = 0, string id = "") : base(a, b, layer)
    {

    }

    public virtual void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        currentlevel = PlayingState.currentFloor.CurrentLevel;
        //TODO check player volgende floor mag nextFloor true maken
    }

    public void ChooseBoss(int currentlevel)
    {
        // if (currentlevel == 1)

    }

    public void ChooseBossPosition(Boss boss)
    {
        //
    }

    public override void CheckExit()
    {
        base.CheckExit();
        Vector2 MiddleofPlayer = new Vector2(PlayingState.player.position.X + GameEnvironment.assetManager.GetSprite("Sprites/Random").Width / 2, PlayingState.player.position.Y + GameEnvironment.assetManager.GetSprite("Sprites/Random").Height / 2);
        if (cleared)
        {
            if (MiddleofPlayer.X >= Exit.X && MiddleofPlayer.X <= Exit.X + CellWidth)
                if (MiddleofPlayer.Y >= Exit.Y && MiddleofPlayer.Y <= Exit.Y + CellHeight)
                    PlayingState.currentFloor.NextShop();
        }
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
    }
}

