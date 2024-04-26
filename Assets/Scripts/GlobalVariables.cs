using UnityEngine;

public static class Globals
{
  public static bool gravityOff = false;
  public static float flyVelocity = 0.1f;
  public static bool lights = false;
  public static bool earthSpin = false;
  public static bool landing = false;

  // 0 = Start
  // 1 = Takeoff
  // 2 = Space
  // 3 = Landed
  public static int phase = 0;
}
