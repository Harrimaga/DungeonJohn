using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CraftingBench : SpriteGameObject
{
    public CraftingBench(Vector2 startPosition, bool Price, int layer = 0, string id = "Anvil")
    : base("Sprites/Items/Anvil", layer, id)
    {
        position = startPosition;
    }
    public override void Update(GameTime gameTime)
    {
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(GameEnvironment.assetManager.GetSprite("Sprites/items/Anvil"), position);
        if (CollidesWith(PlayingState.player))
        {
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Press SpaceBar to Craft", position + new Vector2(-60,100), Color.White);
        }
    }
    public override void HandleInput(InputHelper inputHelper, GameTime gameTime)
    {
        if(CollidesWith(PlayingState.player)&& inputHelper.KeyPressed(Keys.Space))
        {
            GameEnvironment.gameStateManager.SwitchTo("Crafting");
        }
    }
}

