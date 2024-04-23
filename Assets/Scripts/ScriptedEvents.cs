using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvents : MonoBehaviour
{
    [Header("Skybox Transition")]
    public Material skybox;
    public ParticleSystem zoom;
    public bool blendStart = false;
    public float blendTime = 5f;
    public float blend = 0f;
    private float zoomStart = 0.2f;
    private float zoomEnd = 1.5f;
    private float time = 0f;

    [Header("Open Doors")]
    public Animator doors;
    public bool doorStart = false;

    void Start()
    {
        skybox.SetFloat("_Blend", 0);
        // zoom.Stop();
    }

    void Update()
    {
        if (blendStart) {
            blend += Time.deltaTime / blendTime;
            skybox.SetFloat("_Blend", blend);

            if (zoom.isPlaying && blend >= zoomEnd) {
                Debug.Log("Stop zoom");
                zoom.Stop();
            }
            else if (!zoom.isPlaying && blend >= zoomStart) {
                Debug.Log("Play zoom");
                zoom.Play();
            }

            if (blend >= 2) {
                blendStart = false;
                blend = 0f;
                time = 0f;
            }
        }

        if (doorStart) {
            openDoors();
        }
    }

    public void takeOff() {
        blendStart = true;
    }

    public void openDoors() {
        if (doors != null) {
            doors.Play("OpenDoors");
        } else {
            Debug.LogError("Doors not assigned");
        }
    }
}
