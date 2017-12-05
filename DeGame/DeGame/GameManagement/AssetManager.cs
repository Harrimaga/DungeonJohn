using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class AssetManager
{ 
    protected ContentManager contentManager;
    public AssetManager(ContentManager content)
    {
        contentManager = content;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
        {
            return null;
        }
        return contentManager.Load<Texture2D>(assetName);
    }
}

