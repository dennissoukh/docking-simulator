using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuStartButton : MonoBehaviour
{
  private Camera main;
  private Button Button;
  private CanvasGroup group;
  private bool movement = false;

  // Start is called before the first frame update
  void Start()
  {
    main = Camera.main;

    // Get MainMenu CanvasGroup
    group = GameObject.Find("SceneLoader").GetComponentInChildren<CanvasGroup>();

    // Setup button
    Button = gameObject.GetComponent<Button>();
    Button.onClick.AddListener(HandleStartClick);
  }

  void HandleStartClick()
  {
    if (!movement) {
      movement = true;
      StartCoroutine(MoveFromTo(main.transform, main.transform.position, new Vector3(0, 0, 500), 240f));
    }
  }

  IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
  {
    float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
    float t = 0;

    StartCoroutine(FadeCanvasGroup(1f, 0f, 2f));

    while (t <= 1.0f)
    {
      // Goes from 0 to 1, incrementing by step each time
      t += step;

      // Move objectToMove closer to b
      objectToMove.position = Vector3.Lerp(a, b, t);

      // Leave the routine and return here in the next frame
      yield return new WaitForFixedUpdate();
    }

    objectToMove.position = b;
    movement = false;
  }

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

    // Transition between main menu and game scene
    SceneManager.LoadScene(0);
  }
}
