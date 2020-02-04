using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
   
    public ParticleSystem SpeedOn;
    public static GameManager _inst;
    public TextMeshProUGUI scoreText;
    public bool isGameOver;
    public float globalScrollSpeed = 1;
    public float gamePlayTime;
    public float songPlayTime;
    public int songCircale;

    public int cubePerRow = 8;
    float repeatSongEvery = 189.832f;
    public int score;

    private void Awake()
    {
        _inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        gamePlayTime += Time.deltaTime;

        if (Mathf.Floor(gamePlayTime / repeatSongEvery) > songCircale)
            songCircale++;

        songPlayTime = (GameManager._inst.gamePlayTime - (songCircale * repeatSongEvery));

    }

    public void gameOver()
    {
        isGameOver = true;
        SoundManager._inst.playSFX(EnumsData.SFXEnum.destroy);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.lose, .2f);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.monsterLaugh, .6f);
        Time.timeScale = 0.4f;
        PlayerController._inst.onPlayerDie();
        Invoke("reloadScene", 2.2f);


    }

    public void reloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
