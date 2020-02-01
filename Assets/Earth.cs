using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{

    public GameObject earthPiece;
    private Vector3 startScale;
    // Start is called before the first frame update
    void Start()
    {

        startScale = transform.localScale;
        for (var i = 0; i < 8; i++)
        {
            var pos = transform.position;
            pos.z += (15f * i);
            for (var x = 0; x < 15; x++)
            {
                
                Instantiate(earthPiece, new Vector3(pos.x + (7.5f * x), pos.y, pos.z + (7.5f * x)), earthPiece.transform.rotation, transform);
            }
            for (var x = 0; x < 15; x++)
            {
                Instantiate(earthPiece, new Vector3(pos.x - (7.5f * x), pos.y, pos.z + (7.5f * x)), earthPiece.transform.rotation, transform);
            }
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        var band = AudioPeer._audioBandBuffer[0] * 25f;
        transform.localScale = new Vector3(startScale.x, band, startScale.z);
    }
}
