using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayTimeSystem : MonoBehaviour
{
    public Light directionalLight;

    public float dayDurationInseconds = 24.0f;
    public int currentHour;
    public int currentMinus;
    public float currentSecond;
    public int speedDurationForSeconds;
    float currentTimeOfDay = 0.35f;
    float blendedValue = 0.0f;
    public List<SkyboxTimeMapping> timeMapping;

    public TextMeshProUGUI hourText;
    bool lockNextDayTrigger = false;

    private void Update()
    {
        currentTimeOfDay += Time.deltaTime / dayDurationInseconds;
        currentTimeOfDay %= 1;
        UpdateClock();
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3((currentTimeOfDay * 360) - 90, 170, 0));
        UpdateSkyBox();
    }

    private void UpdateClock()
    {

      
        hourText.text = $"0{currentHour}:0{currentMinus}";
        if (currentHour > 9 || currentMinus > 9)
        {
            hourText.text = $"{currentHour}:{currentMinus}";
        }
        currentSecond += Time.deltaTime * speedDurationForSeconds;
        if (currentSecond > 60)
        {
            currentSecond = 00;
            currentMinus++;
            if (currentMinus > 60)
            {
                currentMinus = 00;
                currentHour++;
                if (currentHour > 24)
                {
                    currentHour = 01;
                }
            }
        }

    }
    private void UpdateSkyBox()
    {
        Material currentSkybox = null;
        foreach (SkyboxTimeMapping timeMapping in timeMapping)
        {
            if (currentHour == timeMapping.hour)
            {
                currentSkybox = timeMapping.skyboxMaterial;

                if (currentSkybox.shader.name == "Custom/SkyboxTransition")
                {
                    blendedValue += Time.deltaTime;
                    blendedValue = Mathf.Clamp01(blendedValue);

                    currentSkybox.SetFloat("_TransitionFactor", blendedValue);
                }
                else
                {
                    blendedValue = 0;
                }
                break;
            }
        }
        if (currentHour == 0 && !lockNextDayTrigger)
        {
            TimeManager.Instance.TriggerNextday();
            lockNextDayTrigger = true;
        }

        if (currentHour != 0)
        {
            lockNextDayTrigger = false;
        }
        if (currentSkybox != null)
        {
            RenderSettings.skybox = currentSkybox;
        }
    }

    [Serializable]
    public class SkyboxTimeMapping
    {
        public string PhaseName;
        public int hour;// 0 - 23
        public Material skyboxMaterial;

    }
}
