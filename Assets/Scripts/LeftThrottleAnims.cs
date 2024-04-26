using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftThrottleAnims : MonoBehaviour
{
  private Animator animator;

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void ThrottleOn() {
    animator.Play("LeftThrottleOn");
  }

  public void ThrottleOff() {
    animator.Play("LeftThrottleOff");
  }
}
