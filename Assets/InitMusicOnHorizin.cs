using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMusicOnHorizin : MonoBehaviour
{
    public float power = 1000;
    public EmmOnAudio musicStepPrefab;
    EmmOnAudio[] array = new EmmOnAudio[512];

    // Start is called before the first frame update
    void Start()
    {
        for (var x=0; x < 512; x++)
        {
            var pos = transform.position;
            pos.x += (x * 1);
            var gameobj = Instantiate(musicStepPrefab, pos, transform.rotation, transform);
            //gameobj._band = x;
            gameobj.changeOneColor = true;
            array[x] = gameobj;
        }
    }

    private void Update()
    {
        for (var x = 0; x < 512; x++)
        {
            if (array[x] != null)
            {
                array[x].transform.localScale = new Vector3(1, AudioPeer._samples[x] * 5000f, 1);

            }
        }
    }

}
