using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationScript : MonoBehaviour
{
  void OnCollisionEnter(Collision col)
  {
    if (GameManagerScript.GameState == 0 && col.gameObject.name == "Spacecraft") GameManagerScript.UnsuccessfulDock("Crashed into Space Station");
  }
}
