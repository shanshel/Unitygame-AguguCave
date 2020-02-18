using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public static spawner _inst;
    public obstacles[] obstclesPattern;
    public GameObject[] playerShapes;

    public float increaseSpeed;
    public float maxspeed = 20f;

    public List<obstacles> activeObstacles = new List<obstacles>();

    private void Awake()
    {
        if (_inst == null)
            _inst = this;
    }


    public Vector3 getCurrentObstaclePosition ()
    {
        if (activeObstacles.Count > 0)
            return activeObstacles[0].transform.position;
        else
            return Vector3.zero;

    }
    float starterPlusSpeed = 0f;
    public void SpawnObs()
    {
        if (!GameManager._inst.isGameStarted || GameManager._inst.gamePlayTime < 2f)
            return;
        int rand = Random.Range(0, obstclesPattern.Length);
        var obs = Instantiate(obstclesPattern[rand], transform.position, Quaternion.identity);
 
        PlayerHeadAnim._inst.playerAnimWhenSpawnObstcale();
        

        SoundManager._inst.playSFX(EnumsData.SFXEnum.spawnObst);
   
      
        if (GameManager._inst.earthScore < 3f)
        {
            starterPlusSpeed = 2f;
        }
        else
        {
            starterPlusSpeed = 0f;
        }
        
        if (GameManager._inst.globalScrollSpeed < maxspeed)
        {
            GameManager._inst.globalScrollSpeed += increaseSpeed + starterPlusSpeed;
        }
        if (GameManager._inst.globalScrollSpeed == 100f)
        {
            GameManager._inst.oldGlobalScrollSpeed += increaseSpeed + starterPlusSpeed;

        }


    }

    public void playerFail()
    {
        if (GameManager._inst.isGameOver) return;
        GameManager._inst.gameOver();
        activeObstacles.RemoveRange(0, activeObstacles.Count);
    }

    public void playerPass(int scorePoint)
    {
        if (GameManager._inst.isGameOver) return;
        Handheld.Vibrate();
        PlayerController._inst.onPlayerPass(scorePoint);
        GameManager._inst.SpeedOn.Play();
        PostProcessEffect._inst.changeProfile();
        ScoreManager._inst.IncreaseScore();
        Monster._inst.attackAfter(.5f);
        CameraMain._inst.whenPlayerPass();

    }

    

}
