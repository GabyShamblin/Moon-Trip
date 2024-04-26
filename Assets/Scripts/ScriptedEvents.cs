using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ScriptedEvents : MonoBehaviour
{
  private float time = 0f;
  private float waitTime = 0f;

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

  [Header("Throttles")]
  public LeftThrottleAnims leftThrottle;
  public RightThrottleAnims rightThrottle;

  [Header("Landing")]
  public GameObject moon;
  public bool landing = false;
  public float landingTime = 15f;
  private float moonStart = -100f;
  private float moonEnd = -6f;

  [Header("Sound")]
  public Narrator narrator;
  public AudioSource error;

  void Start()
  {
    skybox.SetFloat("_Blend", 0);

    moon.SetActive(false);
    moon.transform.position = new Vector3(moon.transform.position.x, moonStart, moon.transform.position.z);
    // zoom.Stop();
  }

  void Update()
  {
    if (waitTime > 0) {
      if (time < waitTime) {
        time += Time.deltaTime;
      }
      else if (Globals.phase == 0) {
        TakeOff();
      }
    }

    // Skybox blending
    if (blendStart) {
      // Increase blend
      blend += Time.deltaTime / blendTime;
      skybox.SetFloat("_Blend", blend);

      // If zoom is still playing and blend is over, stop
      if (zoom.isPlaying && blend >= zoomEnd) {
        // Debug.Log("Stop zoom");
        zoom.Stop();
        leftThrottle.ThrottleOff();
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

    // Force lights off after 3 seconds
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
        rightThrottle.ThrottleOff();
        moon.transform.position = new Vector3(moon.transform.position.x, moonEnd, moon.transform.position.z);
        time = 0f;
        landing = false;
        Globals.phase = 3;
      }
    }
  }

  // Wait 11s before take off
  public void PreTakeOff() {
    if (Globals.phase == 0) {
      leftThrottle.ThrottleOn();
      narrator.PlaySound();
      waitTime = 11;
    } else {
      error.Play();
    }
  }

  // Take off
  public void TakeOff() {
    blendStart = true;
    rocket.Play();
    Globals.phase = 1;
    waitTime = 0;
  }

  // Start landing sequence
  public void Land() {
    if (Globals.phase <= 2) {
      rightThrottle.ThrottleOn();
      landing = true;
      // rocket.Play();
    } else {
      error.Play();
    }
  }
}
