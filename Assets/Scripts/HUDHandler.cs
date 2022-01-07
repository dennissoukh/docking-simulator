using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour
{
  private GameObject spacecraft;
  private GameObject spacecraftGameObject;
  private Rigidbody spacecraftRigidbody;
  private GameObject activeDockingPort;
  private TextMeshProUGUI range;
  private TextMeshProUGUI roll;
  private TextMeshProUGUI rollRate;
  private TextMeshProUGUI pitch;
  private TextMeshProUGUI pitchRate;
  private TextMeshProUGUI yawRate;
  private TextMeshProUGUI yaw;
  private TextMeshProUGUI rangeRate;
  private TextMeshProUGUI distanceX;
  private TextMeshProUGUI distanceY;
  private TextMeshProUGUI distanceZ;
  private static Color blue;
  private static Color yellow;
  private static Color red;

  private const float RateBarMaxAngle = 40;
  private const float RollRateBarPositiveZeroAngle = 50;
  private const float RollRateBarNegativeZeroAngle = 0 - RateBarMaxAngle;
  private const float PitchRateBarPositiveZeroAngle = -50;
  private const float PitchRateBarNegativeZeroAngle = -130;
  private const float YawRateBarPositiveZeroAngle = 180;
  private const float YawRateBarNegativeZeroAngle = -180 + RateBarMaxAngle;

  private GameObject positiveRollRateBar;
  private GameObject negativeRollRateBar;
  private GameObject positivePitchRateBar;
  private GameObject negativePitchRateBar;
  // private GameObject positiveYawRateBar;
  // private GameObject negativeYawRateBar;

  // Start is called before the first frame update
  void Start()
  {
    spacecraft = GameObject.Find("Spacecraft");
    spacecraftGameObject = spacecraft.gameObject;
    spacecraftRigidbody = spacecraft.GetComponent<Rigidbody>();

    activeDockingPort = GameObject.Find("04 (PMA) Pressurized Mating Adapter 2");
    range = GameObject.Find("RangeValue1").GetComponent<TextMeshProUGUI>();

    roll = GameObject.Find("RollAngle").GetComponent<TextMeshProUGUI>();
    rollRate = GameObject.Find("RollRateValue").GetComponent<TextMeshProUGUI>();

    pitch = GameObject.Find("PitchAngle").GetComponent<TextMeshProUGUI>();
    pitchRate = GameObject.Find("PitchRateValue").GetComponent<TextMeshProUGUI>();

    yaw = GameObject.Find("YawAngle").GetComponent<TextMeshProUGUI>();
    yawRate = GameObject.Find("YawRateValue").GetComponent<TextMeshProUGUI>();

    rangeRate = GameObject.Find("RangeRateValue").GetComponent<TextMeshProUGUI>();

    distanceX = GameObject.Find("DistanceXValue").GetComponent<TextMeshProUGUI>();
    distanceY = GameObject.Find("DistanceYValue").GetComponent<TextMeshProUGUI>();
    distanceZ = GameObject.Find("DistanceZValue").GetComponent<TextMeshProUGUI>();

    blue = new Color32(36, 210, 253, 255);
    yellow = new Color32(255, 165, 0, 255);
    red = Color.red;

    positiveRollRateBar = GameObject.Find("PositiveRollRateBar");
    negativeRollRateBar = GameObject.Find("NegativeRollRateBar");
    positivePitchRateBar = GameObject.Find("PositivePitchRateBar");
    negativePitchRateBar = GameObject.Find("NegativePitchRateBar");
    // positiveYawRateBar = GameObject.Find("PositiveYawRateBar");
    // negativeYawRateBar = GameObject.Find("NegativeYawRateBar");
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

    // HUD Element - Range Rate
    hudRangeRate();

    // HUD Element - Distance X, Y, Z
    hudDistanceComponents();

    // HUD Element - Positive roll rate bar
    hudRollPositiveRateBar();

    // HUD Element - Negative roll rate bar
    hudRollNegativeRateBar();

    // HUD Element - Positive pitch rate bar
    hudPitchPositiveRateBar();

    // HUD Element - Negative pitch rate bar
    hudPitchNegativeRateBar();

    // HUD Element - Positive yaw rate bar
    // hudYawPositiveRateBar();

    // HUD Element - Negative yaw rate bar
    // hudYawNegativeRateBar();
  }

  private void hudRoll() {
    var y = roll.GetComponent<TextMeshProUGUI>();
    y.text = floatToStringRepresentation(
      WrapAngle(spacecraftGameObject.transform.eulerAngles.z),
      "°"
    );
  }

  private void hudRollRate() {
    var dZ = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).z * Mathf.Rad2Deg;

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
    var dX = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).x * Mathf.Rad2Deg;

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
    var dY = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).y * Mathf.Rad2Deg;

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
    range.text = floatToStringRepresentation(
      Vector3.Distance(activeDockingPort.transform.position, spacecraft.transform.position) / 100,
      "m",
      "F2"
    );
  }

  private void hudRangeRate() {
    Vector3 relativePortPosition = activeDockingPort.transform.position - spacecraft.transform.position;
    Vector3 relativePortVelocity = Vector3.Project(spacecraftRigidbody.velocity, relativePortPosition);
    float approachRate = (Vector3.Angle(relativePortPosition, spacecraftRigidbody.velocity) > 90 ? -1 : 1) * relativePortVelocity.magnitude;

    rangeRate.text = floatToStringRepresentation(
      approachRate / 100,
      "m/s",
      "F2"
    );
  }

  private void hudDistanceComponents() {
    Vector3 relativeDockingPortPosition = spacecraft.transform.InverseTransformPoint(activeDockingPort.transform.position);

    distanceX.text = floatToStringRepresentation(
      (relativeDockingPortPosition.x / 100) * -1,
      "m"
    );

    distanceY.text = floatToStringRepresentation(
      (relativeDockingPortPosition.y / 100) * -1,
      "m"
    );

    distanceZ.text = floatToStringRepresentation(
      (relativeDockingPortPosition.z / 100) * -1,
      "m"
    );
  }

  private void hudRollPositiveRateBar() {
    float dZ = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).z * Mathf.Rad2Deg;

    RectTransform rectTransform = positiveRollRateBar.GetComponent<RectTransform>();
    RawImage rawImage = positiveRollRateBar.GetComponent<RawImage>();

    if (dZ >= 0.7 || dZ <= -0.7)
    {
      rawImage.color = red;
    }
    else if (dZ >= 0.5 || dZ <= -0.5)
    {
      rawImage.color = yellow;
    }
    else if (dZ < 0.5 || dZ > 0.5)
    {
      rawImage.color = blue;
    }

    rectTransform.rotation = Quaternion.Euler(
      0,
      0,
      Mathf.Max(
        0,
        RollRateBarPositiveZeroAngle + dZ * RateBarMaxAngle
      )
    );
  }

  private void hudRollNegativeRateBar() {
    float dZ = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).z * Mathf.Rad2Deg;

    RectTransform rectTransform = negativeRollRateBar.GetComponent<RectTransform>();
    RawImage rawImage = negativeRollRateBar.GetComponent<RawImage>();

    if (dZ >= 0.7 || dZ <= -0.7)
    {
      rawImage.color = red;
    }
    else if (dZ >= 0.5 || dZ <= -0.5)
    {
      rawImage.color = yellow;
    }
    else if (dZ < 0.5 || dZ > 0.5)
    {
      rawImage.color = blue;
    }

    rectTransform.rotation = Quaternion.Euler(
      0,
      0,
      Mathf.Min(
        0,
        RollRateBarNegativeZeroAngle + dZ * RateBarMaxAngle
      )
    );
  }

  private void hudPitchPositiveRateBar() {
    float dX = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).x * Mathf.Rad2Deg;
    RectTransform rectTransform = positivePitchRateBar.GetComponent<RectTransform>();
    RawImage rawImage = positivePitchRateBar.GetComponent<RawImage>();

    if (dX >= 0.7 || dX <= -0.7)
    {
      rawImage.color = red;
    }
    else if (dX >= 0.5 || dX <= -0.5)
    {
      rawImage.color = yellow;
    }
    else if (dX < 0.5 || dX > 0.5)
    {
      rawImage.color = blue;
    }

    rectTransform.rotation = Quaternion.Euler(
      0,
      0,
      Mathf.Max(
        -90,
        PitchRateBarPositiveZeroAngle + dX * RateBarMaxAngle
      )
    );
  }

  private void hudPitchNegativeRateBar() {
    float dX = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).x * Mathf.Rad2Deg;
    RectTransform rectTransform = negativePitchRateBar.GetComponent<RectTransform>();
    RawImage rawImage = negativePitchRateBar.GetComponent<RawImage>();

    if (dX >= 0.7 || dX <= -0.7)
    {
      rawImage.color = red;
    }
    else if (dX >= 0.5 || dX <= -0.5)
    {
      rawImage.color = yellow;
    }
    else if (dX < 0.5 || dX > 0.5)
    {
      rawImage.color = blue;
    }

    rectTransform.rotation = Quaternion.Euler(
      0,
      0,
      Mathf.Min(
        -92,
        PitchRateBarNegativeZeroAngle + dX * RateBarMaxAngle
      )
    );
  }

  // private void hudYawPositiveRateBar() {
  //   float dY = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).y * Mathf.Rad2Deg;
  //   RectTransform rectTransform = positiveYawRateBar.GetComponent<RectTransform>();
  //   RawImage rawImage = positiveYawRateBar.GetComponent<RawImage>();

  //   if (dY >= 0.7 || dY <= -0.7)
  //   {
  //     rawImage.color = red;
  //   }
  //   else if (dY >= 0.5 || dY <= -0.5)
  //   {
  //     rawImage.color = yellow;
  //   }
  //   else if (dY < 0.5 || dY > 0.5)
  //   {
  //     rawImage.color = blue;
  //   }

  //   rectTransform.rotation = Quaternion.Euler(
  //     0,
  //     0,
  //     Mathf.Min(
  //       -180,
  //       YawRateBarPositiveZeroAngle + dY * RateBarMaxAngle
  //     )
  //   );
  // }

  // private void hudYawNegativeRateBar() {
  //   float dY = spacecraftGameObject.transform.TransformDirection(spacecraftRigidbody.angularVelocity).y * Mathf.Rad2Deg;
  //   RectTransform rectTransform = negativeYawRateBar.GetComponent<RectTransform>();
  //   RawImage rawImage = negativeYawRateBar.GetComponent<RawImage>();

  //   if (dY >= 0.7 || dY <= -0.7)
  //   {
  //     rawImage.color = red;
  //   }
  //   else if (dY >= 0.5 || dY <= -0.5)
  //   {
  //     rawImage.color = yellow;
  //   }
  //   else if (dY < 0.5 || dY > 0.5)
  //   {
  //     rawImage.color = blue;
  //   }

  //   rectTransform.rotation = Quaternion.Euler(
  //     0,
  //     0,
  //     Mathf.Max(
  //       -180,
  //       YawRateBarNegativeZeroAngle + dY * RateBarMaxAngle
  //     )
  //   );
  // }

  private static float WrapAngle(float angle)
  {
    return angle > 180 ? angle - 360 : angle;
  }

  string floatToStringRepresentation(float f, string units, string places = "F1")
  {
    return string.Format("{0} {1}", f.ToString(places), units);
  }
}
