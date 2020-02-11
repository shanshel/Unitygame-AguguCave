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
        Invoke("Attack", 2f);
    }

    public void attackAfter(float time)
    {
        Invoke("StopSpeedOnPar", .4f);
        Invoke("Attack", time);
    }

    public void Attack()
    {


        
        float time = 2.5f;
        if (GameManager._inst.earthScore < 3)
        {
            time = 2.5f;
        }
        else if (GameManager._inst.earthScore < 6)
        {
            time = 2f;
        }
        else 
        {
            time = 1.5f;
        }
 

        Invoke("playAttackAnimation", time);
        Invoke("spawnObstacle", time + 1f);
    }

    void StopSpeedOnPar()
    {
        GameManager._inst.SpeedOn.Stop();
    }

    void playAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
    void spawnObstacle()
    {
        spawner._inst.SpawnObs();
    }
  

    private void Update()
    {
        float scaleY = AudioPeer._audioBandBuffer[0];
        transform.localScale = new Vector3(transform.localScale.x, startScale.y + (scaleY * 2f), transform.localScale.z);
        transform.localPosition = new Vector3(startPos.x, startPos.y + (scaleY * 1.5f), startPos.z);
    }

}
