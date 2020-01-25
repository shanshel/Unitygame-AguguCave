using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    GameObject objectToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        if (objectToLookAt == null)
            objectToLookAt = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt( objectToLookAt.transform.position);
    }
}
