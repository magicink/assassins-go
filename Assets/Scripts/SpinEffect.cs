using UnityEngine;

public class SpinEffect : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float speed = 2.0f;

    private void Start()
    {
        Spin();
    }

    private void Spin()
    {
        iTween.RotateBy(gameObject, iTween.Hash(
            "y", 180.0f,
            "delay", delay,
            "speed", speed,
            "oncomplete", "Spin"
            ));
    }
}
