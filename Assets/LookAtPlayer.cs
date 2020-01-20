using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public GameObject objectToLookAt;
    Tweener thisTweener;
    Vector3 lastPos, targetLastPos;

    private void Start()
    {
        var obj = objectToLookAt.transform.position;
        thisTweener = transform.DOLookAt(obj, 2f).SetAutoKill(false);
        lastPos = transform.position;
        targetLastPos = objectToLookAt.transform.position;
    
    }
    // Update is called once per frame
    void Update()
    {
       
        lastPos = objectToLookAt.transform.position;
        if (targetLastPos == lastPos) return;
        thisTweener.ChangeEndValue(objectToLookAt.transform.position, true).Restart();
        targetLastPos = lastPos;
   

    }
}
