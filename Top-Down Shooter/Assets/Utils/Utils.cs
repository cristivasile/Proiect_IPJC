using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Quaternion GetRelativeRotation(Vector3 origin, Vector3 target)
    {
        var orientation = target - origin;
        var angle = -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, angle);
    }
}
