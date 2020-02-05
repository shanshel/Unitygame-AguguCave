using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadAnim : MonoBehaviour
{
    public static PlayerHeadAnim _inst;

    Animator _anim;
    public GameObject borw1, brow2, mustache;

    Vector3 brow1Pos, brow2Pos, mustachePos;

    private void Awake()
    {
        _inst = this;
        _anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        brow1Pos = borw1.transform.localPosition;
        brow2Pos = brow2.transform.localPosition;
        mustachePos = mustache.transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
 

        float peer1 = AudioPeer._audioBandBuffer[0];
        float peer2 = AudioPeer._audioBandBuffer[1];

        borw1.transform.localPosition = new Vector3(brow1Pos.x, brow1Pos.y + peer1, brow1Pos.z);
        brow2.transform.localPosition = new Vector3(brow2Pos.x, brow2Pos.y + peer1, brow2Pos.z);
        mustache.transform.localRotation = Quaternion.Euler(mustachePos.x, mustachePos.y, mustachePos.z + (peer2 * 10f));

    }

    public void playerAnimWhenSpawnObstcale()
    {
        _anim.SetTrigger("WhenObstcaleSpawn");
    }

    public void playerAnimWhenObstcalePassed()
    {
  
        _anim.SetTrigger("WhenObstcalePassed");



    }
}
