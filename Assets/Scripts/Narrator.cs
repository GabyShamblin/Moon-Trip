using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
  private AudioSource audioSource;
  public List<AudioClip> audios;
  public bool playAudio = false;
  public float soundTimer = 0;
  private int currAudio = 0;
  private int phaseCheck = 0;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    PlaySound();
    phaseCheck = 0;
  }

  void Update()
  {
    if (soundTimer > 0) {
      soundTimer -= Time.deltaTime;
    }
    else if (soundTimer < 0) {
      soundTimer = 0;
    }

    // if (phaseCheck < Globals.phase || playAudio) {
    //   PlaySound();
    // }
  }

  public void PlaySound() {
    if (soundTimer <= 0 && currAudio < audios.Count) {
      audioSource.PlayOneShot(audios[currAudio]);
      soundTimer = audios[currAudio].length;
      Debug.Log("Play audio " + currAudio + ": " + audios[currAudio].name);
      currAudio++;
      phaseCheck++;
      playAudio = false;
    }
  }

  public void PlaySound(int track) {
    if (track < audios.Count) {
      audioSource.PlayOneShot(audios[track]);
      soundTimer = audios[track].length;
      Debug.Log("Play audio " + track + ": " + audios[track].name);
      phaseCheck++;
      playAudio = false;
    }
  }
}
