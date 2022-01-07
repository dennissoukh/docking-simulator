using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacecraftScript : MonoBehaviour
{
  // Variables declared here
  public static GameObject NoseCam, DockingPortCam, ThirdPersonCam;
  private GameObject SpaceShuttle;
  private GameObject SpaceStation;
  public static Rigidbody SpacecraftRB;

  private float ManeuverSpeedMultiplier, ForceMultiplier; // For handling the physics of the control of the spacecraft

  public static float SpacecraftSpeed, SpacecraftPitch, SpacecraftYaw, SpacecraftRoll, Range; // these variables are used for the spacecraft gui or HUD
  public static Vector3 StartingPos; // storing the initial starting position
  public static float Area; // area of where to spawn
  private LayerMask mask; // mask for docking port

  void Start()
  {
    // Variables initialized
    NoseCam = GameObject.Find("NoseCamera");
    DockingPortCam = GameObject.Find("DockingPortCamera");
    ThirdPersonCam = GameObject.Find("ThirdPersonCamera");
    SpaceStation = GameObject.Find("DockingPortTrigger");
    SpaceShuttle = GameObject.Find("space_shuttle");
    mask = LayerMask.GetMask("Docking Port");
    SpacecraftRB = GetComponent<Rigidbody>();
    SpacecraftYaw = SpacecraftPitch = SpacecraftSpeed = 0;
    ManeuverSpeedMultiplier = 45000f;
    ForceMultiplier = 0.1f;
    Area = 5000f;

    StartingPos = transform.position; // Starting position of the spacecraft stored
    transform.position = new Vector3(Random.Range(transform.position.x - Area, transform.position.x + Area), Random.Range(transform.position.y - Area, transform.position.y + Area), transform.position.z); // spacecraft is spawned in a random position within an area considering the starting position
    SetCamNose(); // player views through space shuttles nose camera
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManagerScript.GameState == 0) // if the game state is currently in playing mode
    {
      CheckControls();
      UpdateUIElements();
      if (Range <= 500f) checkSuccessfulDock(); // if the range is less than 500 check if its a successful dock
    }
  }

  // Checks if its a successful dock
  void checkSuccessfulDock()
  {
    if (Physics.Raycast(DockingPortCam.transform.position, DockingPortCam.transform.forward, 100f, mask) && DockingPortCam.transform.up.y > 0.6) // Casts a raycast checking if it hit the space stations docking port and also if the y rotation of the docking port camera is greater than 0.6
    {
      GameManagerScript.SuccessfulDock();
    }
  }

  //Updates the UI elements
  void UpdateUIElements()
  {
    SpacecraftSpeed = SpacecraftRB.velocity.magnitude; // setting the speed of the spacecraft
    Quaternion quat = transform.rotation; // Rotation of the spacecraft
    SpacecraftPitch = -(Mathf.Rad2Deg * Mathf.Atan2(2 * quat.x * quat.w - 2 * quat.y * quat.z, 1 - 2 * quat.x * quat.x - 2 * quat.z * quat.z)); // Calculating the spacecrafts pitch
    SpacecraftYaw = -(Mathf.Rad2Deg * Mathf.Atan2(2 * quat.y * quat.w - 2 * quat.x * quat.z, 1 - 2 * quat.y * quat.y - 2 * quat.z * quat.z)); // Calculating the spacecrafts yaw
    SpacecraftRoll = Mathf.Rad2Deg * Mathf.Asin(2 * quat.x * quat.y + 2 * quat.z * quat.w); // Calculating the spacecrafts roll
    Range = (transform.position - SpaceStation.transform.position).magnitude; // Calculating the spacecrafts range from the space station
  }


  void CheckControls()
  {
    //All of these trigger a function when a corresponding button is pressed
    if (Input.GetKey(KeyCode.W)) ForwardThrust();
    if (Input.GetKey(KeyCode.S)) BackwardThrust();
    if (Input.GetKey(KeyCode.Q)) UpwardsThrust();
    if (Input.GetKey(KeyCode.E)) DownwardsThrust();
    if (Input.GetKey(KeyCode.UpArrow)) IncreasePitch();
    if (Input.GetKey(KeyCode.DownArrow)) DecreasePitch();
    if (Input.GetKey(KeyCode.A)) RotateRight();
    if (Input.GetKey(KeyCode.D)) RotateLeft();
    if (Input.GetKey(KeyCode.Z)) Leftwardthrust();
    if (Input.GetKey(KeyCode.C)) RightwardsThrust();
    if (Input.GetKey(KeyCode.LeftArrow)) RollLeft();
    if (Input.GetKey(KeyCode.RightArrow)) RollRight();
    if (Input.GetKey(KeyCode.Alpha1)) SetCamNose();
    if (Input.GetKey(KeyCode.Alpha2)) SetCamDockingPort();
    if (Input.GetKey(KeyCode.Alpha3)) SetCamThirdPerson();
  }

  //Maneuvering spacecraft
  void ForwardThrust()
  {
    SpacecraftRB.velocity = this.transform.forward * ForceMultiplier + SpacecraftRB.velocity;
  }

  void BackwardThrust()
  {
    SpacecraftRB.velocity = -this.transform.forward * ForceMultiplier + SpacecraftRB.velocity;
  }

  void UpwardsThrust()
  {
    SpacecraftRB.velocity = this.transform.up * ForceMultiplier + SpacecraftRB.velocity;
  }

  void DownwardsThrust()
  {
    SpacecraftRB.velocity = -this.transform.up * ForceMultiplier + SpacecraftRB.velocity;
  }

  void RightwardsThrust()
  {
    SpacecraftRB.velocity = this.transform.right * ForceMultiplier + SpacecraftRB.velocity;
  }

  void Leftwardthrust()
  {
    SpacecraftRB.velocity = -this.transform.right * ForceMultiplier + SpacecraftRB.velocity;
  }

  void RotateRight()
  {
    SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * -this.transform.up);
  }

  void RotateLeft()
  {
    SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * this.transform.up);
  }

  void RollLeft()
  {
    SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * this.transform.forward);
  }

  void RollRight()
  {
    SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * -this.transform.forward);
  }

  void IncreasePitch()
  {
    SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * -this.transform.right);
  }

  void DecreasePitch()
  {
    SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * this.transform.right);
  }

  //Cameras

  //Sets the camera to the nose cam
  public static void SetCamNose()
  {
    DockingPortCam.SetActive(false);
    ThirdPersonCam.SetActive(false);
    GameManagerScript.EndGameCam.SetActive(false);
    NoseCam.SetActive(true);
  }

  //Sets the camera to the Docking port camera
  public static void SetCamDockingPort()
  {
    NoseCam.SetActive(false);
    ThirdPersonCam.SetActive(false);
    GameManagerScript.EndGameCam.SetActive(false);
    DockingPortCam.SetActive(true);
  }

  //Sets the camera to the third person camera
  public static void SetCamThirdPerson()
  {
    NoseCam.SetActive(false);
    DockingPortCam.SetActive(false);
    GameManagerScript.EndGameCam.SetActive(false);
    ThirdPersonCam.SetActive(true);
  }

}