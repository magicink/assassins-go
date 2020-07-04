using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private bool _moving;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public float speed = 1.5f;

    void Start()
    {
        Move(new Vector3(3f, 0f, 0f));
    }

    public void Move(Vector3 destination, float delay = 0.25f)
    {
        StartCoroutine(MoveRoutine(destination, delay));
    }

    private IEnumerator MoveRoutine(Vector3 destination, float delay)
    {
        _moving = true;

        yield return new WaitForSeconds(delay);

        iTween.MoveTo(gameObject, iTween.Hash(
            "x", destination.x,
            "y", destination.y,
            "z", destination.z,
            "easetype", easeType,
            "speed", speed
        ));

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            yield return null;
        }

        transform.position = destination;
        _moving = false;
    }
}