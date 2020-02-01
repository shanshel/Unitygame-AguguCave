using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnAudio : MonoBehaviour
{
    Light _light;
    public int _band = 0;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.color = new Color(AudioPeer._audioBandBuffer[_band], _light.color.g, _light.color.b);
    }
}
