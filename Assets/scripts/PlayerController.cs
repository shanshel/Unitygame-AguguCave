using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController _inst;
    [SerializeField]
    GameObject playerActionEffectPrefab;
    [SerializeField]
    public GameObject playerPiecePrefab, shadowPiecePrefab, shadowContainer;
    public List<GameObject> playerShapes = new List<GameObject>();
    private List<Vector3> playerShapePoss = new List<Vector3>();
    private List<GameObject> shadowShapes = new List<GameObject>();

    public GameObject PlayerFirstPiece;
    public GameObject pointPrefab;
    
    bool freezPlayerInput = false;

    float rotateTimer;

    public Joystick joystick;
    private void Awake()
    {
        _inst = this;
    }

   
    private void Start()
    {
        playerShapes.Add(PlayerFirstPiece);
        playerShapePoss.Add(PlayerFirstPiece.transform.position);

        for (var x = 0; x < 16; x++)
        {
            GameObject cube = Instantiate(playerPiecePrefab, transform);
            playerShapes.Add(cube);
            playerShapePoss.Add(cube.transform.position);
        }

        for (var x = 0; x < 17; x++)
        {
            GameObject cube = Instantiate(shadowPiecePrefab, shadowContainer.transform);
            shadowShapes.Add(cube);
        }
    }
    public Vector3 moveTarget, lastMoveTarget;


    private void Update()
    {

        if (GameManager._inst.isGameOver)
        {
            return;
        }


        rotateTimer -= Time.deltaTime;

        doupleClickDetection();
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveTarget.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moveTarget.y = -1;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveTarget.x = 1;


        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            moveTarget.x = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotation(1);
        }


        movement(moveTarget);
    }

    float delay = .2f;
    float clickAtTime;
    void doupleClickDetection()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            clickAtTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if ( (Time.time - clickAtTime) < delay )
            {
                rotation(1);
            }
        }
    }


    public void rotation(int forceRotateCount = 1)
    {
        if (rotateTimer > 0f) return;
        rotateTimer = .1f;
        if (forceRotateCount == 0) forceRotateCount = 1;
        var angle = forceRotateCount * 90f;
        SoundManager._inst.playSFX(EnumsData.SFXEnum.rotate);
        Instantiate(playerActionEffectPrefab, transform);
        Vector3 centerPoint = playerShapes[0].transform.position;
        Handheld.Vibrate();
        for (var i = 0; i < playerShapePoss.Count; i++)
        {
                   
            shadowShapes[i].transform.RotateAround(centerPoint, Vector3.forward, angle);
            playerShapes[i].transform.DOMoveX(shadowShapes[i].transform.position.x, .15f);
            playerShapes[i].transform.DOMoveY(shadowShapes[i].transform.position.y, .15f);

            //playerShapes[i].transform.RotateAround(centerPoint, Vector3.forward, 90f);
            Vector3 shapePos = playerShapePoss[i];
            shapePos.x = shadowShapes[i].transform.position.x;
            shapePos.y = shadowShapes[i].transform.position.y;
            playerShapePoss[i] = shapePos;
        }
      
    }


    void movement(Vector3 move)
    {
       
     

     

        float shadowZ = -50f;
        if (spawner._inst.getCurrentObstaclePosition() != Vector3.zero)
        {
            shadowZ = spawner._inst.getCurrentObstaclePosition().z - .5f;
        }
   
        shadowContainer.transform.position = new Vector3(shadowContainer.transform.position.x, shadowContainer.transform.position.y, shadowZ);
        if (rotateTimer > 0f || freezPlayerInput)
            return;
        if (move == lastMoveTarget) return;
        lastMoveTarget = move;
        Vector3 travelDistance = Vector3.zero;


        for (var i = 0; i < playerShapes.Count; i++) 
        {
            if (i == 0)
            {
                travelDistance = move - playerShapePoss[i];
                playerShapePoss[i] = move;
                var pos = playerShapePoss[i];
                playerShapes[i].transform.position = pos;
                pos.z = shadowContainer.transform.position.z;
                shadowShapes[i].transform.position = pos;
            }
            else
            {
                playerShapePoss[i] += travelDistance;
                var pos = playerShapePoss[i];
                playerShapes[i].transform.position = pos;
                pos.z = shadowContainer.transform.position.z;
                shadowShapes[i].transform.position = pos;
            }
            
        }
        

    }


    IEnumerator reshape(List<GameObject> cubes)
    {
   
        for (var x = 0; x < playerShapes.Count; x++)
        {
            var pos = playerShapes[0].transform.position;
            shadowShapes[x].transform.DOMoveX(pos.x, .1f);
            shadowShapes[x].transform.DOMoveY(pos.y, .1f);
            playerShapes[x].transform.DOMoveX(pos.x, .1f);
            playerShapes[x].transform.DOMoveY(pos.y, .1f);
        }
        for (var x = 0; x < playerShapes.Count; x++)
        {
            playerShapes[x].SetActive(false);
            shadowShapes[x].SetActive(false);
        }
        yield return new WaitForSeconds(.1f);

        var playerCubeIndex = 0;
        Vector3 distanceToCenter = cubes[0].transform.position - playerShapes[0].transform.position;


        for (var x = 0; x < cubes.Count; x++)
        {
            if (!cubes[x].activeInHierarchy)
            {


                Vector3 pos = cubes[x].transform.position - distanceToCenter;


                shadowShapes[playerCubeIndex].SetActive(true);
                shadowShapes[playerCubeIndex].transform.DOMoveX(pos.x, .2f);
                shadowShapes[playerCubeIndex].transform.DOMoveY(pos.y, .2f);



                playerShapes[playerCubeIndex].SetActive(true);
                playerShapes[playerCubeIndex].transform.DOMoveX(pos.x, .2f);
                playerShapes[playerCubeIndex].transform.DOMoveY(pos.y, .2f);
                playerShapePoss[x] = pos;
                playerCubeIndex++;
            }
        }
        yield return new WaitForSeconds(.2f);

        rotation(Random.Range(1, 3));
        yield return new WaitForSeconds(.1f);

        freezPlayerInput = false;
    }


    public void setPlayerShape(List<GameObject> cubes, GameObject obstcalePiviot)
    {
       
        freezPlayerInput = true;
        StartCoroutine(reshape(cubes));
    }

    // Player Die & Pass 
    public void onPlayerDie()
    {
        transform.DOShakePosition(.3f, 1.5f, 5, 80f).SetLoops(-1, LoopType.Incremental);
    }

    public void onPlayerPass(int scorePoint)
    {
        PlayerHeadAnim._inst.playerAnimWhenObstcalePassed();
        var pos = playerShapes[0].transform.position;
        pos.z -= 1.5f;
        var gamObj = Instantiate(pointPrefab, pos, Quaternion.identity, transform);

        var txt = gamObj.GetComponent<TextMeshPro>();
        txt.text = "+" + scorePoint.ToString();
        gamObj.transform.localScale = new Vector3(2f, 2f, 2f);
        gamObj.transform.DOScale(0f, 2f).SetEase(Ease.Linear);
        gamObj.transform.DOMoveY(pos.y + 5f, 2f).SetEase(Ease.InOutElastic);
        GameManager._inst.score += scorePoint;
        GameManager._inst.earthScore += 1;
        Invoke("addScore", 1f);
        Destroy(gamObj, 3f);
        rotateTimer = 1.4f;
    }
    void addScore()
    {
        GameManager._inst.scoreText.text = GameManager._inst.score.ToString();
    }

}
