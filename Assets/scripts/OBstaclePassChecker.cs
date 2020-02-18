using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBstaclePassChecker : MonoBehaviour
{
    [SerializeField]
    obstacles _father;
    [SerializeField]
    BoxCollider _collider;


    private void Start()
    {
        _father = spawner._inst.activeObstacles[spawner._inst.activeObstacles.Count - 1];
    }
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.tag == "Player")
        {

            _collider.enabled = false;
            _father.touchCount += 1;
        }
        
    }
}
