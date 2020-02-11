using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAudio : MonoBehaviour
{

    Vector3 startScale, startPos;
    public bool peerPosX, peerPosY, peerRotate;
    public float amount = 1;
    public int ban = 0;

    float xFloat, yFloat;
    void Start()
    {
        startScale = transform.localScale;
        startPos = transform.localPosition;
        xFloat = peerPosX ? 1 : 0;
        yFloat = peerPosY ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(startScale.x, startScale.y + (AudioPeer._audioBandBuffer[ban] * amount), startScale.z);

        if (peerPosX || peerPosY)
        {

            transform.localPosition = new Vector3(startPos.x + (xFloat * AudioPeer._audioBandBuffer[ban]), startPos.y + (AudioPeer._audioBandBuffer[ban] * yFloat), startPos.z);

        }

      
    }
}
