using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmOnAudio : MonoBehaviour
{
    Material _material;
    public int _band = 0;
    public bool changeOneColor;
    Color startColor;
    [ColorUsage(true, true)]
    public Color[] colors;
    float eachColorRange, colorTimer;
    int targetColorIndex;
    public float eachColorTime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
        startColor = _material.GetColor("_EmissionColor");
        eachColorRange = 1;
        if (colors.Length != 0)
        {
            eachColorRange = 1f / colors.Length;
            Debug.Log("EachColor: " + eachColorRange);
        }
        colorTimer = eachColorTime;




    }

    // Update is called once per frame

    void Update()
    {
        var almost = AudioPeer._audioBandBuffer[_band];
        Vector4 currentColorVector4 = _material.GetColor("_EmissionColor");
        Color currentColor = _material.GetColor("_EmissionColor");
        Vector4 newColor = Vector4.zero;
        
        if (colors.Length > 0)
        {
            colorTimer -= Time.deltaTime;
            if (colorTimer <= 0)
            {
                targetColorIndex++;
                if (targetColorIndex == colors.Length)
                    targetColorIndex = 0;
                colorTimer = eachColorTime;
            }
  

            newColor = Color.Lerp(currentColor, colors[targetColorIndex], Time.deltaTime);
            _material.SetColor("_EmissionColor", new Vector4(newColor.x, newColor.y, newColor.z, almost * 100f));
        }
        /*
        Color color = Color.white ;
        for (var i = 0; i < colors.Length; i++)
        {
            Debug.Log(almost);
            if (almost <= i)
            {
                color = colors[i];
                break;
            }
        }


        _material.SetColor("_EmissionColor", color);
        */
        


    }
}
