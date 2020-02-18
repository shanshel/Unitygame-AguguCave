using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof (Camera))]
public class CameraMain : MonoBehaviour
{
    public static CameraMain _inst;
    public Camera _mainCamera;
    [SerializeField]
    private CinemachineVirtualCamera vcam;

    float changeTimer, fieldOfViewTarget;

    private void Awake()
    {
        _inst = this;
    }



    public void whenPlayerPass()
    {
        //anim.SetTrigger("spawnObs");
        changeTimer = 1f;
        fieldOfViewTarget = 140f;
        Invoke("resetValues", 1f);
    }

    private void resetValues()
    {
        fieldOfViewTarget = 80f;
        changeTimer = 1f;

    }

    private void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer > 0f)
        {
           // vcam.m_Lens.FieldOfView = Mathf.MoveTowards(vcam.m_Lens.FieldOfView, fieldOfViewTarget, Time.deltaTime * 200f);
        }
    }

}
