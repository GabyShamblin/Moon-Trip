using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightThrottleAnims : MonoBehaviour
{
  private Animator animator;

  void Start()
  {
    animator = GetComponent<Animator>();
  }

  public void ThrottleOn() {
    animator.Play("RightThrottleOn");
  }

  public void ThrottleOff() {
    animator.Play("RightThrottleOff");
  }
}
