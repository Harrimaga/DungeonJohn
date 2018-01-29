using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Boss :  SpriteGameObject
{
    protected float maxhealth, statmultiplier;
    public float health;
    protected int expGive, LevelofBoss, poisoncounter = 0;
    protected Vector2 basevelocity = new Vector2((float)0.5, (float)0.5);
    protected Vector2 PlayerOrigin;
    protected Texture2D playersprite;
    Vector2 Roomposition;
    HealthBar healthbar;
    public bool alive = true;
    protected Color color = Color.White;
    int counter = 10;
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
                if (PlayingState.player.VialOfPoison && bullet.poisonbullet)
                    poisoncounter = 350;
                RemoveBullets.Add(bullet);
            }
         
        foreach (Bullet bullet in RemoveBullets)
            PlayingState.player.bullets.Remove(bullet);

        RemoveBullets.Clear();
        PlayerCollision();
        //SolidCollision();
        PlayerOrigin = new Vector2(PlayingState.player.position.X + playersprite.Width / 2, PlayingState.player.position.Y + playersprite.Height / 2);
        base.Update(gameTime);
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Center.X, PlayingState.player.collisionhitbox.Top)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.Y++;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Center.X, PlayingState.player.collisionhitbox.Bottom)))
            while (CollidesWith(PlayingState.player))
            {
                PlayingState.player.position.Y--;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Left, PlayingState.player.collisionhitbox.Center.Y)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.X++;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Right, PlayingState.player.collisionhitbox.Center.Y)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.X--;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }

        if (poisoncounter > 0)
        {
            if (poisoncounter % 75 == 0 && poisoncounter < 350)
                health -= 4;
            poisoncounter--;
            color = Color.YellowGreen;
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
        if (CollidesWith(PlayingState.player))
        {
            velocity = Vector2.Zero;
            counter--;
            if (counter <= 0)
            {
                PlayingState.player.health -= 1;
                counter = 100;
            }
        }

        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Center.X, PlayingState.player.collisionhitbox.Top)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.Y++;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Center.X, PlayingState.player.collisionhitbox.Bottom)))
            while (CollidesWith(PlayingState.player))
            {
                PlayingState.player.position.Y--;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Left, PlayingState.player.collisionhitbox.Center.Y)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.X++;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }
        if (BoundingBox.Contains(new Vector2(PlayingState.player.collisionhitbox.Right, PlayingState.player.collisionhitbox.Center.Y)))
            while (BoundingBox.Intersects(PlayingState.player.collisionhitbox))
            {
                PlayingState.player.position.X--;
                PlayingState.player.collisionhitbox = new Rectangle((int)PlayingState.player.position.X, (int)PlayingState.player.position.Y + 20, PlayingState.player.BoundingBox.Width, PlayingState.player.BoundingBox.Width);
            }

    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
    }
}
