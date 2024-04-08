using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class DialSnap : MonoBehaviour
{
    [SerializeField] private GameObject dial;
    [SerializeField] private GameObject ticksParent;
    private GameObject[] ticks;
    private int[] tickLocations;
    private int numTicks = 0;
    [SerializeField] private int interval = 30;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        numTicks = ticksParent.transform.childCount;
        // Debug.Log("Num ticks: " + numTicks);

        tickLocations = new int[numTicks];
        float jump = (numTicks / 2.0f) - 0.5f;

        for (int i = 0; i < numTicks; i++) {
            tickLocations[i] = (int)(interval * jump);
            // Debug.Log("Angle: " + tickLocations[i]);
            // Debug.Log("Curr step: " + jump);
            jump -= 1.0f;
        }

        dial.transform.Rotate(0f, 50f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (dial != null && timer >= 5) {
            float rotation = dial.transform.localRotation.eulerAngles.y;
            // Debug.Log("Rotation before: " + rotation);

            float minDistance = 180;
            int minIndex = 0;
            for (int i = 0; i < numTicks; i++) {
                // Debug.Log(tickLocations[i] - rotation + " " + Math.Abs(tickLocations[i] - rotation) + " < " + minDistance);
                if (Math.Abs(tickLocations[i] - rotation) < minDistance) {
                    minIndex = i;
                    minDistance = Math.Abs(tickLocations[i] - rotation);
                    // Debug.Log("Switched to " + i + " tick");
                }
            }
            // Debug.Log("Closest tick: " + tickLocations[minIndex]);

            dial.transform.Rotate(0f, minDistance, 0f, Space.Self);
            timer = 0f;
        }
    }
}
