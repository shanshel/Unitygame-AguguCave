using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingEnv : MonoBehaviour
{
    public float speed, endAt, startAt;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = GameManager._inst.globalScrollSpeed * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, endAt), step);
        if (transform.position.z <= endAt)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startAt);
        }
    }
}
