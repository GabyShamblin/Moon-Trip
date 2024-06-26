using UnityEngine;

public class EarthSpin : MonoBehaviour
{
  [Tooltip("Earth rotates in the negative direction.")]
  [SerializeField] private float spinSpeed = -1f;

  void Update() {
    if (Globals.earthSpin) {
      transform.Rotate(0f, spinSpeed, 0f, Space.Self);
    }
  }
}
