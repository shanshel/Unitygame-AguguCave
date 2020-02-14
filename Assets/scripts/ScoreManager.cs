using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager _inst;
    private void Awake()
    {
        if (_inst == null)
        {
            _inst = this;
        }
    }
  
    public void IncreaseScore()
    {

        SoundManager._inst.playSFX(EnumsData.SFXEnum.pass);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.pass, .15f);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.pass, .3f);
        GameManager._inst.oldGlobalScrollSpeed = GameManager._inst.globalScrollSpeed;
        GameManager._inst.globalScrollSpeed *= 4f;

        Invoke("resetScrollSpeed", 1f);
    }

    void resetScrollSpeed()
    {
        GameManager._inst.globalScrollSpeed = GameManager._inst.oldGlobalScrollSpeed;

    }

}
