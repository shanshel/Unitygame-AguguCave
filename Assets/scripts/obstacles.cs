using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class obstacles : MonoBehaviour
{
    
    public ParticleSystem speedon;
    public bool isPassed = false;
    public GameObject obstacleCube, pivoitObject, hardContainer, passWall, dangerWall, checkPassContainer;
    private GameObject[] cubes;
    public GameObject obstHolePrefab;
    int cubePerRow = 3, totalCubes = 9;

    public int earnPoint = 10;
    
    public int touchCount, touchCountRequired;


    bool isReadyToMove;
    Vector3 dangerWallScale, passWallScale;
    private void Start()
    {
        spawner._inst.activeObstacles.Add(this);
        StartCoroutine(buildObstacle());
    }

    IEnumerator buildObstacle()
    {
        if (GameManager._inst.earthScore < 5)
        {
            cubePerRow = 3;
        }
        else if (GameManager._inst.earthScore < 10)
        {
            cubePerRow = 4;
        }
        else if (GameManager._inst.earthScore < 20)
        {
            cubePerRow = 5;
        }
        else if (GameManager._inst.earthScore < 40)
        {
            cubePerRow = 6;
        }
        else
        {
            cubePerRow = Random.Range(3, 6);
        }
        totalCubes = cubePerRow * cubePerRow;
        cubes = new GameObject[totalCubes];
     



        dangerWallScale = dangerWall.transform.localScale;
        passWallScale = passWall.transform.localScale;
        dangerWall.transform.localScale = Vector3.zero;
        passWall.transform.localScale = Vector3.zero;

        int xMove = Random.Range(-1, 2);
        int yMove = Random.Range(0, 7);
        var currentPos = transform.position;
        transform.position = new Vector3(currentPos.x + xMove, currentPos.y + yMove, currentPos.z);

        yield return 0;
        for (var x = 0; x < cubes.Length; x++)
        {

            var pos = transform.position;
            var round = Mathf.Floor(x / cubePerRow);
            pos.x += x - (cubePerRow * round);
            pos.y -= round;

            GameObject cube = Instantiate(obstacleCube, new Vector3(0f, 0f, 100f), Quaternion.identity, hardContainer.transform);

            cubes[x] = cube;
            cubes[x].transform.DOMove(pos, Random.Range(.2f, .6f)).SetEase(Ease.InOutBounce);
            if (x % 2 == 0)
                yield return 0;
        }
        dangerWall.transform.DOScale(dangerWallScale, .3f).SetEase(Ease.InFlash);
        passWall.transform.DOScale(passWallScale, .5f).SetEase(Ease.InFlash);
        yield return new WaitForSeconds(.6f);
    

        /*
        for (var x = 0; x < cubes.Length; x++)
        {
            cubes[x].SetActive(true);
        }
        */
     

        var holeCubeCount = Random.Range(1, cubePerRow * 2);
        int agreedCubeHoleCount = 0;
        List<int> cubeToDisableIndex = new List<int>();
        List<GameObject> agreedCubes = new List<GameObject>();


        for (var x = 0; x < holeCubeCount; x++)
        {
            if (x == 0)
            {
                var randomIndex = Random.Range(0, cubes.Length - 1);
                cubeToDisableIndex.Add(randomIndex);
                agreedCubes.Add(cubes[randomIndex]);
                agreedCubeHoleCount++;
                Instantiate(obstHolePrefab, cubes[randomIndex].transform.position, Quaternion.identity, checkPassContainer.transform);
                cubes[randomIndex].SetActive(false);
            }
            else
            {
                var dir = GameManager._inst.getDirection(cubeToDisableIndex[cubeToDisableIndex.Count - 1], cubes.Length, cubePerRow);
                var cubeIndex = GameManager._inst.getCubeIndexOnDirection(cubeToDisableIndex[cubeToDisableIndex.Count - 1], dir, cubePerRow);
                if (!cubeToDisableIndex.Contains(cubeIndex))
                {
                    cubeToDisableIndex.Add(cubeIndex);
                    agreedCubes.Add(cubes[cubeIndex]);
                    agreedCubeHoleCount++;
                    Instantiate(obstHolePrefab, cubes[cubeIndex].transform.position, Quaternion.identity, checkPassContainer.transform);
                    cubes[cubeIndex].SetActive(false);
                }
            }
            yield return 0;
        }



        touchCountRequired = agreedCubes.Count;
        yield return 0;
        PlayerController._inst.setPlayerShape(agreedCubes, pivoitObject);
    
        yield return new WaitForSeconds(.1f);
        isReadyToMove = true;
    }






    
    private void FixedUpdate()
    {
        if (isReadyToMove)
            transform.Translate(Vector3.back * (GameManager._inst.globalScrollSpeed) * Time.deltaTime);
    }


    public void obstFail()
    {
        if (isPassed) return;
        spawner._inst.playerFail();
        isPassed = true;
    }
    public void obstPass()
    {
       
        if (isPassed) return;
        isPassed = true;
        spawner._inst.activeObstacles.Remove(this);
        spawner._inst.playerPass(earnPoint);
        Destroy(gameObject, 2f);
    }


 
}
