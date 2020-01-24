using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacles : MonoBehaviour
{
    private Shake shake;
    public float speed = 10;
    public ParticleSystem speedon;
    public bool isPassed = false;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        spawner._inst.activeObstacles.Add(this);
    }
    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (isPassed) return;
        
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (other.CompareTag("Passenger"))
        {
            
            isPassed = true;
            GameManager._inst.SpeedOn.Play();
            Invoke("StopSpeedOnPar", 0.1f);
            shake.camShake();
            ScoreManager._inst.IncreaseScore();
            spawner._inst.activeObstacles.Remove(this);
            StartCoroutine(spawnObstacle());
            speed = 25;
            
        }
    }
    void StopSpeedOnPar()
    {
        GameManager._inst.SpeedOn.Stop();
    }

    IEnumerator spawnObstacle()
    {
        yield return new WaitForSeconds(3f);

        spawner._inst.SpawnObs();

        yield return null;
    }
}
