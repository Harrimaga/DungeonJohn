﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

public class Boss :  SpriteGameObject
{
    public float health = 300;
    protected float maxhealth = 300;
    protected int expGive = 240;
    //protected float attack;
    //protected float attackspeed;
    //protected float range;
    protected Vector2 basevelocity = new Vector2((float)0.5, (float)0.5);
    public SpriteEffects Effects;
    HealthBar healthbar;
    Vector2 Roomposition;

    public Boss(Vector2 startPosition, Vector2 roomposition, int layer = 0, string id = "Boss") : base("Sprites/Enemies/Boss", layer, id)
    {
        position = startPosition;
        healthbar = new HealthBar(health, maxhealth, position, false, true);
        Roomposition = roomposition;
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

        if (health <= 0)
        {
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].enemycounter--;
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position);
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position + new Vector2(30, 50));
            PlayingState.currentFloor.floor[(int)Roomposition.X, (int)Roomposition.Y].DropConsumable(position + new Vector2(-40, 10));
            PlayingState.player.exp += expGive;
            GameObjectList.RemovedObjects.Add(this);
        }

        RemoveBullets.Clear();
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);
        healthbar.Draw(spriteBatch);
    }
}
