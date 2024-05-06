using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
  private Light light;
  private float maxInterval = 1;
  private float maxFlicker = 0.2f;

  float defaultIntensity;
  bool isOn = false;
  float timer = 0;
  float delay;
  float flickerTime;

  void Start()
  {
    light = GetComponent<Light>();
    defaultIntensity = light.intensity;
    light.intensity = 0.5f;

    flickerTime = Random.Range(1, 3);
  }

  void Update()
  {
    if (Globals.lights) {
      timer += Time.deltaTime;
      if (timer > delay && timer < flickerTime)
      {
        // Debug.Log("Toggle " + timer + " " + flickerTime);
        ToggleLight();
      }
    }

    if (timer >= flickerTime && !isOn) {
      ToggleOn();
    }
  }

  void ToggleLight()
  {
    isOn = !isOn;

    if (isOn)
    {
      light.intensity = defaultIntensity;
      delay = Random.Range(0, maxInterval);
    }
    else
    {
      light.intensity = Random.Range(0.6f, defaultIntensity);
      delay = Random.Range(0, maxFlicker);
    }

    timer = 0;
  }

  void ToggleOn() {
    isOn = true;
    light.intensity = defaultIntensity;
  }
}
