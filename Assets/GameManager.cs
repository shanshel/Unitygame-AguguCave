using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public ParticleSystem SpeedOn;
    public static GameManager _inst;
    public float globalScrollSpeed = 1;
    public float gamePlayTime;
    public float songPlayTime;
    public int songCircale;

    public int cubePerRow = 8;
    float repeatSongEvery = 189.832f;


    private void Awake()
    {
        _inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gamePlayTime += Time.deltaTime;

        if (Mathf.Floor(gamePlayTime / repeatSongEvery) > songCircale)
            songCircale++;

        songPlayTime = (GameManager._inst.gamePlayTime - (songCircale * repeatSongEvery));

    }
}
