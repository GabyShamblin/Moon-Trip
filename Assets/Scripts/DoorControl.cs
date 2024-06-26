using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
  public Animator doors;
  public AudioSource doorSound;
  public AudioSource doorError;
  private bool doorOpen = false;

  public void ToggleDoors() {
    // If landed, allow doors to open
    if (Globals.phase >= 3) {
      if (doorOpen) {
        CloseDoors();
      } else {
        OpenDoors();
      }
    } else {
      doorError.Play();
    }
  }

  // Start open door animation
  public void OpenDoors() {
    if (doors != null) {
      doors.Play("OpenDoors");
      doorSound.pitch = 0;
      doorSound.Play();
      doorOpen = true;
    } else {
      Debug.LogError("Doors not assigned");
    }
  }

  // Start close door animation
  public void CloseDoors() {
    if (doors != null) {
      doors.Play("CloseDoors");
      doorSound.pitch = -0.2f;
      doorSound.Play();
      doorOpen = false;
    } else {
      Debug.LogError("Doors not assigned");
    }
  }
}
