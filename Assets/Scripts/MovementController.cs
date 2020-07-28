using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Board _board;
    private bool _boardIsDefined;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float tweenDelay;

    public delegate void MoveEvent();

    public static event MoveEvent OnMoveEnd;

    public bool IsMoving { get; private set; }

    private void Awake()  
    {
        _board = FindObjectOfType<Board>();
        _boardIsDefined = _board != null;
    }

    public void MoveBack()
    {
        Move(transform.position + new Vector3(0, 0, -Board.Spacing), tweenDelay);
    }

    public void MoveForward()
    {
        Move(transform.position + new Vector3(0, 0, Board.Spacing), tweenDelay);
    }

    public void MoveLeft()
    {
        Move(transform.position + new Vector3(-Board.Spacing, 0, 0), tweenDelay);
    }

    public void MoveRight()
    {
        Move(transform.position + new Vector3(Board.Spacing, 0, 0), tweenDelay);
    }

    private void Move(Vector3 destination, float delay = 0.25f)
    {
        var target = _board.FindNodeAt(destination);
        if (!IsMoving && _boardIsDefined && target && _board.PlayerNode.LinkedNodes.Contains(target))
        {
            StartCoroutine(MoveRoutine(destination, delay));
        }
    }

    private IEnumerator MoveRoutine(Vector3 destination, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        var lookRotation = 
            Quaternion.LookRotation((destination - transform.position).normalized);
        transform.rotation = lookRotation;

        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destination.x,
            "y", destination.y,
            "z", destination.z,
            "easetype", easeType,
            "delay", delay,
            "speed", speed,
            "onstart", "OnMoveStart",
            "oncomplete", "OnMoveComplete",
            "oncompleteparams", destination
        ));
    }

    // ReSharper disable once UnusedMember.Local
    private void OnMoveStart()
    {
        IsMoving = true;
    }

    // ReSharper disable once UnusedMember.Local
    private void OnMoveComplete(Vector3 destination)
    {
        IsMoving = false;
        transform.position = VectorHelper.Floor(destination);
        OnMoveEnd?.Invoke();
    }
}