﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using EasyMobile;
public class GameManager : MonoBehaviour
{
   
    public ParticleSystem SpeedOn;
    public static GameManager _inst;
    public bool isGameOver, isGameStarted, isPassing;
    public float globalScrollSpeed = 1f, oldGlobalScrollSpeed = 1f;
    public float gamePlayTime;
    public GameObject water;
    
    public int score, earthScore;
    private float startGlobalScrollSpeed;

    private void Awake()
    {
        if (_inst != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _inst = this;
        Application.targetFrameRate = 300;

    }
    // Start is called before the first frame update
    void Start()
    {

        startGlobalScrollSpeed = globalScrollSpeed;
        
        water.transform.DOLocalMoveY(-2.5f, 2f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        gamePlayTime += Time.deltaTime;
    }

    public void gameOver()
    {
        TinySauce.OnGameFinished(score);

        if (score > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("score", score);
        }
        EasyMobileProManager.reportScore(score);


        isGameOver = true;
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.lose, .1f);
        SoundManager._inst.playSFXCorot(EnumsData.SFXEnum.monsterLaugh, .6f);
        Time.timeScale = 0.4f;
        PlayerController._inst.onPlayerDie();
        MasterUI._inst.onGameOver();
        //Invoke("reloadScene", 2.2f);
    }

    public void onStartGame()
    {
        Time.timeScale = 1f;
        TinySauce.OnGameStarted();
        isGameStarted = true;
        isGameOver = false;
        globalScrollSpeed = startGlobalScrollSpeed;
        oldGlobalScrollSpeed = startGlobalScrollSpeed;
        gamePlayTime = 0f;
        score = 0;
        earthScore = 0;
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(1);
    }



    public int getCubeIndexOnDirection(int cubeIndex, EnumsData.FourDirectionEnum cubeDirection, int cubePerRow)
    {

        if (cubeDirection == EnumsData.FourDirectionEnum.Bottom)
        {
            return cubeIndex + cubePerRow;
        }
        else if (cubeDirection == EnumsData.FourDirectionEnum.Top)
        {
            return cubeIndex - cubePerRow;
        }
        else if (cubeDirection == EnumsData.FourDirectionEnum.Right)
        {
            return cubeIndex + 1;
        }
        return cubeIndex - 1;
    }
    public EnumsData.FourDirectionEnum getDirection(int cubeIndex, int length, int cubePerRow)
    {
        var rand = Random.Range(0, 4);
        if (cubeIndex == 0)
        {

            if (rand > 2)
            {
                ///Debug.Log("Next To First On Right: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Right;
            }
            else
            {
                ///Debug.Log("Next To First On Bottom: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Bottom;
            }
        }

        else if (cubeIndex == length - 1)
        {
            //return EnumsData.CubeTypeEnum.BottomRightCorner;
            if (rand > 2)
            {
                ///Debug.Log("Next To BottomRightCorner On Top: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Top;
            }
            else
            {
                ///Debug.Log("Next To BottomRightCorner On Left: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Left;
            }
        }
        else if (cubeIndex == cubePerRow - 1)
        {

            //return EnumsData.CubeTypeEnum.TopRightCorner;

            if (rand > 2)
            {
                ///Debug.Log("Next To TopRightCorner On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
            else
            {
                ///Debug.Log("Next To TopRightCorner On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
        }
        else if (cubeIndex == length - cubePerRow)
        {

            //return EnumsData.CubeTypeEnum.BottomLeftCorner;
            if (rand > 2)
            {
                ///Debug.Log("Next To BottomLeftCorner On Top: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Top;
            }
            else
            {
                ///Debug.Log("Next To BottomLeftCorner On Right: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Right;
            }
        }
        else if (Mathf.Floor(cubeIndex / cubePerRow) == 0)
        {
            //return EnumsData.CubeTypeEnum.TopEdge;
            if (rand == 0)
            {
                ///Debug.Log("Next To TopEdge On Right: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Right;
            }
            else if (rand == 1)
            {
                ///Debug.Log("Next To TopEdge On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
            else
            {
                ///Debug.Log("Next To TopEdge On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
        }
        else if (Mathf.Floor(cubeIndex / cubePerRow) == Mathf.Floor((length - 1) / cubePerRow))
        {

            //return EnumsData.CubeTypeEnum.BottomEdge;
            if (rand == 0)
            {
                ///Debug.Log("Next To BottomEdge On Right: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Right;
            }
            else if (rand == 1)
            {
                ///Debug.Log("Next To BottomEdge On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
            else
            {
                ///Debug.Log("Next To BottomEdge On Top: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Top;
            }
        }
        else if (cubeIndex % cubePerRow == 0)
        {
            //return EnumsData.CubeTypeEnum.RightEdge;
            if (rand == 0)
            {
                ///Debug.Log("Next To LeftEdge On Top: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Top;
            }
            else if (rand == 1)
            {
                ///Debug.Log("Next To LeftEdge On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
            else
            {
                ///Debug.Log("Next To LeftEdge On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Right;
            }
        }
        else if (cubeIndex - (cubePerRow * Mathf.Floor(cubeIndex / cubePerRow)) == cubePerRow - 1)
        {

            //return EnumsData.CubeTypeEnum.LeftEdge;
            if (rand == 0)
            {
                ///Debug.Log("Next To RightEdge On Top: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Top;
            }
            else if (rand == 1)
            {
                ///Debug.Log("Next To RightEdge On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
            else
            {
                ///Debug.Log("Next To RightEdge On Right: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
        }


        if (rand == 0)
        {
           ///Debug.Log("Next To Center On Top: " + cubeIndex);

            return EnumsData.FourDirectionEnum.Top;
        }
        else if (rand == 1)
        {
            ///Debug.Log("Next To Center On Bottom: " + cubeIndex);

            return EnumsData.FourDirectionEnum.Bottom;
        }
        else if (rand == 2)
        {
            ///Debug.Log("Next To Center On Right: " + cubeIndex);

            return EnumsData.FourDirectionEnum.Right;
        }
        ///Debug.Log("Next To Center On Left: " + cubeIndex);

        return EnumsData.FourDirectionEnum.Left;

    }

}
