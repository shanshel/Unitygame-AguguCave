using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{

    [SerializeField]
    GameObject envCirclePrefab;

    GameObject[] _circles = new GameObject[25];
    void Start()
    {
        
        for (var i = 0; i < 20; i++)
        {
            var pos = transform.position;
            pos.z += i * 5f;
            var gameObj = Instantiate(envCirclePrefab, pos, Quaternion.identity, transform);
            gameObj.transform.RotateAround(Vector3.zero, Vector3.forward, Random.Range(0f, 180f));
        }
    }

    private void Update()
    {
        
    }


}
