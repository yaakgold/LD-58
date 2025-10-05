using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Utils
{
    public static Vector2 MousePos(Camera cam)
    {
        return cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public static Vector2 GetRandomInUnitWithDist(float dist = 1)
    {
        return Random.insideUnitCircle.normalized * dist;
    }

    public static GameObject GetRandom(this List<GameObject> list)
    {
        return list.Count == 0 ? null : list[Random.Range(0, list.Count)];
    }
}
