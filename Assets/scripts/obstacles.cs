using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class obstacles : MonoBehaviour
{
    public float speed = 10;
    public ParticleSystem speedon;
    public bool isPassed = false;
    public GameObject obstacleCube, pivoitObject, hardContainer, passWall, dangerWall;
    private GameObject[] cubes = new GameObject[48];
    public int cubePerRow = 8;

    
    public int touchCount, touchCountRequired;


    bool isReadyToMove;
    Vector3 dangerWallScale, passWallScale;
    private void Start()
    {
        dangerWallScale = dangerWall.transform.localScale;
        passWallScale = passWall.transform.localScale;
        dangerWall.transform.localScale = Vector3.zero;
        passWall.transform.localScale = Vector3.zero;
        for (var x = 0; x < cubes.Length; x++)
        {
        
            var pos = transform.position;
            var round = Mathf.Floor(x / cubePerRow);
            pos.x += x - (cubePerRow * round);
            pos.y -= round;
            
            GameObject cube = Instantiate(obstacleCube, new Vector3(0f, 20f, 0f), Quaternion.identity, hardContainer.transform);
        
            cubes[x] = cube;
            cubes[x].transform.DOMove(pos, Random.Range(.2f, .6f)).SetEase(Ease.InOutBounce);


        }
        Invoke("GenerateRandomShape", .65f);
        spawner._inst.activeObstacles.Add(this);
    }

    void GenerateRandomShape()
    {
        dangerWall.transform.DOScale(dangerWallScale, .3f).SetEase(Ease.InFlash);
        passWall.transform.DOScale(passWallScale, .5f).SetEase(Ease.InFlash);

   
        isReadyToMove = true;
        for (var x = 0; x < cubes.Length; x++)
        {
            cubes[x].SetActive(true);
        }

        var holeCubeCount = Random.Range(1, 16);
        int agreedCubeHoleCount = 0;
        List<int> cubeToDisableIndex = new List<int>();
        List<GameObject> agreedCubes = new List<GameObject>();


        

        for (var x = 0; x < holeCubeCount; x ++)
        {
            if (x == 0)
            {
                var randomIndex = Random.Range(0, cubes.Length - 1);
                cubeToDisableIndex.Add(randomIndex);
                agreedCubes.Add(cubes[randomIndex]);
                agreedCubeHoleCount++;
                cubes[randomIndex].SetActive(false);
            }
            else
            {
                var dir = getDirection(cubeToDisableIndex[cubeToDisableIndex.Count - 1]);
                var cubeIndex = getCubeIndexOnDirection(cubeToDisableIndex[cubeToDisableIndex.Count - 1], dir);
                if (!cubeToDisableIndex.Contains(cubeIndex))
                {
                    cubeToDisableIndex.Add(cubeIndex);
                    agreedCubes.Add(cubes[cubeIndex]);
                    agreedCubeHoleCount++;
                    cubes[cubeIndex].SetActive(false);
                }
            }
        }



        touchCountRequired = agreedCubes.Count;
        PlayerController._inst.setPlayerShape(agreedCubes, pivoitObject);
    }


    int getCubeIndexOnDirection(int cubeIndex, EnumsData.FourDirectionEnum cubeDirection)
    {

        if (cubeDirection == EnumsData.FourDirectionEnum.Bottom)
        {
            return cubeIndex + cubePerRow;
        }
        else if (cubeDirection == EnumsData.FourDirectionEnum.Top)
        {
            return cubeIndex - cubePerRow;
        }
        else if (cubeDirection == EnumsData.FourDirectionEnum.Right)
        {
            return cubeIndex + 1;
        }
        return cubeIndex - 1;
    }
    EnumsData.FourDirectionEnum getDirection(int cubeIndex)
    {
        var rand = Random.Range(0, 4);
        if (cubeIndex == 0)
        {
            
            if (rand > 2)
            {
                Debug.Log("Next To First On Right: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Right;
            }
            else
            {
                Debug.Log("Next To First On Bottom: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Bottom;
            }
        }

        else if (cubeIndex == cubes.Length - 1)
        {
            //return EnumsData.CubeTypeEnum.BottomRightCorner;
            if (rand > 2)
            {
                Debug.Log("Next To BottomRightCorner On Top: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Top;
            }
            else
            {
                Debug.Log("Next To BottomRightCorner On Left: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Left;
            }
        }
        else if (cubeIndex == cubePerRow - 1 )
        {

            //return EnumsData.CubeTypeEnum.TopRightCorner;

            if (rand > 2)
            {
                Debug.Log("Next To TopRightCorner On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
            else
            {
                Debug.Log("Next To TopRightCorner On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
        }
        else if (cubeIndex == cubes.Length - cubePerRow)
        {

            //return EnumsData.CubeTypeEnum.BottomLeftCorner;
            if (rand > 2)
            {
                Debug.Log("Next To BottomLeftCorner On Top: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Top;
            }
            else
            {
                Debug.Log("Next To BottomLeftCorner On Right: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Right;
            }
        }
        else if (Mathf.Floor(cubeIndex / cubePerRow) == 0)
        {
            //return EnumsData.CubeTypeEnum.TopEdge;
            if (rand == 0)
            {
                Debug.Log("Next To TopEdge On Right: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Right;
            }
            else if (rand == 1)
            {
                Debug.Log("Next To TopEdge On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
            else
            {
                Debug.Log("Next To TopEdge On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
        }
        else if (Mathf.Floor(cubeIndex / cubePerRow) == Mathf.Floor( ( cubes.Length - 1 ) / cubePerRow))
        {
         
            //return EnumsData.CubeTypeEnum.BottomEdge;
            if (rand == 0)
            {
                Debug.Log("Next To BottomEdge On Right: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Right;
            }
            else if (rand == 1)
            {
                Debug.Log("Next To BottomEdge On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
            else
            {
                Debug.Log("Next To BottomEdge On Top: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Top;
            }
        }
        else if (cubeIndex % cubePerRow == 0)
        {
            //return EnumsData.CubeTypeEnum.RightEdge;
            if (rand == 0)
            {
                Debug.Log("Next To LeftEdge On Top: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Top;
            }
            else if (rand == 1)
            {
                Debug.Log("Next To LeftEdge On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
            else
            {
                Debug.Log("Next To LeftEdge On Left: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Right;
            }
        }
        else if (cubeIndex - (cubePerRow *  Mathf.Floor(cubeIndex / cubePerRow)) == cubePerRow - 1)
        {
            
            //return EnumsData.CubeTypeEnum.LeftEdge;
            if (rand == 0)
            {
                Debug.Log("Next To RightEdge On Top: " + cubeIndex);
                return EnumsData.FourDirectionEnum.Top;
            }
            else if (rand == 1)
            {
                Debug.Log("Next To RightEdge On Bottom: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Bottom;
            }
            else
            {
                Debug.Log("Next To RightEdge On Right: " + cubeIndex);

                return EnumsData.FourDirectionEnum.Left;
            }
        }


        if (rand == 0)
        {
            Debug.Log("Next To Center On Top: " + cubeIndex);

            return EnumsData.FourDirectionEnum.Top;
        }
        else if (rand == 1)
        {
            Debug.Log("Next To Center On Bottom: " + cubeIndex);

            return EnumsData.FourDirectionEnum.Bottom;
        }
        else if (rand == 2)
        {
            Debug.Log("Next To Center On Right: " + cubeIndex);

            return EnumsData.FourDirectionEnum.Right;
        }
        Debug.Log("Next To Center On Left: " + cubeIndex);

        return EnumsData.FourDirectionEnum.Left;

        //return EnumsData.CubeTypeEnum.Any;
    }

    
    private void Update()
    {
        if (isReadyToMove)
            transform.Translate(Vector3.back * speed * Time.deltaTime);
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
        speed = 25;
        spawner._inst.activeObstacles.Remove(this);
        spawner._inst.playerPass();
        Destroy(gameObject, 2f);
    }


 
}
