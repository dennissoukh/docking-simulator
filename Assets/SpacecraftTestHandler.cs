using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacecraftTestHandler : MonoBehaviour
{
  private Rigidbody _rigidbody;
  // public float AmbientSpeed = 5000f;
  // public float RotationSpeed = 10f;
  public float speed = 1000000f;

  void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    // Quaternion AddRot = Quaternion.identity;

    // float roll = 0;
    // float pitch = 0;
    // float yaw = 0;

    // // roll = Input.GetAxis("Horizontal") * (Time.fixedDeltaTime * RotationSpeed);
    // pitch = Input.GetAxis("Horizontal") * (Time.fixedDeltaTime * RotationSpeed);
    // yaw = Input.GetAxis("Vertical") * (Time.fixedDeltaTime * RotationSpeed);
    // AddRot.eulerAngles = new Vector3(-pitch, yaw, roll);
    // _rigidbody.rotation *= AddRot;

    // Vector3 AddPos = Vector3.forward;
    // AddPos = _rigidbody.rotation * AddPos;
    // _rigidbody.velocity = AddPos * (Time.fixedDeltaTime * AmbientSpeed);
  }

  private void Update()
  {
    // if (Input.GetKey(KeyCode.W))
    // {
    //   _rigidbody.velocity = transform.forward * speed;
    // }

    // if (Input.GetKey(KeyCode.S))
    // {
    //   _rigidbody.velocity = -transform.forward * speed;
    // }

    // if (Input.GetKey(KeyCode.D))
    // {
    //   // transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speed, Space.World);
    //   _rigidbody.velocity = transform.right * speed;
    // }

    // if (Input.GetKey(KeyCode.A))
    // {
    //   // transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * speed, Space.World);
    //   _rigidbody.velocity = -transform.right * speed;
    // }

    /*if (Input.GetKey(KeyCode.RightArrow))
    {
      _rigidbody.transform.position += Vector3.right * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      _rigidbody.transform.position += Vector3.left * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.UpArrow))
    {
      _rigidbody.transform.position += Vector3.forward * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
      _rigidbody.transform.position += Vector3.back * speed * Time.deltaTime;
    }*/
  }
}
