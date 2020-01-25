using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;

public class SoundManager : MonoBehaviour
{

    public AudioClip dashSfx, rotateSfx, scaleUpSfx, scaleDownSfx, passSfx;
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
            case SFXEnum.pass:
                audio = passSfx;
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
    
    public void playSFXCorot(SFXEnum sfx, float seconds)
    {
        StartCoroutine(playSFXCorotPrivate(sfx, seconds));
    }

    IEnumerator playSFXCorotPrivate(SFXEnum sfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AudioClip sfxToPlay = getSFX(sfx);
        Doozy.Engine.Soundy.SoundyManager.Play(sfxToPlay);
        yield return null;
    }




    IEnumerator stopSFX(AudioSource sfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sfx.Stop();
        yield return null;
    }

 
}
