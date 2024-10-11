using Microsoft.Xna.Framework;
namespace G;

public class EarthScene : Component
{
  public static EarthScene Instance { get; private set; } = new();
  public override void LoadContent()
  {
    base.LoadContent();
    Core.Input.PushWorld(Def.Input.World.Battleground);
    Core.Container.Add(Def.Container.Battleground, UFO.Instance);
    Core.Layer.Add(Def.Layer.Battleground, UFO.Instance);
    UFO.Instance.Position = new Vector2(100, 100);
  }

  public override void Update(GameTime gameTime)
  {
  }

  public override void Draw(GameTime gameTime)
  {
    base.Draw(gameTime);
  }
}