using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
   
    [SerializeField]
    float cubeSize = .5f;
    [SerializeField]
    GameObject playerShadow;
    Vector3 shadowTraget, playerTarget; // Target to follow
    Vector3 ShadowtargetLastPos, pTargetLastPos;
    Tweener Shadowtween, playerTweeen;

    float lastAngle = 90f;
    float lastScale = 1;
    public float scaleStep = .2f;
    private float maxScale;
    private void Start()
    {
        maxScale = 1 + (scaleStep * 3);
        playerTarget = transform.position;

        //Ease.OutElastic
        playerTweeen = transform.DOMove(playerTarget, 0.3f).SetAutoKill(false).SetEase(Ease.OutElastic, .5f);

        pTargetLastPos = playerTarget;
        shadowTraget = new Vector3(transform.position.x, transform.position.y, playerShadow.transform.position.z);
        Shadowtween = playerShadow.transform.DOMove(shadowTraget, 1).SetAutoKill(false);
        ShadowtargetLastPos = shadowTraget;
    }
    private void Update()
    {

        ShadowMovemeent();
        movement();
        rotation();
        scaleListen();
    }

    void ShadowMovemeent()
    {
        shadowTraget = new Vector3(transform.position.x, transform.position.y, spawner._inst.getCurrentObstaclePosition().z - 0.7f);
        if (ShadowtargetLastPos == shadowTraget) return;
        // Add a Restart in the end, so that if the tween was completed it will play again
        Shadowtween.ChangeEndValue(shadowTraget, true).Restart();
        ShadowtargetLastPos = shadowTraget;
    }

    void rotation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            transform.DORotateQuaternion(Quaternion.Euler(0, 0, lastAngle), .3f).SetEase(Ease.OutElastic);
            if (lastAngle + 90f >= 360)
                lastAngle = 0f;
            else
                lastAngle = lastAngle + 90f;
        }
    }

    void scaleListen()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            lastScale += scaleStep;
     
            if (lastScale >= maxScale)
            {
                lastScale = 1;
            }
    
            transform.DOScale(lastScale, .3f).SetEase(Ease.OutElastic);
       
        }
    }
    void movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerTarget.y += cubeSize;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerTarget.y -= cubeSize;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            playerTarget.x += cubeSize;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerTarget.x -= cubeSize;
        }
    

        if (pTargetLastPos == playerTarget) return;
        playerTweeen.ChangeEndValue(playerTarget, true).Restart();
        pTargetLastPos = playerTarget;
    }
    private void FixedUpdate()
    {
        

        //transform.position = pos;


        //update shadow pos

    }




}
