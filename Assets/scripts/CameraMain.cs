using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Camera))]
public class CameraMain : MonoBehaviour
{
    public static CameraMain _inst;
    private Animator anim;
    public Camera _mainCamera;
    private void Awake()
    {
        _inst = this;
        anim = GetComponent<Animator>();
        _mainCamera = GetComponent<Camera>();
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
