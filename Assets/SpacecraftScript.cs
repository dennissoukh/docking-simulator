using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacecraftScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject NoseCam, DockingPortCam,ThirdPersonCam;
    private GameObject SpaceShuttle;
    private GameObject SpaceStation;
    public static Rigidbody SpacecraftRB;

    private float ManeuverSpeedMultiplier, ForceMultiplier;

    public static float SpacecraftSpeed, SpacecraftPitch, SpacecraftYaw, SpacecraftRoll, Range;
    public static Vector3 StartingPos;
    public static float Area;

    void Start()
    {
        NoseCam = GameObject.Find("NoseCamera");
        DockingPortCam = GameObject.Find("DockingPortCamera");
        ThirdPersonCam = GameObject.Find("ThirdPersonCamera");
        SpaceStation = GameObject.Find("DockingPortTrigger");
        SpaceShuttle = GameObject.Find("space_shuttle");
        SpacecraftRB = GetComponent<Rigidbody>();
        SpacecraftYaw = SpacecraftPitch = SpacecraftSpeed = 0;
        ManeuverSpeedMultiplier = 1000000f;
        ForceMultiplier = 0.1f;
        Area = 5000f;
        StartingPos = transform.position;
        transform.position = new Vector3(Random.Range(transform.position.x - Area, transform.position.x + Area),Random.Range(transform.position.y - Area, transform.position.y + Area),transform.position.z);
        SetCamNose();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.GameState == 0)
        {
            CheckControls();
            UpdateUIElements();
            if (Range <= 500f) checkSuccessfulDock();
        }
    }

    void checkSuccessfulDock()
    {
        if(Physics.Raycast(DockingPortCam.transform.position,DockingPortCam.transform.forward,100f) && DockingPortCam.transform.up.y > 0.6)
        {
            GameManagerScript.SuccessfulDock();
        }
    }

    void UpdateUIElements()
    {
        SpacecraftSpeed = SpacecraftRB.velocity.magnitude;
        Quaternion quat = transform.rotation;
        SpacecraftPitch = -(Mathf.Rad2Deg * Mathf.Atan2(2 * quat.x * quat.w - 2 * quat.y * quat.z, 1 - 2 * quat.x * quat.x - 2 * quat.z * quat.z));
        SpacecraftYaw = Mathf.Rad2Deg * Mathf.Atan2(2 * quat.y * quat.w - 2 * quat.x * quat.z, 1 - 2 * quat.y * quat.y - 2 * quat.z * quat.z);
        SpacecraftRoll = Mathf.Rad2Deg * Mathf.Asin(2 * quat.x * quat.y + 2 * quat.z * quat.w);
        Range = (transform.position - SpaceStation.transform.position).magnitude;
    }


    void CheckControls()
    {
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
    public static void SetCamNose()
    {
        DockingPortCam.SetActive(false);
        ThirdPersonCam.SetActive(false);
        GameManagerScript.EndGameCam.SetActive(false);
        NoseCam.SetActive(true);
    }

    public static void SetCamDockingPort()
    {
        NoseCam.SetActive(false);
        ThirdPersonCam.SetActive(false);
        GameManagerScript.EndGameCam.SetActive(false);
        DockingPortCam.SetActive(true);
    }

    public static void SetCamThirdPerson()
    {
        NoseCam.SetActive(false);
        DockingPortCam.SetActive(false);
        GameManagerScript.EndGameCam.SetActive(false);
        ThirdPersonCam.SetActive(true);
    }

}
