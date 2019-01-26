using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NeonColor
{
    public static readonly Color green = new Color(110, 250, 150);
    public static readonly Color blue = new Color(127,89,245);
    public static readonly Color red = new Color(251, 94, 244);

    public static List<Color> Colors = new List<Color>() { green, blue, red };
}
