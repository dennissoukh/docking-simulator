using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
  // Declaring variables
  public static int GameState;
  public static GameObject SuccessfulDockStatusText, FailedDockStatusText, TimeTakenText, BestTimeText, CrashCauseText, DockStatusCanvas; // UI elements
  public static GameObject PauseMenuCanvas; // UI element
  public static GameObject HUD;
  public static GameObject EndGameCam;
  public static GameObject SpaceShuttle, Spacecraft;
  public static float TimeStart, BestTime;
  private bool CanPause;
  private Vector3 SpacecraftVelocity, SpacecraftAngularVelocity;
  public static AudioSource dockingSuccessfulSound;

  // Start is called before the first frame update
  void Start()
  {
    // Initializing game objects
    SuccessfulDockStatusText = GameObject.Find("SuccessfulDockStatusText");
    FailedDockStatusText = GameObject.Find("FailedDockStatusText");
    TimeTakenText = GameObject.Find("TimeTakenText");
    DockStatusCanvas = GameObject.Find("DockStatusCanvas");
    CrashCauseText = GameObject.Find("CrashCauseText");
    BestTimeText = GameObject.Find("BestTimeText");
    HUD = GameObject.Find("Dragon");
    Spacecraft = GameObject.Find("Spacecraft");
    EndGameCam = GameObject.Find("EndGameCamera");
    SpaceShuttle = GameObject.Find("space_shuttle");
    PauseMenuCanvas = GameObject.Find("PauseMenuCanvas");
    dockingSuccessfulSound = GameObject.Find("DockingSuccessfulSound").GetComponent<AudioSource>();

    CanPause = true; //CanPause at start of game
    TimeStart = Time.time; // Start time recorded here
    BestTime = float.MaxValue; // Best time set to largest number

    //UI elements set to invisible
    SuccessfulDockStatusText.SetActive(false);
    FailedDockStatusText.SetActive(false);
    DockStatusCanvas.SetActive(false);
    PauseMenuCanvas.SetActive(false);

    GameState = 0; // Gamestate is in playing mode now
  }

  //Update called every frame
  void Update()
  {
    //If the game can be paused and pause button is hit and the gamestate is in playing or in pause then toggle pause
    if (CanPause && Input.GetKey(KeyCode.Escape) && (GameState == 0 || GameState == 3))
    {
      StartCoroutine(TogglePause());
    }
  }

  //For setting the camera to the end game camera
  public static void SetCamEndGameCam()
  {
    //Set all spacecraft cameras off
    SpacecraftScript.NoseCam.SetActive(false);
    SpacecraftScript.DockingPortCam.SetActive(false);
    SpacecraftScript.ThirdPersonCam.SetActive(false);
    //Enable the End game camera
    EndGameCam.SetActive(true);
  }

  public static void UnsuccessfulDock(string reason)
  {
    GameState = 2; //Game state set to failed dock status screen

    //Dock status canvas and FailedStatusText UI elements now visible
    DockStatusCanvas.SetActive(true);
    FailedDockStatusText.SetActive(true);

    //These UI elements are invisible
    SuccessfulDockStatusText.SetActive(false);
    TimeTakenText.SetActive(false);
    BestTimeText.SetActive(false);

    //Crash status text set to visible with the reason for the crash
    CrashCauseText.SetActive(true);
    CrashCauseText.GetComponent<TMPro.TextMeshProUGUI>().text = "- " + reason;
    HUD.SetActive(false);

    //Camera set to end game camera
    SetCamEndGameCam();
    EndGameCam.transform.position = SpacecraftScript.ThirdPersonCam.transform.position; //End game camera set to position of the third person camera
    EndGameCam.transform.LookAt(SpaceShuttle.transform.position); // End game camera looks at space shuttle
    EndGameCam.GetComponent<Rigidbody>().velocity = EndGameCam.transform.forward * -0.5f; // End game camera then goes back away from the space ship creating a zoom out effect
  }

  public static void SuccessfulDock()
  {
    Debug.Log("SuccessfulDock");
    GameState = 1; //Game state set to Successful dock status screen

    // Play docking sound
    dockingSuccessfulSound.Play(1);

    //Dock status canvas and SuccessfulStatusText UI elements now visible
    DockStatusCanvas.SetActive(true);
    SuccessfulDockStatusText.SetActive(true);

    //These UI elements are invisible
    FailedDockStatusText.SetActive(false);
    CrashCauseText.SetActive(false);
    HUD.SetActive(false);

    //Spacecraft frozen in place
    SpacecraftScript.SpacecraftRB.constraints = RigidbodyConstraints.FreezeAll;

    //Camera set to end game camera
    SetCamEndGameCam();
    EndGameCam.transform.position = SpacecraftScript.ThirdPersonCam.transform.position; // End game camera set to position of the third person camera
    EndGameCam.transform.LookAt(SpaceShuttle.transform.position); // End game camera looks at space shuttle
    EndGameCam.GetComponent<Rigidbody>().velocity = EndGameCam.transform.forward * -0.5f; // End game camera then goes back away from the space ship creating a zoom out effect

    //Calculating the current time taken and setting the UI element for it while also making it visible
    float TimeTaken = Time.time - TimeStart;
    TimeTakenText.GetComponent<TMPro.TextMeshProUGUI>().text = "Time taken: " + TimeTaken + "s";
    TimeTakenText.SetActive(true);

    //Calculating if the timetaken is less than the best time then thats the new best timeand setting the UI element for it while also making it visible
    if (BestTime >= TimeTaken) BestTime = TimeTaken;
    BestTimeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Best Time: " + BestTime + "s";
    BestTimeText.SetActive(true);
  }

  //For replaying the game
  public void Replay()
  {
    SpacecraftScript.SpacecraftRB.constraints = RigidbodyConstraints.None; // Spacecraft is set to not frozen if it was already frozen
    Spacecraft.GetComponent<Rigidbody>().velocity = Vector3.zero; // Velocity of it set to 0
    Spacecraft.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Angular velocity of it set to 0
    Spacecraft.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotation of the spacecraft set to default
    Vector3 pos = SpacecraftScript.StartingPos; // Starting position of the spacecraft held
    Spacecraft.transform.position = new Vector3(Random.Range(pos.x - SpacecraftScript.Area, pos.x + SpacecraftScript.Area), Random.Range(pos.y - SpacecraftScript.Area, pos.y + SpacecraftScript.Area), pos.z); // spacecraft is spawned in a random position within an area considering the starting position
    DockStatusCanvas.SetActive(false); // This UI element becomes invisible
    HUD.SetActive(true); //HUD is now visible
    EndGameCam.GetComponent<Rigidbody>().velocity = Vector3.zero; // End game cameras velocity set to 0
    SpacecraftScript.SetCamNose(); // Player sees through nose camera
    if (GameState == 3) PauseMenuCanvas.SetActive(false); // If the game state is paused then the pause menu is hidden
    GameState = 0; // game is now playing
  }

  // Continue game
  public void Continue() {
    StartCoroutine(TogglePause());
  }

  public void Quit() {
    Application.Quit();
  }

  //For if you want to go to the main menu
  void MainMenu()
  {
    SceneManager.LoadScene("StartMenu"); // Start menu is loaded
  }

  //Toggling the pause
  IEnumerator TogglePause()
  {
    CanPause = false; // Setting a lock for when it can pause
    if (GameState == 3) ResumeGame(); // if the game state is already paused then resume it
    else PauseGame(); // else pause it
    yield return new WaitForSeconds(0.2f); // wait for like 0.2 secs
    CanPause = true; // Lock released
  }

  //For if you want to pause the game
  void PauseGame()
  {
    GameState = 3; // Setting the game state to paused
    PauseMenuCanvas.SetActive(true); // Making the pause menu canvas visible
    // Current Velocity and angular velocity are both stored for later resuming before setting them to zero
    SpacecraftVelocity = Spacecraft.GetComponent<Rigidbody>().velocity;
    SpacecraftAngularVelocity = Spacecraft.GetComponent<Rigidbody>().angularVelocity;
    Spacecraft.GetComponent<Rigidbody>().velocity = Vector3.zero;
    Spacecraft.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
  }

  // For if you want to resume the game
  void ResumeGame()
  {
    GameState = 0; // Game state is set to playing
    PauseMenuCanvas.SetActive(false); // pause menu now invisible
    //Constraints removed and velocity + angular velocty restored
    Spacecraft.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    Spacecraft.GetComponent<Rigidbody>().angularVelocity = SpacecraftAngularVelocity;
    Spacecraft.GetComponent<Rigidbody>().velocity = SpacecraftVelocity;
  }
}
