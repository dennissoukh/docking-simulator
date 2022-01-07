using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
  public static int GameState;

  // Start is called before the first frame update
  void Start()
  {
    GameState = 0;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public static void DockUnsuccessful(string reason) {
    GameState = 2;
  }
}
