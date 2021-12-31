using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDHandler : MonoBehaviour
{
  private GameObject spacecraft;
  private GameObject spacecraftGameObject;
  private GameObject activeDockingPort;
  private TextMeshProUGUI range;
  private TextMeshProUGUI roll;
  private TextMeshProUGUI rollRate;
  private TextMeshProUGUI pitch;
  private TextMeshProUGUI pitchRate;
  private TextMeshProUGUI yawRate;
  private TextMeshProUGUI yaw;
  private static Color blue;
  private static Color yellow;
  private static Color red;

  // Start is called before the first frame update
  void Start()
  {
    spacecraft = GameObject.Find("Spacecraft");
    spacecraftGameObject = spacecraft.gameObject;

    activeDockingPort = GameObject.Find("04 (PMA) Pressurized Mating Adapter 2");
    range = GameObject.Find("RangeValue1").GetComponent<TextMeshProUGUI>();

    roll = GameObject.Find("RollAngle").GetComponent<TextMeshProUGUI>();
    rollRate = GameObject.Find("RollRateValue").GetComponent<TextMeshProUGUI>();

    pitch = GameObject.Find("PitchAngle").GetComponent<TextMeshProUGUI>();
    pitchRate = GameObject.Find("PitchRateValue").GetComponent<TextMeshProUGUI>();

    yaw = GameObject.Find("YawAngle").GetComponent<TextMeshProUGUI>();
    yawRate = GameObject.Find("YawRateValue").GetComponent<TextMeshProUGUI>();

    blue = new Color32(36, 210, 253, 255);
    yellow = new Color32(255, 165, 0, 255);
    red = Color.red;
  }

  // Update is called once per frame
  void Update()
  {
    // HUD Element - Roll
    hudRoll();

    // HUD Element - Roll Rate
    hudRollRate();

    // HUD Element - Pitch
    hudPitch();

    // HUD Element - Pitch Rate
    hudPitchRate();

    // HUD Element - Yaw
    hudYaw();

    // HUD Element - Yaw Rate
    hudYawRate();

    // HUD Element - Range to target object
    hudRangeToTarget();
  }

  private void hudRoll() {
    var y = roll.GetComponent<TextMeshProUGUI>();
    y.text = floatToStringRepresentation(
      WrapAngle(spacecraftGameObject.transform.eulerAngles.z),
      "°"
    );
  }

  private void hudRollRate() {
    var dZ = spacecraftGameObject.transform.TransformDirection(spacecraft.GetComponent<Rigidbody>().angularVelocity).z * Mathf.Rad2Deg;

    if (dZ >= 0.7 || dZ <= -0.7)
    {
      rollRate.color = red;
    }
    else if (dZ >= 0.5 || dZ <= -0.5)
    {
      rollRate.color = yellow;
    }
    else if (dZ < 0.5 || dZ > 0.5)
    {
      rollRate.color = blue;
    }

    rollRate.text = floatToStringRepresentation(
      dZ,
      "°/s"
    );
  }

  private void hudPitch() {
    pitch.text = floatToStringRepresentation(
      WrapAngle(spacecraftGameObject.transform.eulerAngles.x),
      "°"
    );
  }

  private void hudPitchRate() {
    var dX = spacecraftGameObject.transform.TransformDirection(spacecraft.GetComponent<Rigidbody>().angularVelocity).x * Mathf.Rad2Deg;

    if (dX >= 0.7 || dX <= -0.7)
    {
      pitchRate.color = red;
    }
    else if (dX >= 0.5 || dX <= -0.5)
    {
      pitchRate.color = yellow;
    }
    else if (dX < 0.5 || dX > 0.5)
    {
      pitchRate.color = blue;
    }

    pitchRate.text = floatToStringRepresentation(
      dX,
      "°/s"
    );
  }

  private void hudYaw() {
    yaw.text = floatToStringRepresentation(
      WrapAngle(spacecraftGameObject.transform.eulerAngles.y),
      "°"
    );
  }

  private void hudYawRate() {
    var dY = spacecraftGameObject.transform.TransformDirection(spacecraft.GetComponent<Rigidbody>().angularVelocity).y * Mathf.Rad2Deg;

    if (dY >= 0.7 || dY <= -0.7)
    {
      yawRate.color = red;
    }
    else if (dY >= 0.5 || dY <= -0.5)
    {
      yawRate.color = yellow;
    }
    else if (dY < 0.5 || dY > 0.5)
    {
      yawRate.color = blue;
    }

    yawRate.text = floatToStringRepresentation(
      dY,
      "°/s"
    );
  }

  private void hudRangeToTarget() {
    var r = range.GetComponent<TextMeshProUGUI>();
    r.text = floatToStringRepresentation(
      Vector3.Distance(activeDockingPort.transform.position, spacecraft.transform.position) / 100,
      "m",
      "F2"
    );
  }

  private static float WrapAngle(float angle)
  {
    angle %= 360;
    if (angle > 180)
      return angle - 360;

    return angle;
  }

  string floatToStringRepresentation(float f, string units, string places = "F1")
  {
    return string.Format("{0}{1}", f.ToString(places), units);
  }
}
