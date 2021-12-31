using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuQuitButton : MonoBehaviour
{
  private Button button;

  // Awake is called when the script instance is being loaded
  void Awake()
  {
    button = gameObject.GetComponent<Button>(); button.onClick.AddListener(HandleQuitClick);
  }

  void HandleQuitClick()
  {
    Application.Quit();
  }
}