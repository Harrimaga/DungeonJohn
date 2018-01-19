using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public class Boss :  SpriteGameObject
{
    protected float maxhealth, statmultiplier;
    public float health;
    protected int expGive, LevelofBoss;
    //protected float attack;
    //protected float attackspeed;
    //protected float range;
    protected Vector2 basevelocity = new Vector2((float)0.5, (float)0.5);
    public Vector2 PlayerOrigin;
    public Texture2D playersprite;
    Vector2 Roomposition;
    public SpriteEffects Effects;
    HealthBar healthbar;
    public bool alive = true;

    public Boss(Vector2 startPosition, Vector2 roomposition, string assetname, int difficulty = 0, int layer = 0, string id = "Boss") : base(assetname, layer, id)
    {
        position = startPosition;
        healthbar = new HealthBar(health, maxhealth, position, false, true);
        Roomposition = roomposition;
        statmultiplier = difficulty / 10 + 1;
        LevelofBoss = difficulty;
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerFront");
    }

    public override void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, health, maxhealth, new Vector2(Roomposition.X * PlayingState.currentFloor.currentRoom.roomwidth, Roomposition.Y * PlayingState.currentFloor.currentRoom.roomheight)); //<---

        List<GameObject> RemoveBullets = new List<GameObject>();

        foreach (Bullet bullet in PlayingState.player.bullets.Children)
            if (CollidesWith(bullet))
            {
                health -= PlayingState.player.attack;
                RemoveBullets.Add(bullet);
            }
         
        foreach (Bullet bullet in RemoveBullets)
            PlayingState.player.bullets.Remove(bullet);

        RemoveBullets.Clear();
        
        PlayerOrigin = new Vector2(PlayingState.player.position.X + playersprite.Width / 2, PlayingState.player.position.Y + playersprite.Height / 2);
        base.Update(gameTime);
    }

    public void FinalStage()
    {
        if (health <= 0 && alive)
        {
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position + new Vector2(30, 50));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position + new Vector2(-40, 10));
            PlayingState.player.exp += expGive;
            alive = false;
            if (PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].Type == "bossroom")
                EndRoom.cleared = true;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
    }
}
