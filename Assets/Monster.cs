using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static Monster _inst;
    private Animator _animator;
    private Shake shake;

    // Start is called before the first frame update

    private void Awake()
    {
        _inst = this;
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        Invoke("Attack", 3f);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        Invoke("StopSpeedOnPar", 0.1f);
        StartCoroutine(spawnObstacle());
    }

    void StopSpeedOnPar()
    {
        GameManager._inst.SpeedOn.Stop();
    }

    IEnumerator spawnObstacle()
    {
        yield return new WaitForSeconds(1.5f);
        spawner._inst.SpawnObs();
        yield return null;
    }

}
