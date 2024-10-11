using Microsoft.Xna.Framework;

namespace G;

public class Game1 : Game
{
    public Game1()
    {
        Core.Init(Content, this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Core.InitializeGraphics();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Core.LoadContent();
        Core.Container.Add(Def.Container.Scene, EarthScene.Instance);
        Core.Layer.Add(Def.Layer.SceneRoot, EarthScene.Instance);
    }

    protected override void Update(GameTime gameTime)
    {
        if (IsActive == false)
        {
            return;
        }

        bool isBlocked = Core.Update(gameTime);

        if (isBlocked)
        {
            base.Update(gameTime);
            return;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Core.Draw(gameTime);
        base.Draw(gameTime);
    }
}
