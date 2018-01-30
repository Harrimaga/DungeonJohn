using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Boss :  SpriteGameObject
{
    protected float maxhealth, statmultiplier, contactdamage;
    public float health;
    protected int LevelofBoss, poisoncounter = 0, hitcounter = 0;
    protected float expGive;
    int counter = 0;
    protected Vector2 basevelocity = new Vector2((float)0.5, (float)0.5);
    protected Vector2 PlayerOrigin;
    protected Texture2D playersprite;
    Vector2 Roomposition;
    HealthBar healthbar;
    public bool alive = true;
    static public bool endless = true;
    protected Color color = Color.White;


    public Boss(Vector2 startPosition, Vector2 roomposition, string assetname, int difficulty = 0, int layer = 0, string id = "Boss") : base(assetname, layer, id)
    {
        position = startPosition;
        healthbar = new HealthBar(health, maxhealth, position, false, true);
        Roomposition = roomposition;
        statmultiplier = difficulty / 10 + 1;
        LevelofBoss = difficulty;
        playersprite = GameEnvironment.assetManager.GetSprite("Sprites/Characters/PlayerDown");
    }

    public override void Update(GameTime gameTime)
    {
        healthbar.Update(gameTime, health, maxhealth, new Vector2(Roomposition.X * PlayingState.currentFloor.currentRoom.roomwidth, Roomposition.Y * PlayingState.currentFloor.currentRoom.roomheight));

        List<GameObject> RemoveBullets = new List<GameObject>();

        foreach (Bullet bullet in PlayingState.player.bullets.Children)
            if (CollidesWith(bullet))
            {
                health -= PlayingState.player.attack;
                if (PlayingState.player.poisonchance > 0 && bullet.poisonbullet)
                    poisoncounter = 5000;
                hitcounter = 200;
                RemoveBullets.Add(bullet);
            }
         
        foreach (Bullet bullet in RemoveBullets)
            PlayingState.player.bullets.Remove(bullet);

        RemoveBullets.Clear();
        PlayerCollision();
        PlayerOrigin = new Vector2(PlayingState.player.position.X + playersprite.Width / 2, PlayingState.player.position.Y + playersprite.Height / 2);
        base.Update(gameTime);
        if (hitcounter > 0)
        {
            hitcounter -= gameTime.ElapsedGameTime.Milliseconds;
            color = Color.Salmon;
        }
        else if (poisoncounter > 0)
        {
            color = Color.YellowGreen;
            if (poisoncounter % 600 <= 20 && poisoncounter < 5000)
            {
                health -= 4;
                color = Color.Salmon;
            }
            poisoncounter -= gameTime.ElapsedGameTime.Milliseconds;
        }
        else
            color = Color.White;
    }

    public void FinalStage()
    {
        if (health <= 0 && alive)
        {
            if (EndRoom.finalboss)
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position, true);
            else
            {
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position + new Vector2(30, 50));
                PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position + new Vector2(-40, 10));
            }
            PlayingState.player.exp += expGive;
            alive = false;
            if (PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].Type == "bossroom")
                EndRoom.cleared = true;
            GameObjectList.RemovedObjects.Add(this);
        }
    }

    public void PlayerCollision()
    {
        counter--;
        if (CollidesWith(PlayingState.player))
        {
            velocity = Vector2.Zero;
           
            if (counter <= 0)
            {
                PlayingState.player.TakeDamage(contactdamage);
                counter = 1000;
            }
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
    }
}
