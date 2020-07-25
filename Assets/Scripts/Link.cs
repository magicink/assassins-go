using System;
using UnityEngine;

public class Link : MonoBehaviour
{
    [SerializeField] private GameObject geometry;
    [SerializeField] private float offset = 0.025f;
    [SerializeField] private float lineWeight = 0.5f;
    [SerializeField] private float scaleTime = 1.0f;
    [SerializeField] private float delay;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

    public void Draw(Vector3 start, Vector3 end)
    {
        if (geometry == null) return;
        
        // Geometry changes
        geometry.transform.localScale = new Vector3(lineWeight, 1f, 0f);
        var direction = start - end;
        var distance = direction.magnitude - offset * 2f;
        var destination = new Vector3(lineWeight, 1f, distance);

        // Parent changes
        Transform parentTransform;
        (parentTransform = transform).rotation = Quaternion.LookRotation(direction);
        parentTransform.position = start + parentTransform.forward * offset;
        
        iTween.ScaleTo(geometry, iTween.Hash("time", scaleTime, "scale", destination, "easetype", easeType, "delay", delay));
    }
}
