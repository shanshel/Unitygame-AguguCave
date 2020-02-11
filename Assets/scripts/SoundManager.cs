using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumsData;
using Doozy.Engine.Soundy;

public class SoundManager : MonoBehaviour
{

    public AudioClip dashSfx, rotateSfx, passSfx, 
        spawnObstSfx, monsterLaughSfx, loseSfx;

    public AudioSource _music;
    float targetPitch = .4f;
    public static SoundManager _inst;
    bool stopCheckGameOver;
    public SoundyController _soundy;

    public SoundyManager _test;

    private void Awake()
    {
        _inst = this;
    }

    private void Update()
    {
        if (!stopCheckGameOver && GameManager._inst.isGameOver)
        {
            _music.pitch = Mathf.MoveTowards(_music.pitch, targetPitch, Time.deltaTime * .5f);
            if (_music.pitch == targetPitch)
            {
                _music.Stop();
                stopCheckGameOver = true;
            }
        }
    }
    private void Start()
    {
        Doozy.Engine.Soundy.SoundyManager.Play(passSfx);


    }
    AudioClip getSFX(SFXEnum sfx)
    {

          AudioClip audio = null;

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
        playSFX(sfx);
    }




    IEnumerator stopSFX(AudioSource sfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sfx.Stop();
        yield return null;
    }

 

 
}
