using UnityEngine;

public static class VectorHelper
{
    public static Vector3 Round(Vector3 source)
    {
        return new Vector3(Mathf.Round(source.x), Mathf.Round(source.y), Mathf.Round(source.z));
    }

    public static Vector2 Round(Vector2 source)
    {
        return new Vector2(Mathf.Round(source.x), Mathf.Round(source.y));
    }
}
