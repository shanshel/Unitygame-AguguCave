using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacles : MonoBehaviour
{
    public float speed = 10;
    public ParticleSystem speedon;
    public bool isPassed = false;

    private void Start()
    {
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
            speed = 25;
            spawner._inst.activeObstacles.Remove(this);
            GameManager._inst.SpeedOn.Play();
            ScoreManager._inst.IncreaseScore();
            CameraMain._inst.camShake();
            Monster._inst.Attack();
           

        }
    }


 
}
