using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public float[] _samples = new float[512];

    AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        GetSpectrumAudioSource();
    }
    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
