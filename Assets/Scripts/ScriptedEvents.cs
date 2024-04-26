using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ScriptedEvents : MonoBehaviour
{
  private float time = 0f;

  [Header("Skybox Transition")]
  public Material skybox;
  public ParticleSystem zoom;
  public AudioSource rocket;
  public DynamicMoveProvider playerMove;
  public bool blendStart = false;
  public float blendTime = 5f;
  public float blend = 0f;
  private float zoomStart = 0.2f;
  private float zoomEnd = 1.5f;

  [Header("Landing")]
  public GameObject moon;
  public bool landing = false;
  public float landingTime = 15f;
  private float moonStart = -100f;
  private float moonEnd = -6f;

  [Header("Narrator")]
  public Narrator narr;

  void Start()
  {
    skybox.SetFloat("_Blend", 0);
    moon.transform.position = new Vector3(moon.transform.position.x, moonStart, moon.transform.position.z);
    // zoom.Stop();
  }

  void Update()
  {
    // Skybox blending
    if (blendStart) {
      // Play rocket sound
      if (blend <= 0) {
        rocket.Play();
      }

      // Increase blend
      blend += Time.deltaTime / blendTime;
      skybox.SetFloat("_Blend", blend);

      // If zoom is still playing and blend is over, stop
      if (zoom.isPlaying && blend >= zoomEnd) {
        // Debug.Log("Stop zoom");
        zoom.Stop();
        Globals.gravityOff = true;
        Globals.earthSpin = true;
      }
      // If zoom is not playing while blending, start
      else if (!zoom.isPlaying && blend >= zoomStart) {
        // Debug.Log("Play zoom");
        zoom.Play();
      }

      // Stop blend when it reaches its max
      if (blend >= 2) {
        playerMove.useGravity = false;
        blendStart = false;
        blend = 0f;
        Globals.phase = 2;
        Globals.lights = true;
      }
    }

    if (Globals.lights) {
      time += Time.deltaTime;
      if (time >= 3) {
        Globals.lights = false;
      }
    }

    // "Landing" - Bring moon surface to ship
    if (landing) {
      if (time <= 0) {
        moon.SetActive(true);
      }

      time += Time.deltaTime;
      float newy = (time / landingTime * Math.Abs(moonStart - moonEnd)) + moonStart;
      moon.transform.position = new Vector3(moon.transform.position.x, newy, moon.transform.position.z);
      
      if (time >= landingTime) {
        moon.transform.position = new Vector3(moon.transform.position.x, moonEnd, moon.transform.position.z);
        time = 0f;
        landing = false;
        Globals.phase = 3;
      }
    }
  }

  // Start take off
  public void TakeOff() {
    blendStart = true;
    Globals.phase = 1;
  }
}
