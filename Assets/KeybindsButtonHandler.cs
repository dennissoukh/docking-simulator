using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeybindsButtonHandler : MonoBehaviour
{
  private GameObject KeybindsMenu;
  private Button Button;
  // Start is called before the first frame update
  void Start()
  {
    Button = gameObject.GetComponent<Button>();
    KeybindsMenu = GameObject.Find("KeybindsMenu");
    Button.onClick.AddListener(HandleKeybindsMenuClick);
  }

  void HandleKeybindsMenuClick()
  {
    if (KeybindsMenu != null) KeybindsMenu.GetComponentInChildren<Canvas>().enabled = true;

    var menu = GameObject.Find("SceneLoader").GetComponentInChildren<Canvas>();
    menu.enabled = false;
  }
}
