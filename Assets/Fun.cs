using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Fun : MonoBehaviour
{
    // Start is called before the first frame update
 

    private void Update()
    {
        float speed = PlayerController._inst._moveAudioSource.pitch;

        if (PlayerContainer.touched)
        {
            speed *= 360f;
        }
        else
        {
            speed *= 50f;
        }
        if (GameManager._inst.isPassing)
            speed = 1000f;

        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0f));
    }

}
