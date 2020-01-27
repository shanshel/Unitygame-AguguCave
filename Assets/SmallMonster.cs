using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallMonster : MonoBehaviour
{

    private void Start()
    {
        transform.DOBlendablePunchRotation(new Vector3(1f, 1f, 1f), 1f, 6, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InElastic);
    }
}
