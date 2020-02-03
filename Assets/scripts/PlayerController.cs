using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public static PlayerController _inst;
    [SerializeField]
    GameObject playerActionEffectPrefab;
 
    private float playerTimeout = 0f;
    public GameObject playerPiecePrefab, shadowPiecePrefab, shadowContainer;
    private List<GameObject> playerShapes = new List<GameObject>();
    private List<Vector3> playerShapePoss = new List<Vector3>();
    private List<GameObject> shadowShapes = new List<GameObject>();

    public GameObject PlayerFirstPiece;
    public GameObject pointPrefab;
    
    bool freezPlayerInput = false;
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
    private void Update()
    {
        if (GameManager._inst.isGameOver)
        {
           
            return;
        }
            
        playerTimeout -= Time.deltaTime;
        movement();
        rotation();
    }

    public void onPlayerDie()
    {
        transform.DOShakePosition(.3f, 1.5f, 5, 80f).SetLoops(-1, LoopType.Incremental);
    }
    
    public void onPlayerPass()
    {
        var pos = playerShapes[0].transform.position;
        pos.z -= 1.5f;
        var gamObj = Instantiate(pointPrefab, pos, Quaternion.identity, transform);
        gamObj.transform.localScale = new Vector3(2f, 2f, 2f);
        gamObj.transform.DOScale(0f, 2f).SetEase(Ease.Linear);
        gamObj.transform.DOMoveY(pos.y + 6f, 2f).SetEase(Ease.InOutElastic);
        GameManager._inst.score++;
        Invoke("addScore", 1f);
        Destroy(gamObj, 3f);
    }
    void addScore()
    {
        GameManager._inst.scoreText.text = GameManager._inst.score.ToString();
    }

    void rotation(int forceRotateCount = 0)
    {
        if ((!freezPlayerInput && Input.GetKeyDown(KeyCode.Space)) || forceRotateCount > 0)
        {
            if (forceRotateCount == 0) forceRotateCount = 1;
            freezPlayerInput = true;
            for (var ri = 0; ri < forceRotateCount; ri++)
            {
                SoundManager._inst.playSFX(EnumsData.SFXEnum.rotate);
                Instantiate(playerActionEffectPrefab, transform);
                Vector3 centerPoint = playerShapes[0].transform.position;
                for (var i = 0; i < playerShapePoss.Count; i++)
                {
                   
                    shadowShapes[i].transform.RotateAround(centerPoint, Vector3.forward, 90f);
                    playerShapes[i].transform.DOMoveX(shadowShapes[i].transform.position.x, .2f);
                    playerShapes[i].transform.DOMoveY(shadowShapes[i].transform.position.y, .2f);

                    //playerShapes[i].transform.RotateAround(centerPoint, Vector3.forward, 90f);
                    Vector3 shapePos = playerShapePoss[i];
                    shapePos.x = shadowShapes[i].transform.position.x;
                    shapePos.y = shadowShapes[i].transform.position.y;
                    playerShapePoss[i] = shapePos;
                }           
            }
            playerTimeout += .2f;
            freezPlayerInput = false;
        }
    }

  
    void movement()
    {
        Vector3 move = Vector3.zero;
        if (!freezPlayerInput && playerTimeout <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                move.y = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                move.y = -1;
            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                move.x = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                move.x = -1;
            }
        }
        float shadowZ = -10f;
        if (spawner._inst.getCurrentObstaclePosition() != Vector3.zero)
        {
            shadowZ = spawner._inst.getCurrentObstaclePosition().z - 2f;
        }
   
        shadowContainer.transform.position = new Vector3(shadowContainer.transform.position.x, shadowContainer.transform.position.y, shadowZ);
        if (move == Vector3.zero) return;
    

        freezPlayerInput = true;
        for (var i = 0; i < playerShapes.Count; i++) 
        {
            playerShapePoss[i] += move;
            var pos = playerShapePoss[i];
            playerShapes[i].transform.DOMove(pos, .1f);
            pos.z = shadowContainer.transform.position.z;
            shadowShapes[i].transform.DOMove(pos, .1f);
        }

        playerTimeout = .1f;
        freezPlayerInput = false;
    }



    IEnumerator buildShape(List<GameObject> cubes)
    {
        yield return new WaitForSeconds(.15f);
        var playerCubeIndex = 0;
        Vector3 distanceToCenter = Floor(cubes[0].transform.position) - Floor(playerShapes[0].transform.position); //- pivoitPoint;
        
        for (var x = 0; x < cubes.Count; x++)
        {
            if (!cubes[x].activeInHierarchy)
            {


                Vector3 pos = Floor( cubes[x].transform.position - distanceToCenter );


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
        yield return new WaitForSeconds(.21f);
        
        rotation(Random.Range(1, 3));
        freezPlayerInput = false;
       
       
    }
    IEnumerator decayShape(List<GameObject> cubes)
    {
       
        for (var x = 0; x < playerShapes.Count; x++)
        {
            var pos = Floor(playerShapes[0].transform.position);  
            shadowShapes[x].transform.DOMoveX(pos.x, .1f);
            shadowShapes[x].transform.DOMoveY(pos.y, .1f);
            playerShapes[x].transform.DOMoveX(pos.x, .1f);
            playerShapes[x].transform.DOMoveY(pos.y, .1f);

        }
        yield return new WaitForSeconds(.11f);
        for (var x = 0; x < playerShapes.Count; x++)
        {
            playerShapes[x].SetActive(false);
            shadowShapes[x].SetActive(false);
        }

    }

    public void setPlayerShape(List<GameObject> cubes, GameObject obstcalePiviot)
    {
       
        freezPlayerInput = true;
        StartCoroutine(decayShape(cubes));
        StartCoroutine(buildShape(cubes));
        playerTimeout = .4f;
    }


    public Vector3 Floor(Vector3 vector3)
    {
        return new Vector3(
            Mathf.Floor(vector3.x),
            Mathf.Floor(vector3.y ),
            Mathf.Floor(vector3.z ));
    }

}
