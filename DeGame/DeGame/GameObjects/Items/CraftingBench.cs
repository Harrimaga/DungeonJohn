using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            spriteBatch.DrawString(GameEnvironment.assetManager.GetFont("Sprites/SpelFont"), "Press spacebar to craft", position + new Vector2(-60,60), Color.White);
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

