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
        ScoreText.text = score.ToString();
    }

}
