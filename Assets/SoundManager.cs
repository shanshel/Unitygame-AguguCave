using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class SoundManager : MonoBehaviour
{

    public AudioClip dashSfx, rotateSfx, scaleUpSfx, scaleDownSfx;
    public static SoundManager _inst;


    private void Awake()
    {
        _inst = this;
    }

    private void Start()
    {
       

    }
    AudioClip getSFX(SFXEnum sfx)
    {

          AudioClip audio = null;

        switch (sfx)
        {
            case SFXEnum.dash:
                audio = dashSfx;
                break;
            case SFXEnum.scaleUp:
                audio = scaleUpSfx;
                break;
            case SFXEnum.scaleDown:
                audio = scaleDownSfx;
                break;
            case SFXEnum.rotate:
                audio = rotateSfx;
                break;
        }

        return audio;
    }


    public AudioClip playSFX(SFXEnum sfx)
    {
        AudioClip sfxToPlay = getSFX(sfx);
        Doozy.Engine.Soundy.SoundyManager.Play(sfxToPlay);
        return sfxToPlay;
    }

    IEnumerator stopSFX(AudioSource sfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sfx.Stop();
        yield return null;
    }

 
}
