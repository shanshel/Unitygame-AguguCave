using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using EasyMobile;
public class MasterUI : MonoBehaviour
{
    public static MasterUI _inst;
    public GameObject mainMenuPanel, gameOverPanel, inGamePanel;

    public TextMeshProUGUI scoreInMenu, scoreInGameOver, scoreInGame;
    // Start is called before the first frame update

    private void Awake()
    {
        if (_inst != null)
        {
            Destroy(gameObject);
            return;
        }
        _inst = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        resetScoreInfo();
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        inGamePanel.SetActive(false);


    }

    public void onStartClicked()
    {
        resetScoreInfo();
        mainMenuPanel.SetActive(false);
        inGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        GameManager._inst.onStartGame();
        SceneManager.LoadScene(1);
    }

    public void onLeaderBoardButtonClicked()
    {
        if (GameServices.IsInitialized())
        {
            GameServices.ShowLeaderboardUI();
        }
    }
    void resetScoreInfo()
    {
        scoreInMenu.text = PlayerPrefs.GetInt("score", 0).ToString();
        scoreInGameOver.text = "0";
        scoreInGame.text = "0";
    }

    public void onGameOver()
    {
        scoreInGameOver.text = scoreInGame.text;
        Invoke("gameOverDealyed", 1.5f);

    }
    void gameOverDealyed()
    {
        gameOverPanel.SetActive(true);
        inGamePanel.SetActive(false);
        mainMenuPanel.SetActive(false);
    }

    public void updateScore()
    {
        scoreInGame.text = GameManager._inst.score.ToString();
    }
   
}
