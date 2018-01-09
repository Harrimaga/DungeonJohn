using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ChasingEnemy : Enemy
{
    public ChasingEnemy(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Enemy") : base(startPosition, roomposition, layer, id)
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (CollidesWith(PlayingState.player))
        {
            counter--;
            if (counter == 0)
            {
                // velocity = Vector2.Zero;
                PlayingState.player.health -= 0;
                counter = 100;
            }
        }

        if (!CollidesWith(PlayingState.player))
        {
            velocity = basevelocity;
        }

        if (PlayingState.currentFloor.currentRoom.position == Roomposition)
            Chase();

        //if (health <= 0 && alive == true && PlayingState.currentFloor.currentRoom.position == Roomposition)
        //{
        //    PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
        //    PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
        //    PlayingState.player.exp += expGive;
        //    //alive = false;
        //    GameObjectList.RemovedObjects.Add(this);
        //}
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/BearEnemy"), position, null, Color.White, 0f, Vector2.Zero, 1f, Effects, 0f);
    }
}


   