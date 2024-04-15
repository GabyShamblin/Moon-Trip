using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvents : MonoBehaviour
{
    public Material skybox;
    public bool blendStart = false;
    public float blendTime = 5f;
    public float blend = 0f;
    private float time = 0f;

    void Start()
    {
        skybox.SetFloat("_Blend", 0);
    }

    void Update()
    {
        if (blendStart) {
            blend += Time.deltaTime / blendTime;
            skybox.SetFloat("_Blend", blend);

            if (blend >= 2) {
                blendStart = false;
                blend = 0f;
            }
        }

    }
}
