using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMobileProManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    public static void reportScore(int score)
    {

        GameServices.ReportScore(score, EM_GameServicesConstants.Leaderboard_agugu_high_score);
    }

}
