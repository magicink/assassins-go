using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector2 _coordinates;
    private bool _initialized;
    [SerializeField] private bool autoRun;
    [SerializeField] private float delay = 0.25f;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    [SerializeField] private GameObject geometry;
    [SerializeField] private float scaleTime = 0.25f;

    public Vector2 Coordinates => VectorHelper.Round(_coordinates);

    public List<Node> Neighbors { get; set; } = new List<Node>();

    private void Awake()
    {
        var position = transform.position;
        _coordinates = new Vector2(position.x, position.z);
    }

    private void Start()
    {
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;
        }

        if (autoRun) Initialize();
    }

    private void ShowGeometry()
    {
        iTween.ScaleTo(geometry, iTween.Hash(
            "time", scaleTime,
            "scale", Vector3.one,
            "easetype", easeType,
            "delay", delay
        ));
    }

    public void Initialize()
    {
        if (_initialized) return;
        ShowGeometry();
        InitializeNeighbors();
        _initialized = true;
    }

    private void InitializeNeighbors()
    {
        StartCoroutine(InitializeNeighborsRoutine());
    }

    private IEnumerator InitializeNeighborsRoutine()
    {
        yield return new WaitForSeconds(delay);
        foreach (var neighbor in Neighbors)
        {
            neighbor.Initialize();
        }
    }

    public static List<Node> FindNeighbors(Node target, List<Node> nodes)
    {
        var neighbors = new List<Node>();
        foreach (var direction in Board.Directions)
        {
            var neighbor = nodes.Find(n => n.Coordinates == target.Coordinates + direction);
            if (neighbor != null && !neighbors.Contains(neighbor)) neighbors.Add(neighbor);
        }

        return neighbors;
    }
}