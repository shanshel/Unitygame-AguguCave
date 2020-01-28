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
 
    private float playerTimeout = 0f;

    public GameObject playerPiecePrefab, shadowPiecePrefab, shadowContainer;
    private List<GameObject> playerShapes = new List<GameObject>();
    private List<GameObject> shadowShapes = new List<GameObject>();
 
    public GameObject pivotObject, containerObject;
    private Vector3 pivoitPoint;
    bool freezPlayerInput = false;
    Rigidbody _rigidBody;
    private void Awake()
    {
        _inst = this;
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        pivoitPoint = pivotObject.transform.position;
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


   
        playerTarget = transform.position;
        playerTweeen = transform.DOMove(playerTarget, 0.3f).SetAutoKill(false).SetEase(Ease.OutElastic, .5f);
        pTargetLastPos = playerTarget;


        shadowTraget = new Vector3(transform.position.x, transform.position.y, playerTarget.z + 5f);
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
        shadowTraget = new Vector3(transform.position.x, transform.position.y, spawner._inst.getCurrentObstaclePosition().z - .5f);

        Debug.Log(shadowTraget);
        if (ShadowtargetLastPos == shadowTraget) return;
        // Add a Restart in the end, so that if the tween was completed it will play again
        var pos = shadowTraget;

        Shadowtween.ChangeEndValue(pos, true).Restart();
        ShadowtargetLastPos = shadowTraget;
    }

    void rotation()
    {
       
        if (!freezPlayerInput && Input.GetKeyDown(KeyCode.Space))
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
        return;
        /*
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
        */
    }
    void movement()
    {
        if (!freezPlayerInput)
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
        yield return new WaitForSeconds(.5f);
        var playerCubeIndex = 0;
        Vector3 distanceToCenter = cubes[0].transform.position - pivoitPoint;
        for (var x = 0; x < cubes.Count; x++)
        {
            if (!cubes[x].activeInHierarchy)
            {


                Vector3 pos = cubes[x].transform.position - distanceToCenter;


                shadowShapes[playerCubeIndex].SetActive(true);
                shadowShapes[playerCubeIndex].transform.DOMoveX(pos.x, .4f);
                shadowShapes[playerCubeIndex].transform.DOMoveY(pos.y, .4f);


              
                playerShapes[playerCubeIndex].SetActive(true);
                playerShapes[playerCubeIndex].transform.DOMoveX(pos.x, .4f);
                playerShapes[playerCubeIndex].transform.DOMoveY(pos.y, .4f);
                playerCubeIndex++;
            }
        }
        pivoitPoint = playerShapes[0].transform.position;
        freezPlayerInput = false;
    }
    IEnumerator decayShape(List<GameObject> cubes)
    {

        for (var x = 0; x < playerShapes.Count; x++)
        {
            var pos = pivoitPoint;

            shadowShapes[x].transform.DOMoveX(pos.x, .2f);
            shadowShapes[x].transform.DOMoveY(pos.y, .2f);

            playerShapes[x].transform.DOMoveX(pos.x, .2f);
            playerShapes[x].transform.DOMoveY(pos.y, .2f);

        }
        yield return new WaitForSeconds(.4f);
        for (var x = 0; x < playerShapes.Count; x++)
        {
            playerShapes[x].SetActive(false);
            shadowShapes[x].SetActive(false);
        }

    }
    Vector3 shapeDistance;
    GameObject getActiveCubeFromPlayerShape()
    {
        for (var x = 0; x < playerShapes.Count; x++)
        {
            if (playerShapes[x].activeInHierarchy)
                return playerShapes[x];
        }

        return null;
    }

    Vector3 lastCenterPoint;
    public void setPlayerShape(List<GameObject> cubes, GameObject obstcalePiviot)
    {
        freezPlayerInput = true;

        StartCoroutine(decayShape(cubes));
        StartCoroutine(buildShape(cubes));
    }


}
