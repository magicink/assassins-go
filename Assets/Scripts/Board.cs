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

    private PlayerInput _playerInput;

    public Node PlayerNode { get; private set; }

    private List<Node> Nodes { get; set; } = new List<Node>();

    private void InitializeNodes()
    {
        Nodes = new List<Node>(FindObjectsOfType<Node>());
    }

    private void Awake()
    {
        _playerInput = FindObjectOfType<PlayerInput>().GetComponent<PlayerInput>();
        MovementController.OnMoveEnd += HandleMoveEnd;
        InitializeNodes();
    }

    private void HandleMoveEnd()
    {
        PlayerNode = FindPlayerNode();
    }

    private void Start()
    {
        PlayerNode = FindPlayerNode();
        foreach (var node in Nodes)
        {
            node.Neighbors = Node.FindNeighbors(node, Nodes);
        }
    }

    public Node FindNodeAt(Vector3 target)
    {
        var boardPosition = VectorHelper.Flatten(target);
        return Nodes.Find(n => n.Coordinates == boardPosition);
    }

    public Node FindPlayerNode()
    {
        return _playerInput ? FindNodeAt(_playerInput.transform.position) : null;
    }
}
