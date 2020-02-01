using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MarioMonster : MonoBehaviour
{
    public GameObject tn;
    public int tnBand, shakeBand;
    public int power;
    Animator _anim;
    Sequence _seq;
    // Start is called before the first frame update

    private void Awake()
    {
        _anim = GetComponent<Animator>();
       
    }

    private void Start()
    {
        _seq = DOTween.Sequence();
    }

    void danceStatusUpdate()
    {
        //_seq.Insert(1, transform.DOShakeRotation(.3f, AudioPeer._audioBandBuffer[shakeBand] * 50f, 2, 45f, true));
        //_seq.Insert(2, transform.DOShakePosition(.3f, AudioPeer._audioBandBuffer[shakeBand] * 5f, 3, 45f, true));

    }
    // Update is called once per frame
    void Update()
    {
        tn.transform.localScale = new Vector3(tn.transform.localScale.x, tn.transform.localScale.y, AudioPeer._audioBandBuffer[tnBand] * power);
        float scaleY = AudioPeer._audioBandBuffer[shakeBand];
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);

    }


}
