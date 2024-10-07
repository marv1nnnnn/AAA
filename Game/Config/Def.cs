using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace G;

public class Def
{
  #region Core Config
  public static class Screen
  {
    public static readonly ITheme Theme = new ApolloTheme();
    public static Color BackgroundColor => Palette.Blue[1];
    public static readonly int TargetScreenWidth = 1280;
    public static readonly int TargetScreenHeight = 720;
    public static readonly int ScreenWidth = 640;
    public static readonly int ScreenHeight = 360;
    public static readonly int PPU = 16;
  }

  public static readonly List<(string, string)> Fonts = [
    ("zpix", @"Content/Font/zpix.ttf"),
    ("fusion8", @"Content/Font/fusion8.ttf"),
    ("fusion10", @"Content/Font/fusion10.ttf"),
    ("console", @"Content/Font/console.ttf"),
  ];

  public static class Input
  {
    public enum Action
    {
      CursorPressed = 1,
      CursorDown = 2,
      CursorReleased = 3,
      CursorUp = 4,
      CursorRightReleased = 5,
      DismissWindow = 6,
    }

    public enum World
    {
      Battleground = 1,
      DiceViewer = 2,
    }

    public static readonly Dictionary<Action, List<string>> Bindings = new()
    {
      { Action.CursorPressed, ["MouseLeftPressed"] },
      { Action.CursorDown, ["MouseLeftDown"] },
      { Action.CursorReleased, ["MouseLeftReleased"] },
      { Action.CursorUp, ["MouseLeftUp"] },
      { Action.CursorRightReleased, ["MouseRightReleased"] },
      { Action.DismissWindow, ["Escape", "MouseRightReleased"] },
    };
  }


  public static class Camera
  {
    public static readonly int ScrollHitSlop = 8;
    public static readonly float Velocity = 400;
    public static readonly float MinVelocity = 5;
    public static readonly float Deceleration = 0.85f;
  }

  public enum Container
  {
    Scene = 1,
    DiceViewer = 2,
    Battleground = 3,
    BattlegroundUI = 4,
    Background = 5,
  }

  public enum PhysicsWorld
  {
    Main = 1
  }

  public enum Layer
  {
    DevUI = 1,
    SceneRoot = 2,
    DiceViewer = 3,
    Dice = 7,
    BattlegroundUI = 9,
    Board = 11,
    Background = 100,
  }

  public static readonly Dictionary<Layer, Dictionary<string, object>> LayerConfig = new()
  {
    [Layer.DevUI] = new Dictionary<string, object>
    {
      { "IsCameraFixed", true }
    }
  };

  #endregion Core Config

  public static class Board
  {
    public static readonly int GridSize = 48;
  }
}