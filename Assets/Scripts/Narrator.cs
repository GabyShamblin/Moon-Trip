using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
  private AudioSource audioSource;
  public List<AudioClip> audios;
  public bool playAudio = false;
  private int currAudio = 0;
  private float soundTimer = 0;
  private int phaseCheck = 0;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    PlaySound();
  }

  void Update()
  {
    if (soundTimer > 0) {
      soundTimer -= Time.deltaTime;
    }
    else if (soundTimer < 0) {
      soundTimer = 0;
    }

    if (phaseCheck < Globals.phase || playAudio) {
      PlaySound();
    }
  }

  public void PlaySound() {
    if (soundTimer <= 0 && currAudio < audios.Count) {
      audioSource.PlayOneShot(audios[currAudio]);
      soundTimer = audios[currAudio].length;
      currAudio++;
      playAudio = false;
      Debug.Log("Play audio " + currAudio + ": " + audios[currAudio].name);
    }
  }
}
