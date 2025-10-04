using UnityEngine;
using UnityEngine.InputSystem;

public static class Utils
{
    public static Vector2 MousePos(Camera cam)
    {
        return cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
