using UnityEngine;

public static class VectorHelper
{
    public static Vector3 Floor(Vector3 source)
    {
        return new Vector3(Mathf.Floor(source.x), Mathf.Floor(source.y), Mathf.Floor(source.z));
    }

    public static Vector2 Floor(Vector2 source)
    {
        return new Vector2(Mathf.Floor(source.x), Mathf.Floor(source.y));
    }

    public static Vector2 Flatten(Vector3 source)
    {
        return Floor(new Vector2(source.x, source.z));
    }
}
