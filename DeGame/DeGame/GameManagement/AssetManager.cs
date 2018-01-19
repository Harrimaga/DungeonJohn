using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        try
        {
            return contentManager.Load<Texture2D>(assetName);
        }
        catch (Exception e)
        {
            return null;
        }
        
    }
    public SpriteFont GetFont(string assetName)
    {
        if (assetName == "")
        {
            return null;
        }
        return contentManager.Load<SpriteFont>(assetName);
    }
}

