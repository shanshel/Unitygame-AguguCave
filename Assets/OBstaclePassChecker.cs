using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBstaclePassChecker : MonoBehaviour
{
    [SerializeField]
    obstacles _father;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _father.touchCount += 1;
    }
}
