using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFly : MonoBehaviour
{
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rigidbody != null && rigidbody.useGravity && Globals.gravityOff) {
            rigidbody.useGravity = false;
            rigidbody.velocity = new Vector3(0, Globals.flyVelocity * Random.Range(0.5f, 1.0f), 0);
        }
    }
}
