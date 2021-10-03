using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorPicker
{
    public static Color GetColor(int type)
    {
        switch (type)
        {
            case 1: return GenerateColor(10, 35, 255, 320);
            case 2: return GenerateColor(150, 210, 70, 100);
            default: return GenerateColor(30, 60, 150, 210);
        }
    }

    private static Color GenerateColor(float a, float b, float c, float d)
    {
        float h1 = Random.Range(a/360, b/360);
        float h2 = Random.Range(c/360, d/360);
        float s = Random.Range(.65f, .95f);
        float v = Random.Range(.95f, 1);
        return Random.Range(0.0f, 1) < 0.5f ? Color.HSVToRGB(h1, s, v) : Color.HSVToRGB(h2, s, v);
    }

}
