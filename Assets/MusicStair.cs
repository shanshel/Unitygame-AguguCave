using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MusicStair : MonoBehaviour
{
    int _band;
    // Start is called before the first frame update
    void Start()
    {
        _band = Random.Range(0, 7);


    }

    private void Update()
    {
        transform.localScale = new Vector3(1, AudioPeer._audioBandBuffer[_band] * 10f, 1f);
    }


}
