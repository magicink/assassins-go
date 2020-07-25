using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Vector2 _coordinates;
    private bool _initialized;
    private bool _isLinkPrefabNull;
    private List<Node> _linkedNodes = new List<Node>();
    [SerializeField] private bool autoRun;
    [SerializeField] private float delay = 0.25f;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    [SerializeField] private GameObject geometry;
    [SerializeField] private GameObject linkPrefab;
    [SerializeField] private float scaleTime = 0.25f;

    public Vector2 Coordinates => VectorHelper.Floor(_coordinates);

    public List<Node> Neighbors { get; set; } = new List<Node>();

    public List<Node> LinkedNodes => _linkedNodes;

    private void Awake()
    {
        _isLinkPrefabNull = linkPrefab == null;
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
            if (!_linkedNodes.Contains(neighbor)) DrawNeighborLink(neighbor);
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

    private void DrawNeighborLink(Node target)
    {
        if (_isLinkPrefabNull) return;
        var nodeTransform = transform;
        var position = nodeTransform.position;
        var linkInstance = Instantiate(linkPrefab, position, Quaternion.identity);
        linkInstance.transform.parent = transform;
        var link = linkInstance.GetComponent<Link>();
        if (link == null) return;
        if (!target.LinkedNodes.Contains(this)) target.LinkedNodes.Add(this);
        if (!_linkedNodes.Contains(target)) _linkedNodes.Add(target);
        link.Target = target;
        link.Draw(position, target.transform.position);
    }
}