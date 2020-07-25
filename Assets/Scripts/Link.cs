using UnityEngine;

public class Link : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    [SerializeField] private float lineWeight = 0.5f;
    [SerializeField] private float offset = 0.025f;
    [SerializeField] private float scaleTime = 1.0f;
    public Node Target { get; set; }

    public void Draw(Vector3 start, Vector3 end)
    {
        // Geometry changes
        var parentTransform = transform;
        parentTransform.localScale = new Vector3(lineWeight, 1f, 0f);
        var direction = end - start;
        var distance = direction.magnitude - offset * 2f;
        var destination = new Vector3(lineWeight, 1f, distance);

        parentTransform.rotation = Quaternion.LookRotation(direction);
        parentTransform.position = start + parentTransform.forward * offset;

        iTween.ScaleTo(gameObject, iTween.Hash(
            "time", scaleTime, 
            "scale", destination, 
            "easetype", easeType, 
            "delay", delay,
            "oncomplete", "HandleComplete"
        ));
    }

    // ReSharper disable once UnusedMember.Local
    private void HandleComplete()
    {
        if (Target != null)
        {
            Target.Initialize();
        }
    }
}