using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
  public float smoothing = 3f;
  private Quaternion rotation;
  private static readonly float[] BoundsX = new float[]{-5f, 5f};
  private static readonly float[] BoundsY = new float[]{-5f, 5f};

  void Start()
  {
    rotation = transform.localRotation;
  }

  void Update()
  {
    rotation.x += Input.GetAxis("Mouse Y") * smoothing * (-1);
    rotation.y += Input.GetAxis("Mouse X") * smoothing;

    // Check if angle within bounds
    rotation.x = Mathf.Clamp(rotation.x, BoundsX[0], BoundsX[1]);
    rotation.y = Mathf.Clamp(rotation.y, BoundsY[0], BoundsY[1]);

    // Slowly rotate towards angle
    Quaternion targetRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 1);
  }
}
