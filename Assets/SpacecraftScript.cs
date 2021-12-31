using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject NoseCam;
    private GameObject DockingPortCam;
    private GameObject ThirdPersonCam;
    Rigidbody SpacecraftRB;

    private GameObject SpaceStation;

    private float ManeuverSpeedMultiplier, ForceMultiplier;

    public float SpacecraftSpeed, SpacecraftPitch, SpacecraftYaw, Range;


    void Start()
    {
        NoseCam = GameObject.Find("NoseCamera");
        DockingPortCam = GameObject.Find("DockingPortCamera");
        ThirdPersonCam = GameObject.Find("ThirdPersonCamera");
        SpaceStation = GameObject.Find("ISS_stationary");
        SpacecraftRB = this.GetComponent<Rigidbody>();
        SpacecraftYaw = SpacecraftPitch = SpacecraftSpeed = 0;
        ManeuverSpeedMultiplier = 15000f;
        ForceMultiplier = 0.1f;
        SetCamNose();
    }

    // Update is called once per frame
    void Update()
    {
        CheckControls();
        UpdateUIElements();
    }

    void FixedUpdate()
    {

    }

    void UpdateUIElements()
    {
        SpacecraftSpeed = SpacecraftRB.velocity.magnitude;
        SpacecraftPitch = this.transform.rotation.x;
        SpacecraftYaw = this.transform.rotation.z;
        Range = (this.transform.position - SpaceStation.transform.position).magnitude;
    }

    void CheckControls()
    {
        if (Input.GetKey(KeyCode.W)) ForwardThrust();
        if (Input.GetKey(KeyCode.S)) BackwardThrust();
        if (Input.GetKey(KeyCode.Q)) UpwardsThrust();
        if (Input.GetKey(KeyCode.E)) DownwardsThrust();
        if (Input.GetKey(KeyCode.UpArrow)) IncreasePitch();
        if (Input.GetKey(KeyCode.DownArrow)) DecreasePitch();
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
        SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * this.transform.right);
    }

    void DecreasePitch()
    {
        SpacecraftRB.AddTorque(ManeuverSpeedMultiplier * ForceMultiplier * -this.transform.right);
    }

    //Cameras
    void SetCamNose()
    {
        DockingPortCam.SetActive(false);
        ThirdPersonCam.SetActive(false);
        NoseCam.SetActive(true);
    }

    void SetCamDockingPort()
    {
        NoseCam.SetActive(false);
        ThirdPersonCam.SetActive(false);
        DockingPortCam.SetActive(true);
    }

    void SetCamThirdPerson()
    {
        NoseCam.SetActive(false);
        DockingPortCam.SetActive(false);
        ThirdPersonCam.SetActive(true);
    }
}
