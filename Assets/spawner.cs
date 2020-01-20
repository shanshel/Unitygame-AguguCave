using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public static spawner _inst;
    public GameObject[] obstclesPattern;
    public GameObject[] playerShapes;

    private float timeBtwSpn;
    public float StartTimeBtwSpn;
    public float decreaseTime;
    public float minTime = 0.65f;
    public List<obstacles> activeObstacles = new List<obstacles>();

 
    private void Awake()
    {
        if (_inst == null)
            _inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwSpn <= 0)
        {
            int rand = Random.Range(0, obstclesPattern.Length);
            Instantiate(obstclesPattern[rand], transform.position, Quaternion.identity);
            timeBtwSpn = StartTimeBtwSpn;
            if (StartTimeBtwSpn > minTime)
            {
                StartTimeBtwSpn -= decreaseTime;
            }
        }else
        {
            timeBtwSpn -= Time.deltaTime;
        }
    }

    public Vector3 getCurrentObstaclePosition ()
    {
        if (activeObstacles.Count > 0)
            return activeObstacles[0].transform.position;
        else
            return Vector3.zero;

    }
  
}
