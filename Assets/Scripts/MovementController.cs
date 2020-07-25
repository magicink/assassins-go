using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class MovementController : MonoBehaviour
{
    private bool _isMoving;
    private Board _board;
    private bool _boardIsDefined = false;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public float speed = 1.5f;
    public float tweenDelay;

    public bool IsMoving => _isMoving;

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

    public void Move(Vector3 destination, float delay = 0.25f)
    {
        if (_boardIsDefined && _board.FindNodeAt(destination))
        {
            StartCoroutine(MoveRoutine(destination, delay));
        }
    }

    private IEnumerator MoveRoutine(Vector3 destination, float delay)
    {
        yield return new WaitForSeconds(delay);

        _isMoving = true;

        var lookRotation = 
            Quaternion.LookRotation((destination - transform.position).normalized);
        transform.rotation = lookRotation;

        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destination.x,
            "y", destination.y,
            "z", destination.z,
            "easetype", easeType,
            "delay", delay,
            "speed", speed
        ));

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            yield return null;
        }

        transform.position = destination;
        _isMoving = false;
    }
}