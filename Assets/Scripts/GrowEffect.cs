using UnityEngine;

public class GrowEffect : MonoBehaviour
{
    [SerializeField] private float time = 1.0f;
    private void Start()
    {
        Grow();
    }

    private void Grow()
    {
        iTween.ScaleFrom(gameObject, iTween.Hash(
            "scale", Vector3.zero,
            "time", time));
    }
}
