using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
namespace G;

public enum UFOState { Idle, Shoot, Suck }

public class UFO : Component
{
  public static UFO Instance { get; private set; } = new();
  public float Speed { get; private set; } = 1;
  public float SuckingSpeed { get; private set; } = 1;
  public float SuckingRange { get; private set; } = 1;
  public float ShootingCooldown { get; private set; }
  public List<Component> Projectiles { get; private set; } = [];
  private Texture2D? Texture;
  public UFOState State { get; private set; } = UFOState.Idle;
  public override void LoadContent()
  {
    Texture = Core.Texture.LoadTexture("UFO/UFO1");
    Size = new Vector2(32, 32);
    base.LoadContent();
  }

  public override void Update(GameTime gameTime)
  {
    UpdateMove(gameTime);
    var shooting = UpdateShoot(gameTime);
    if (shooting)
    {
      State = UFOState.Shoot;
    }
    else
    {
      var sucking = UpdateSuck(gameTime);
      if (sucking)
      {
        State = UFOState.Suck;
      }
      else
      {
        State = UFOState.Idle;
      }
    }

    UpdateRotation(gameTime);
    UpdateCooldown(gameTime);
  }

  private void UpdateCooldown(GameTime gameTime)
  {
    if (ShootingCooldown > 0)
    {
      ShootingCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
      if (ShootingCooldown < 0)
      {
        ShootingCooldown = 0;
      }
    }
  }

  private void UpdateRotation(GameTime gameTime)
  {
    Rotation += 0.02f;
    Rotation %= MathHelper.TwoPi;
  }

  private bool UpdateMove(GameTime gameTime)
  {
    var vector = Vector2.Zero;
    if (Core.Input.IsActive(Def.Input.Action.Up, Def.Input.World.Battleground))
    {
      vector += new Vector2(0, -1);
    }
    if (Core.Input.IsActive(Def.Input.Action.Down, Def.Input.World.Battleground))
    {
      vector += new Vector2(0, 1);
    }
    if (Core.Input.IsActive(Def.Input.Action.Left, Def.Input.World.Battleground))
    {
      vector += new Vector2(-1, 0);
    }
    if (Core.Input.IsActive(Def.Input.Action.Right, Def.Input.World.Battleground))
    {
      vector += new Vector2(1, 0);
    }
    if (vector == Vector2.Zero)
    {
      return false;
    }

    vector = Vector2.Normalize(vector);

    Position += vector * Speed;
    CorrectPosition();
    return true;
  }

  private bool UpdateShoot(GameTime gameTime)
  {
    if (!Core.Input.IsActive(Def.Input.Action.Shoot, Def.Input.World.Battleground))
    {
      return false;
    }

    if (ShootingCooldown > 0)
    {
      return false;
    }

    var projectile = new Laser(1, Position, new Vector2(0, -1), Palette.Green[5], 100f);
    Projectiles.Add(projectile);
    Core.Container.Add(Def.Container.Battleground, projectile);
    Core.Layer.Add(Def.Layer.Battleground, projectile);

    return true;
  }

  private bool UpdateSuck(GameTime gameTime)
  {
    if (!Core.Input.IsActive(Def.Input.Action.Suck, Def.Input.World.Battleground))
    {
      return false;
    }

    return true;
  }

  public override void Draw(GameTime gameTime)
  {
    if (State == UFOState.Suck)
    {
      var polygon = new Polygon([
        new Vector2(-4 * SuckingRange, 0),
        new Vector2(4 * SuckingRange, 0),
        new Vector2(32 * SuckingRange, -64 * SuckingRange),
        new Vector2(-32 * SuckingRange, -64 * SuckingRange),
      ]);
      Core.Sb.DrawPolygon(Position, polygon, Color.White);
    }
    Core.Sb.Draw(
      texture: Texture,
      position: Position,
      sourceRectangle: null,
      color: Color.White,
      rotation: Rotation,
      origin: Origin,
      scale: Scale,
      effects: SpriteEffects.None,
      layerDepth: 0
    );
    base.Draw(gameTime);
  }

  private void CorrectPosition()
  {
    if (Position.X - Size.X / 2.0f < 0)
    {
      Position = new Vector2(Size.X / 2.0f, Position.Y);
    }
    if (Position.Y - Size.Y / 2.0f < 0)
    {
      Position = new Vector2(Position.X, Size.Y / 2.0f);
    }
    if (Position.X + Size.X / 2.0f > Core.ScreenWidth)
    {
      Position = new Vector2(Core.ScreenWidth - Size.X / 2.0f, Position.Y);
    }
    if (Position.Y + Size.Y / 2.0f > Core.ScreenHeight)
    {
      Position = new Vector2(Position.X, Core.ScreenHeight - Size.Y / 2.0f);
    }
  }

}