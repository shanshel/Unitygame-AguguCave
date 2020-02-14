using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public static float[] _samples;
    public static float[] _freqBand;
    public static float[] _bandBuffer;

    float[] _freqBandHieghest;
    public static float[] _audioBand;
    public static float[] _audioBandBuffer;

    float[] _bufferDecrease;

    AudioSource _audioSource;

    private void Awake()
    {
        _samples = new float[512];
        _freqBand = new float[8];
        _bandBuffer = new float[8];
        _freqBandHieghest = new float[8];
        _audioBand = new float[8];
        _audioBandBuffer = new float[8];
        _bufferDecrease = new float[8];
    }
    void Start()
    {
   
        _audioSource = GetComponent<AudioSource>();


    }

    void createAudioBand()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHieghest[i])
            {
                _freqBandHieghest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHieghest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHieghest[i]);

        }
    }

    private void FixedUpdate()
    {
        GetSpectrumAudioSource();
        MakeFreqBandCalc();
        bandBuffer();
        createAudioBand();
    }
    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void bandBuffer()
    {
        for (int g =0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.0005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }
    void MakeFreqBandCalc()
    {
        int count = 0;

        for (var x = 0; x < 8; x++)
        {
            float avrage = 0;
            int sampleCount = (int)Mathf.Pow(2, x) * 2;
            if (x == 7)
                sampleCount += 2;

            for (var j = 0; j < sampleCount; j++)
            {
                avrage += _samples[count] * (count + 1 );
                count++;
            }
            avrage /= count;

            _freqBand[x] = avrage * 10;
        }
    }
}
