using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DayNightManager
{
    public static bool _day;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  public static void InitDay()
    {
        _day = true;
    }
}
