using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float Map(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float ConeInertiaMoment(float mass, float radius) {
        return 0.3f * mass * radius * radius;
    }
}
