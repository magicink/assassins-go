using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private bool _isMoving;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public float speed = 1.5f;
    public float tweenDelay;

    public bool IsMoving => _isMoving;

    public void MoveBack()
    {
        Move(transform.position + new Vector3(0, 0, -2), tweenDelay);
    }

    public void MoveForward()
    {
        Move(transform.position + new Vector3(0, 0, 2), tweenDelay);
    }

    public void MoveLeft()
    {
        Move(transform.position + new Vector3(-2, 0, 0), tweenDelay);
    }

    public void MoveRight()
    {
        Move(transform.position + new Vector3(2, 0, 0), tweenDelay);
    }

    public void Move(Vector3 destination, float delay = 0.25f)
    {
        StartCoroutine(MoveRoutine(destination, delay));
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