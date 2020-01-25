using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text ScoreText;
    public Text finalScore;
    public static ScoreManager _inst;
    private void Awake()
    {
        if (_inst == null)
        {
            _inst = this;
        }
    }
    private void Update()
    {

    }
    public void IncreaseScore()
    {
        score++;
        SoundManager._inst.playSFX(EnumsData.SFXEnum.pass);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.pass, .15f);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.pass, .3f);
        GameManager._inst.globalScrollSpeed = 5f;

        Invoke("resetScrollSpeed", 1f);
        ScoreText.text = score.ToString();
    }

    void resetScrollSpeed()
    {
        GameManager._inst.globalScrollSpeed = 1f;

    }

}
