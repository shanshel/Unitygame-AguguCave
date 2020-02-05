using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static Monster _inst;
    private Animator _animator;
    Vector3 startScale, startPos;
    // Start is called before the first frame update

    private void Awake()
    {
        _inst = this;
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        startScale = transform.localScale;
        startPos = transform.localPosition;
        Invoke("Attack", 3f);
    }

    public void AttackAfter(float second)
    {
        Invoke("Attack", second);
        Invoke("StopSpeedOnPar", 0.1f);
    }
    public void Attack()
    {
        _animator.SetTrigger("Attack");
        StartCoroutine(spawnObstacle());
    }

    void StopSpeedOnPar()
    {
        GameManager._inst.SpeedOn.Stop();
    }

    IEnumerator spawnObstacle()
    {
        yield return new WaitForSeconds(2.5f);
        spawner._inst.SpawnObs();
        yield return null;
    }

    private void Update()
    {
        float scaleY = AudioPeer._audioBandBuffer[0];
        transform.localScale = new Vector3(transform.localScale.x, startScale.y + scaleY * .5f, transform.localScale.z);
        transform.localPosition = new Vector3(startPos.x, startPos.y + (scaleY * 1.5f), startPos.z);
    }

}
