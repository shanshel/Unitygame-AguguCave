using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessEffect : MonoBehaviour
{
    public static PostProcessEffect _inst;

    //profiling
    public float[] tempratures;
    public float[] tints;
    public float[] saturations;
    public Vector4[] lifts;

    int targetProfile;
    float profileChangingTimer;
    Vector4 targettLift;
    float targetTint, targetTemprature, targetSaturation;

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


    public void changeProfile(int profileIndex = -1)
    {
        if (profileIndex == -1)
        {
            profileIndex = targetProfile + 1;
            if (profileIndex == tempratures.Length)
                profileIndex = 0;
        }

        Debug.Log("Load Profile: " + profileIndex);
        if (tempratures.Length + 
            tints.Length +
            saturations.Length + 
            lifts.Length == (tempratures.Length * 4))
        {
            targetProfile = profileIndex;
            profileChangingTimer = 1.5f;
        }
    }

    IEnumerator resetLens(float time)
    {
        yield return new WaitForSeconds(time);
        lenCenterYTarget = lenDefaultCenterY;
        lenScaleTarget = lenDefaultScale;
    }


    private void Update()
    {
        
        if (profileChangingTimer > 0f)
        {
            Debug.Log("Move Toward Profile: " + targetProfile);
            colorFilter.temperature.value = Mathf.MoveTowards(colorFilter.temperature.value, tempratures[targetProfile], Time.deltaTime * 5f);
            colorFilter.tint.value = Mathf.MoveTowards(colorFilter.tint.value, tints[targetProfile], Time.deltaTime * 5f);
            colorFilter.saturation.value = Mathf.MoveTowards(colorFilter.saturation.value, saturations[targetProfile], Time.deltaTime * 5f);
            colorFilter.lift.value = Vector4.MoveTowards(colorFilter.lift.value, lifts[targetProfile], Time.deltaTime * 5f);
            profileChangingTimer -= Time.deltaTime;
        }


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
