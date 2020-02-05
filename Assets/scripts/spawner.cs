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
    public float startspeed = 5f;
    private float currentSpeed;
    public List<obstacles> activeObstacles = new List<obstacles>();

 
    private void Awake()
    {
        if (_inst == null)
            _inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = startspeed;
    }


    public Vector3 getCurrentObstaclePosition ()
    {
        if (activeObstacles.Count > 0)
            return activeObstacles[0].transform.position;
        else
            return Vector3.zero;

    }
    public void SpawnObs()
    {
       
        int rand = Random.Range(0, obstclesPattern.Length);
        var obs = Instantiate(obstclesPattern[rand], transform.position, Quaternion.identity);
        Debug.Log("ERROR");
        PlayerHeadAnim._inst.playerAnimWhenSpawnObstcale();
        CameraMain._inst.spawnObs();

        SoundManager._inst.playSFX(EnumsData.SFXEnum.spawnObst);
        obs.speed = currentSpeed;
        if (currentSpeed < maxspeed)
        {
            currentSpeed += increaseSpeed;
        }
        
    }

    public void playerFail()
    {
        if (GameManager._inst.isGameOver) return;
        GameManager._inst.gameOver();
     
    }

    public void playerPass()
    {
        if (GameManager._inst.isGameOver) return;

        PlayerController._inst.onPlayerPass();
        GameManager._inst.SpeedOn.Play();
        PostProcessEffect._inst.changeProfile();
        ScoreManager._inst.IncreaseScore();
        CameraMain._inst.camShake();
        Monster._inst.AttackAfter(2f);

    }

    

}
