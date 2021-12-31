using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettingsButton : MonoBehaviour
{
  private GameObject SettingsMenu;
  private Button Button;

  // Start is called before the first frame update
  void Start()
  {
    Button = gameObject.GetComponent<Button>();
    SettingsMenu = GameObject.Find("SettingsMenu");

    // Add button listener
    Button.onClick.AddListener(HandleSettingsClick);
  }

  void HandleSettingsClick()
  {
    if (SettingsMenu != null)
      SettingsMenu.GetComponentInChildren<Canvas>().enabled = true;

    // Hide the main menu when the settings menu is displayed
    var menu = GameObject.Find("SceneLoader").GetComponentInChildren<Canvas>();
    menu.enabled = false;
  }
}
