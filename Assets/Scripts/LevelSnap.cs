using System;
using UnityEngine;

public class LeverSnap : MonoBehaviour
{
  [SerializeField] private GameObject lever;
  private int[] tickLocations = {-45, -135};
  private float timer = 0f;

  void Update()
  {
    timer += Time.deltaTime;

    if (lever != null && timer >= 5) {
      float rotation = lever.transform.localRotation.eulerAngles.y;
      // Debug.Log("Rotation before: " + rotation);

      float minDistance = 180;
      int minIndex = 0;
      for (int i = 0; i < tickLocations.Length; i++) {
        if (Math.Abs(tickLocations[i] - rotation) < minDistance) {
          minIndex = i;
          minDistance = Math.Abs(tickLocations[i] - rotation);
        }
      }

      lever.transform.Rotate(minDistance, 0f, 0f, Space.Self);
      timer = 0f;
    }
  }
}
