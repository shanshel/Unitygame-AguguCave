using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingEnv : MonoBehaviour
{
    public float speed, endAt, startAt;
   

    private void FixedUpdate()
    {
        float step = (speed + GameManager._inst.globalScrollSpeed) * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, endAt), step);
        if (transform.position.z <= endAt)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startAt);
        }
    }
}
