using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingPortTriggerScript : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    Debug.Log("Has triggered");

    if (other.gameObject.name == "Spacecraft")
    {
      Debug.Log("Spacecraft has triggered");
    }
  }
}
