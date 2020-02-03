using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{

    [SerializeField]
    GameObject envCirclePrefab;
    [SerializeField]
    GameObject[] spherePrefab;

   
    GameObject[] _circles = new GameObject[25];
    void Start()
    {
        
        for (var i = 0; i < 20; i++)
        {
            var pos = transform.position;
            pos.z += i * 5f;
            var gameObj = Instantiate(envCirclePrefab, pos, Quaternion.identity, transform);
            var oldScale = gameObj.transform.localScale;
            gameObj.transform.localScale = new Vector3(oldScale.x + 1.5f , oldScale.y + 1.5f, oldScale.z);

            gameObj.transform.RotateAround(Vector3.zero, Vector3.forward, Random.Range(0f, 180f));
        }
        
        for (var i = 0; i < 1000; i++)
        {
         
            var pos = transform.position;
            pos.x = -15f;
            if (i % 2 == 0)
            {
                pos.x = 15f;
            }
            pos.z += i * .1f;
            pos.y = Random.Range(-2f, 2f) ;


            var gameObj = Instantiate(spherePrefab[Random.Range(0, spherePrefab.Length)], pos, Quaternion.identity, transform);
            var oldScale = gameObj.transform.localScale;
            gameObj.transform.localScale = new Vector3(oldScale.x + Random.Range(3f, 6f), oldScale.y +  Random.Range(3f, 6f), oldScale.z);
            //gameObj.transform.localScale = new Vector3(Random.Range(5f, 7f), Random.Range(2f, 5f), Random.Range(2f, 5f));
            gameObj.transform.RotateAround(Vector3.zero, Vector3.forward, Random.Range(0f, 180f));
           
        }
       

        transform.position = new Vector3(0f, 13.5f, transform.position.z);
        transform.localScale = new Vector3(1.1f, 1.5f, 1f);
    }

    private void Update()
    {
        
    }


}
