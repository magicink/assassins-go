using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float time = 1.5f;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInSine;
    private void Start()
    {
        Ascend();
    }

    private void Ascend()
    {
        iTween.MoveBy(gameObject, iTween.Hash(
            "y", offset,
            "time", time,
            "easeType", easeType,
            "loopType", iTween.LoopType.pingPong));
    }
}
