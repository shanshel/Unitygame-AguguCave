using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAudio : MonoBehaviour
{

    Vector3 startScale;
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(startScale.x, startScale.y + AudioPeer._audioBandBuffer[0], startScale.z);
    }
}
