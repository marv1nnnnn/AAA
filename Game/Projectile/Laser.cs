using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G;

public class Laser : Component
{
  public Vector2 SourcePosition { get; private set; }
  public Vector2 Direction { get; private set; }
  public float MoveSpeed { get; set; } = 64f;
  public Color Color { get; private set; }
  protected List<VertexPositionColor> Vertices { get; set; } = [];

  public Laser(float scale, Vector2 sourcePos, Vector2 direction, Color color, float distance = 100f)
  {
    Scale = new Vector2(scale);
    Position = sourcePos;
    SourcePosition = sourcePos;
    Direction = Vector2.Normalize(direction);
    Color = color;
    float numberOfSegments = 30;
    float segmentLength = distance / numberOfSegments;

    Vertices.Add(new VertexPositionColor(new Vector3(SourcePosition, 0), color));
    var simplex = new OpenSimplexNoise();
    var perpendicular = new Vector2(direction.Y, -direction.X);
    var maxAmplitude = 64f;
    for (int i = 0; i <= numberOfSegments; i++)
    {
      var dir = sourcePos + direction * i * segmentLength;
      var noise = (float)simplex.Evaluate(dir.X, dir.Y);
      var newVec = noise * (i / numberOfSegments) * maxAmplitude * perpendicular + dir;

      Vertices.Add(new VertexPositionColor(new Vector3(newVec, 0), color));
    }
    Vertices.Add(new VertexPositionColor(new Vector3(SourcePosition + distance * direction, 0), color));
    EnablePrimitiveBatch = true;
  }

  public override void Update(GameTime gameTime)
  {
    Opacity = MathHelper.Lerp(Opacity, 0, 0.2f);
    for (int i = 0; i < Vertices.Count; i++)
    {
      VertexPositionColor v = Vertices[i];
      Vertices[i] = new VertexPositionColor(v.Position, Color * Opacity);
    }
    if (Opacity < 0.01f)
    {
      Die();
    }
  }

  public override void Draw(GameTime gameTime)
  {
    Core.GraphicsDevice.DrawUserPrimitives(
        PrimitiveType.LineStrip,
        Vertices.ToArray(),
        0,
        Vertices.Count - 1
    );
  }
}