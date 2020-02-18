using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;
//using Doozy.Engine.Soundy;

public class SoundManager : MonoBehaviour
{

    public AudioClip dashSfx, rotateSfx, passSfx, 
        spawnObstSfx, monsterLaughSfx, loseSfx, gainSfx;

    public static int dashSfxPrepared, rotateSfxPrepared, passSfxPrepared, spawnObsSfxPrepared, monsterLaughSfxPrepared, loseSfxPrepared, gainSfxPrepared;
    public static SoundManager _inst;
    // public SoundyController _soundy;

    //public SoundyManager _test;

    private void Awake()
    {
        _inst = this;
    }


  
    private void Start()
    {
        dashSfxPrepared = EazySoundManager.PrepareSound(dashSfx);
        rotateSfxPrepared = EazySoundManager.PrepareSound(rotateSfx);
        passSfxPrepared = EazySoundManager.PrepareSound(passSfx);
        spawnObsSfxPrepared = EazySoundManager.PrepareSound(spawnObstSfx);
        monsterLaughSfxPrepared = EazySoundManager.PrepareSound(monsterLaughSfx);
        loseSfxPrepared = EazySoundManager.PrepareSound(loseSfx);
        gainSfxPrepared = EazySoundManager.PrepareSound(gainSfx);
    



        //Doozy.Engine.Soundy.SoundyManager.Play(passSfx);


    }

    AudioClip getSFX(SFXEnum sfx)
    {

        AudioClip audio = dashSfx;

        switch (sfx)
        {
            case SFXEnum.dash:
                audio = dashSfx;
                break;
            case SFXEnum.rotate:
                audio = rotateSfx;
                break;
            case SFXEnum.pass:
                audio = passSfx;
                break;
            case SFXEnum.spawnObst:
                audio = spawnObstSfx;
                break;
            case SFXEnum.monsterLaugh:
                audio = monsterLaughSfx;
                break;
            case SFXEnum.lose:
                audio = loseSfx;
                break;
            case SFXEnum.gain:
                audio = gainSfx;
                break;
        }

        return audio;
    }


    public AudioClip playSFX(SFXEnum sfx)
    {
        AudioClip sfxToPlay = getSFX(sfx);
        EazySoundManager.PlayUISound(sfxToPlay);

        if (sfx == SFXEnum.pass)
        {
            EazySoundManager.PlayUISound(gainSfx);
   
        }



        return sfxToPlay;
    }
    
    public void playSFXCorot(SFXEnum sfx, float seconds)
    {
        StartCoroutine(playSFXCorotPrivate(sfx, seconds));
    }

    IEnumerator playSFXCorotPrivate(SFXEnum sfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playSFX(sfx);
    }




    IEnumerator stopSFX(AudioSource sfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sfx.Stop();
        yield return null;
    }

 

 
}
