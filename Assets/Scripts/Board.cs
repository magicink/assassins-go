using UnityEngine;

public class Board : MonoBehaviour
{
    public static float Spacing = 2f;
    public static readonly Vector2[] Directions = new Vector2[]
    {
        new Vector2(Spacing, 0f),
        new Vector2(-Spacing, 0f),
        new Vector2(0f, Spacing),
        new Vector2(0f, -Spacing), 
    };
}
