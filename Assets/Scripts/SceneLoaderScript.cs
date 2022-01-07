using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneLoaderScript : MonoBehaviour
{
  private Canvas menu;
  private CanvasGroup group;
  private float duration = 3f;

  // Awake is called when the script instance is being loaded
  void Awake()
  {
    menu = GetComponentInChildren<Canvas>();
    group = GetComponentInChildren<CanvasGroup>();
  }

  // Start is called before the first frame update
  void Start()
  {
    // Fade in main menu
    StartCoroutine(FadeCanvasGroup(0f, 1f, duration));

    // Set game & application versions
    var version = menu.transform.Find("Version").gameObject.GetComponent<TextMeshProUGUI>();

    var appPlatform = Application.platform;
    var appVersion = Application.version;
    var unityVersion = Application.unityVersion;

    version.text = "\nPlatform: " + appPlatform + "\nVersion: " + appVersion +
    " (Unity " + unityVersion + ")";
  }

  // Fade in a CanvasGroup object
  private IEnumerator FadeCanvasGroup(float from, float to, float duration)
  {
    float elaspedTime = 0f;
    while (elaspedTime <= duration)
    {
      elaspedTime += Time.deltaTime;
      group.alpha = Mathf.Lerp(from, to, elaspedTime / duration);
      yield return null;
    }
    group.alpha = to;
  }
}
