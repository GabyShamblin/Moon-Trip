using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    public bool toggle = false;
    public GameObject menu;
    bool active = false;

    void FixedUpdate()
    {
        if (active != toggle) {
            toggleMenu();
        }
    }

    void toggleMenu()
    {
        active = toggle;
        menu.SetActive(active);
    }
}
