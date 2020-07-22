using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static float Spacing = 2f;

    public static readonly Vector2[] Directions = {
        new Vector2(Spacing, 0f),
        new Vector2(-Spacing, 0f),
        new Vector2(0f, Spacing),
        new Vector2(0f, -Spacing), 
    };

    private List<Node> _nodes  = new List<Node>();

    public List<Node> Nodes => _nodes;

    private void InitializeNodes()
    {
        _nodes = new List<Node>(FindObjectsOfType<Node>());
    }

    private void Awake()
    {
        InitializeNodes();
    }

    private void Start()
    {
        foreach (var node in Nodes)
        {
            node.Neighbors = Node.FindNeighbors(node, Nodes);
        }
    }
}
