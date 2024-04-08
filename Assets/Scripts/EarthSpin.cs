using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpin : MonoBehaviour
{
    [Tooltip("Earth rotates in the negative direction.")]
    [SerializeField] private int spinSpeed = -10;

    void Update() {
        transform.Rotate(0f, spinSpeed, 0f, Space.Self);
    }
}
