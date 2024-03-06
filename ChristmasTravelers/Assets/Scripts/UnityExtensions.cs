using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class UnityExtensions 
{
    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y);
    }
}
