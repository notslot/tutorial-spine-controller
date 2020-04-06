using System;
using System.Collections.Generic;

public static class DirectionUtil
{
  #region Constants

  private static readonly Dictionary<Direction, Direction> FLIPPABLE =
    new Dictionary<Direction, Direction>
    {
      { Direction.NW, Direction.NE },
      { Direction.W, Direction.E },
      { Direction.SW, Direction.SE }
    };

  #endregion


  #region Methods

  public static bool IsFlippable (Direction direction)
  {
    return FLIPPABLE.ContainsKey(direction);
  }

  public static string GetName (Direction direction)
  {
    return Enum.GetName(typeof(Direction), direction);
  }

  public static string GetFlippedName (Direction direction)
  {
    return GetName(FLIPPABLE[direction]);
  }

  public static string GetActualName (Direction direction)
  {
    return IsFlippable(direction)
      ? GetFlippedName(direction)
      : GetName(direction);
  }

  public static Direction ConvertDegrees (float degrees)
  {
    if ( degrees > -22.5f && degrees <= 22.5f )
      return Direction.E;
    if ( degrees > 22.5f && degrees <= 67.5f )
      return Direction.NE;
    if ( degrees > 67.5f && degrees <= 112.5f )
      return Direction.N;
    if ( degrees > 112.5f && degrees <= 157.5f )
      return Direction.NW;
    if ( degrees > 157.5f || degrees <= -157.5f )
      return Direction.W;
    if ( degrees > -67.5f && degrees <= -22.5f )
      return Direction.SE;
    if ( degrees > -112.5f && degrees <= -67.5f )
      return Direction.S;

    return Direction.SW;
  }

  #endregion
}