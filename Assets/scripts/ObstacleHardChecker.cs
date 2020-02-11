using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHardChecker : MonoBehaviour
{
    [SerializeField]
    obstacles _father;
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.tag == "Player")
        {
            _father.obstFail();
        }
        else if (other.CompareTag("Passenger"))
        {
            
            if (_father.touchCount == _father.touchCountRequired)
            {
                _father.obstPass();
            }
            else
            {
                _father.obstFail();
            }
        }


    }
}
