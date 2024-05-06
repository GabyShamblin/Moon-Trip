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
  public float landingTime = 10f;
  private float moonStart = -100f;
  private float moonEnd = -6f;

  [Header("Sound")]
  public Narrator narrator;
  public AudioSource error;

  [Header("Player")]
  public DynamicMoveProvider playerMove;
  public Rigidbody playerRigid;

  void Start()
  {
    skybox.SetFloat("_Blend", 0);

    // moon.SetActive(false);
    moon.transform.position = new Vector3(moon.transform.position.x, moonStart, moon.transform.position.z);
    // zoom.Stop();
  }

  void Update()
  {
    if (waitTime > 0) {
      if (time < waitTime) {
        time += Time.deltaTime;
      } else {
        if (Globals.phase == 0 || Globals.phase == 1) {
          TakeOff();
        }
        else if (Globals.phase == 2) {
          Globals.lights = true; 
          narrator.PlaySound(2);
        }

        time = 0;
        waitTime = 0;
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
        playerMove.enableFly = true;
        blendStart = false;
        blend = 0f;
        Globals.phase = 2;
        waitTime = 2;
      }
    }

    // Force lights off after 3 seconds
    if (Globals.lights) {
      time += Time.deltaTime;
      if (time >= 3) {
        Globals.lights = false;
        narrator.PlaySound(3); // Play 4Info
      }
    }

    // "Landing" - Bring moon surface to ship
    if (landing) {
      time += Time.deltaTime;
      Debug.Log("Timer: " + time);
      float newy = (time / landingTime * Math.Abs(moonStart - moonEnd)) + moonStart;
      moon.transform.position = new Vector3(moon.transform.position.x, newy, moon.transform.position.z);
      
      if (time >= landingTime) {
        playerMove.enableFly = false;
        playerMove.useGravity = true;
        playerRigid.mass = 0.5f;
        rightThrottle.ThrottleOff();
        moon.transform.position = new Vector3(moon.transform.position.x, moonEnd, moon.transform.position.z);
        time = 0f;
        landing = false;
        Globals.phase = 3;
        narrator.PlaySound(5); // Play 6Landed
      }
    }
  }

  // Interact with red throttle
  // Wait 9s before take off
  public void PreTakeOff() {
    if (Globals.phase == 0 || narrator.soundTimer <= 0) {
      leftThrottle.ThrottleOn();
      narrator.PlaySound(1); // Play 2Takeoff
      waitTime = 9;
      Globals.phase = 1;
    } else {
      error.Play();
    }
  }

  // Take off
  public void TakeOff() {
    blendStart = true;
    rocket.Play();
  }

  // Start landing sequence
  public void Land() {
    if (Globals.phase == 2 || narrator.soundTimer <= 0) {
      moon.SetActive(true);
      rightThrottle.ThrottleOn();
      landing = true;
      narrator.PlaySound(4); // Play 5Landing
      rocket.Play();
    } else {
      error.Play();
    }
  }
}
