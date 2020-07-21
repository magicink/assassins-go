using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private bool autoRun;
    [SerializeField] private float delay = 0.25f;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    [SerializeField] private GameObject geometry;
    [SerializeField] private float scaleTime = 0.25f;

    private void Start()
    {
        if (geometry != null)
        {
            geometry.transform.localScale = Vector3.zero;
        }

        if (autoRun) ShowGeometry();
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
}