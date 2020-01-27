using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Tree : MonoBehaviour
{
    Tweener _tweener;

    // Start is called before the first frame update

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
        _tweener = transform.DOShakeRotation(.8f, 35f, 4, 45f, true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InCirc).Pause();


    }

    private void Update()
    {
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


    }

    void playAt(float time)
    {
        plan[time] += 1;
        _tweener.Play();
        
    }

    void pauseAt(float time)
    {
        plan[time] += 1;
        _tweener.Pause();
    }

}
