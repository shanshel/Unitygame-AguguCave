using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public static CameraMain _inst;
    private Animator anim;
    private void Awake()
    {
        _inst = this;
        anim = GetComponent<Animator>();
    }


    public void camShake()
    {
        anim.SetTrigger("shake");
    }
    public void spawnObs()
    {
        anim.SetTrigger("spawnObs");
    }

}
