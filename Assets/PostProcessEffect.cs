using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessEffect : MonoBehaviour
{
    public static PostProcessEffect _inst;

    private PostProcessVolume m_PostProcessVolume;
    private LensDistortion lensProfile;
    private ColorGrading colorFilter;
    private float lenDefaultCenterY, lenCenterYTarget,
        lenDefaultScale, lenScaleTarget;
    private void Awake()
    {
        _inst = this;
        m_PostProcessVolume = GetComponent<PostProcessVolume>();
        m_PostProcessVolume.profile.TryGetSettings(out lensProfile);
        m_PostProcessVolume.profile.TryGetSettings(out colorFilter);

    }

    private void Start()
    {
        lenDefaultCenterY = lensProfile.centerY.value;
        lenDefaultScale = lensProfile.scale.value;
        lenCenterYTarget = lenDefaultCenterY;
        lenScaleTarget = lenDefaultScale;
    }

    public void setLens(float time, float centerY, float scale)
    {
        StartCoroutine(resetLens(time));
        lenCenterYTarget = centerY;
        lenScaleTarget = scale;

    }
    IEnumerator resetLens(float time)
    {
        yield return new WaitForSeconds(time);
        lenCenterYTarget = lenDefaultCenterY;
        lenScaleTarget = lenDefaultScale;
    }

    private void Update()
    {
        if (false)
        {
            var colorF = AudioPeer._audioBandBuffer[0];
            if (AudioPeer._audioBandBuffer[0] < .85f)
            {
                colorF = .85f;
            }
            colorFilter.colorFilter.value = new Color(colorF, colorF, colorF);
            if (!Mathf.Equals(lenCenterYTarget, lensProfile.centerY.value))
            {
                lensProfile.centerY.value = Mathf.MoveTowards(lensProfile.centerY.value, lenCenterYTarget, Time.deltaTime);
            }

            if (!Mathf.Equals(lenScaleTarget, lensProfile.scale.value))
            {
                lensProfile.scale.value = Mathf.MoveTowards(lensProfile.scale.value, lenScaleTarget, Time.deltaTime);
            }

        }


    }


}
