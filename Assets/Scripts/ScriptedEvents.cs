using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvents : MonoBehaviour
{
  private float time = 0f;

  [Header("Skybox Transition")]
  public Material skybox;
  public ParticleSystem zoom;
  public AudioSource rocket;
  public bool blendStart = false;
  public float blendTime = 5f;
  public float blend = 0f;
  private float zoomStart = 0.2f;
  private float zoomEnd = 1.5f;

  [Header("Open Doors")]
  public Animator doors;
  public bool doorStart = false;

  [Header("Landing")]
  public Transform moon;
  public bool landing = false;
  public float landingTime = 5f;
  private float moonStart = -15f;
  private float moonEnd = -3.54f;

  [Header("Narrator")]
  public Narrator narr;

  void Start()
  {
    skybox.SetFloat("_Blend", 0);
    moon.position = new Vector3(moon.position.x, moonStart, moon.position.z);
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
        Debug.Log("Stop zoom");
        zoom.Stop();
        Globals.gravityOff = true;
        Globals.earthSpin = true;
      }
      // If zoom is not playing while blending, start
      else if (!zoom.isPlaying && blend >= zoomStart) {
        Debug.Log("Play zoom");
        zoom.Play();
      }

      // Stop blend when it reaches its max
      if (blend >= 2) {
        blendStart = false;
        blend = 0f;
      }
    }

    // Open ship doors
    if (doorStart) {
      OpenDoors();
    }

    // "Landing" - Bring moon surface to ship
    if (landing) {
      time += Time.deltaTime;
      float newy = (time / landingTime * Math.Abs(moonStart - moonEnd)) + moonStart;
      moon.position = new Vector3(moon.position.x, newy, moon.position.z);
      
      if (time >= landingTime) {
        moon.position = new Vector3(moon.position.x, moonEnd, moon.position.z);
        time = 0f;
        landing = false;
      }
    }
  }

  // Start take off
  public void TakeOff() {
    blendStart = true;
  }

  // Start open door animation
  public void OpenDoors() {
    if (doors != null) {
      doors.Play("OpenDoors");
    } else {
      Debug.LogError("Doors not assigned");
    }
    doorStart = false;
  }
}
