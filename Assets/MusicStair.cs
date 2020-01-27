using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MusicStair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var to = Random.Range(-9f, -9.5f);
        var delay = Random.Range(.1f, 1f);
        transform.DOMoveY(to, .7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBounce).SetDelay(delay);
     
    }


}
