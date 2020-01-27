using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public static PlayerController _inst;
    [SerializeField]
    float cubeSize = .5f;


    [SerializeField]
    GameObject playerActionEffectPrefab;

    Vector3 shadowTraget, playerTarget; // Target to follow
    Vector3 ShadowtargetLastPos, pTargetLastPos;
    Tweener Shadowtween, playerTweeen;

    float lastAngle = 90f;
    float lastScale = 1;
    public float scaleStep = .2f;
    private float maxScale;
    private float playerTimeout = 0f;

    public GameObject playerPiecePrefab, shadowPiecePrefab, shadowContainer;
    private List<GameObject> playerShapes = new List<GameObject>();
    private List<GameObject> shadowShapes = new List<GameObject>();
    private Vector3 strartPos;
    private void Awake()
    {
        _inst = this;
    }
    private void Start()
    {
        for (var x = 0; x < 17; x++)
        {
            GameObject cube = Instantiate(playerPiecePrefab, transform);
            playerShapes.Add(cube);
        }

        for (var x = 0; x < 17; x++)
        {
            GameObject cube = Instantiate(shadowPiecePrefab, shadowContainer.transform);
            shadowShapes.Add(cube);
        }


        strartPos = transform.position;
        maxScale = 1 + (scaleStep * 3);
        playerTarget = transform.position;
        playerTweeen = transform.DOMove(playerTarget, 0.3f).SetAutoKill(false).SetEase(Ease.OutElastic, .5f);

        pTargetLastPos = playerTarget;
        shadowTraget = new Vector3(transform.position.x, transform.position.y, shadowContainer.transform.position.z);
        Shadowtween = shadowContainer.transform.DOMove(shadowTraget, .1f).SetAutoKill(false);
        ShadowtargetLastPos = shadowTraget;

     
    }
    private void Update()
    {

        ShadowMovemeent();

        if (playerTimeout > 0f)
        {
            playerTimeout -= Time.deltaTime;
            return;
        }
            
        movement();
        rotation();
        scaleListen();
    }

    void ShadowMovemeent()
    {
        shadowTraget = new Vector3(transform.position.x, transform.position.y, spawner._inst.getCurrentObstaclePosition().z - 0.7f);
        if (ShadowtargetLastPos == shadowTraget) return;
        // Add a Restart in the end, so that if the tween was completed it will play again
        var pos = shadowTraget;

        Shadowtween.ChangeEndValue(pos, true).Restart();
        ShadowtargetLastPos = shadowTraget;
    }

    void rotation()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {

            SoundManager._inst.playSFX(EnumsData.SFXEnum.rotate);
            Instantiate(playerActionEffectPrefab, transform);

            transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, lastAngle), .3f).SetEase(Ease.OutElastic);
            if (lastAngle + 90f >= 360)
                lastAngle = 0f;
            else
                lastAngle = lastAngle + 90f;

            playerTimeout = .1f;
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
                SoundManager._inst.playSFX(EnumsData.SFXEnum.scaleDown);

            }
            else
            {
                SoundManager._inst.playSFX(EnumsData.SFXEnum.scaleUp);

            }

            Instantiate(playerActionEffectPrefab, transform);
            transform.DOScale(lastScale, .3f).SetEase(Ease.OutElastic);
            playerTimeout = .1f;

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
        SoundManager._inst.playSFX(EnumsData.SFXEnum.dash);
        Instantiate(playerActionEffectPrefab, transform);

        playerTimeout = .1f;
        playerTweeen.ChangeEndValue(playerTarget, true).Restart();
        pTargetLastPos = playerTarget;
    }



    IEnumerator buildShape(List<GameObject> cubes)
    {
      
        yield return new WaitForSeconds(.3f);
        var playerCubeIndex = 0;
        for (var y = 0; y < cubes.Count; y++)
        {
            if (!cubes[y].activeInHierarchy)
            {
                Vector3 pos = cubes[y].transform.position;
                Vector3 shadowPos = cubes[y].transform.position;
                pos.z = strartPos.z;
                shadowPos.z -= 1f;

                playerShapes[playerCubeIndex].SetActive(true);
                playerShapes[playerCubeIndex].transform.DOMove(pos, .4f);


                shadowShapes[playerCubeIndex].SetActive(true);
                shadowShapes[playerCubeIndex].transform.DOMove(shadowPos, .2f);

                playerCubeIndex++;
            }
        }




    }
    IEnumerator decayShape(List<GameObject> cubes)
    {
        for (var x = 0; x < playerShapes.Count; x++)
        {
            var pos = cubes[0].transform.position;
            var shadowPos = cubes[0].transform.position;
            pos.z = strartPos.z;
            shadowPos.z -= 1f;
            playerShapes[x].transform.DOMove(pos, .2f);
            shadowShapes[x].transform.DOMove(shadowPos, .2f);
        }
        yield return new WaitForSeconds(.3f);
        for (var x = 0; x < playerShapes.Count; x++)
        {
            playerShapes[x].SetActive(false);
            shadowShapes[x].SetActive(false);
        }

    }


    public void setPlayerShape(List<GameObject> cubes)
    {
        transform.position = strartPos;
        StartCoroutine(decayShape(cubes));
        StartCoroutine(buildShape(cubes));
    }


}
