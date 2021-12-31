using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuHandler : MonoBehaviour
{
  private Canvas menu;
  private CanvasGroup group;

  // Start is called before the first frame update
  void Awake()
  {
    menu = GetComponentInChildren<Canvas>();
    group = GetComponentInChildren<CanvasGroup>();

    // By default, keep the settings menu hidden
    menu.enabled = false;
  }
}
