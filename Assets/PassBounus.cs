using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassBounus : MonoBehaviour
{
    [SerializeField]
    int bounus = 0;
    [SerializeField]
    BoxCollider _collider;

    obstacles _father;

    // Start is called before the first frame update

    private void Start()
    {
        _father = spawner._inst.activeObstacles[spawner._inst.activeObstacles.Count - 1];
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            _collider.enabled = false;
            _father.earnPoint += bounus;
        }

    }
}
