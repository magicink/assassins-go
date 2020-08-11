using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class Overlay : MonoBehaviour
{
    private MaskableGraphic _graphic;
    [SerializeField] private Color from = Color.white;
    [SerializeField] private Color to = new Color(1f,1f,1f,0f);
    [SerializeField] private float delay = 0;
    [SerializeField] private float time = 1f;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    private void Awake()
    {
        _graphic = GetComponent<MaskableGraphic>();
    }

    public void FadeOut()
    {
        var o = gameObject;
        iTween.ValueTo(o, iTween.Hash(
            "from", from, 
            "to", to, 
            "time", time,
            "delay", delay,
            "onupdatetarget", o,
            "onupdate", "OnUpdate",
            "oncomplete", "OnComplete",
            "onstart", "OnStart"
        ));
    }

    public void FadeIn()
    {
        var o = gameObject;
        iTween.ValueTo(o, iTween.Hash(
            "from", to, 
            "to", from, 
            "time", time,
            "delay", delay,
            "onupdatetarget", o,
            "onupdate", "OnUpdate",
            "oncomplete", "OnComplete",
            "onstart", "OnStart"
        ));
    }

    private void OnUpdate(Color nextColor)
    {
        if (_graphic)
        {
            _graphic.color = nextColor;
        }
    }

    private void OnComplete()
    {
        // gameObject.SetActive(false);
    }

    private void OnStart()
    {
        
    }
}
