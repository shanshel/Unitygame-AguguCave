using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallMonster : MonoBehaviour
{
    Vector3 startPos;
    public int direction = 1;

    private void Start()
    {
        startPos = transform.localPosition;
    }
    private void Update()
    {
        if (!GameManager._inst.isGameStarted || GameManager._inst.gamePlayTime < 3f)
            return;

    
        transform.localPosition = new Vector3(startPos.x + (direction * AudioPeer._audioBandBuffer[0] * 5f), startPos.y + (AudioPeer._audioBandBuffer[0] * 2f), startPos.z);

    }
}
