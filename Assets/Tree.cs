using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tree : MonoBehaviour
{


    public GameObject treeBranch;
    Vector3 treeBranchPos;
    public int treeBranchBand;
    // Start is called before the first frame update

    Sequence _seq;
    Dictionary<float, int> plan = new Dictionary<float, int> {
        { 14.5f, -1 },
        { 48.3f, -1  },
        { 63.2f, -1  },
        { 120f, -1  },
        { 140.5f, -1  },
        { 174f, -1  },
    };
    
    void Start()
    {
        _seq = DOTween.Sequence();
        treeBranchPos = treeBranch.transform.localPosition;
        InvokeRepeating("danceStatusUpdate", 2f, .5f);
    }

    void danceStatusUpdate()
    {
        _seq.Insert(1, transform.DOShakeRotation(.5f, AudioPeer._audioBandBuffer[1] * 50f, 4, 45f, true));
    }
    private void Update()
    {

        treeBranch.transform.localPosition = new Vector3(treeBranchPos.x, treeBranchPos.y + (AudioPeer._audioBandBuffer[treeBranchBand] * 3.5f), treeBranchPos.z);
        float scaleY = AudioPeer._audioBandBuffer[0];
        if (scaleY < .65f)
        {
            scaleY = .65f;
        }
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);

        /*
    float time = GameManager._inst.songPlayTime;
    int circle = GameManager._inst.songCircale;
    if (circle > plan[14.5f] && time > 14.5f)
    {
        playAt(14.5f);
    }
    if (circle > plan[48.3f] && time > 48.3f)
    {
        pauseAt(48.3f);
    }
    if (circle > plan[63.2f] && time > 63.2f)
    {
        playAt(63.2f);
    }
    if (circle > plan[120f] && time > 120f)
    {
        pauseAt(120f);
    }
    if (circle > plan[140.5f] && time > 140.5f)
    {
        playAt(140.5f);
    }
    if (circle > plan[174f] && time > 174f)
    {
        pauseAt(174f);
    }
    */


    }
}
