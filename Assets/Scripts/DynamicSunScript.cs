using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class DynamicSunScript : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private DynamicSunObject Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    public Volume volume;
    private ColorAdjustments color;
    private WhiteBalance whiteBalance;

    private void OnValidate()
    {
        if(DirectionalLight != null)
            return;

        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] Lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in Lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void Start()
    {
        volume.profile.TryGet<ColorAdjustments>(out color);
        volume.profile.TryGet<WhiteBalance>(out whiteBalance);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        
        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0f));
        }
    }

    private void UpdatePostProcessing(float time)
    {
        if(time > 5.0f && time < 16.0f)
        {
            color.postExposure.value = Mathf.Lerp(color.postExposure.value, 0.5f, Time.deltaTime / 4);
            whiteBalance.temperature.value = Mathf.Lerp(whiteBalance.temperature.value, 50f, Time.deltaTime / 4);
        }
        else
        {
            color.postExposure.value = Mathf.Lerp(color.postExposure.value, 1.5f, Time.deltaTime / 4);
            whiteBalance.temperature.value = Mathf.Lerp(whiteBalance.temperature.value, -50f, Time.deltaTime / 4);
        }
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime / 2;
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
            UpdatePostProcessing(TimeOfDay);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

}