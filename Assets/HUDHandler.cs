using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDHandler : MonoBehaviour
{
  private GameObject spacecraft;
  private GameObject port;
  private GameObject range;

  // Start is called before the first frame update
  void Start()
  {
    spacecraft = GameObject.Find("Spacecraft");
    port = GameObject.Find("04 (PMA) Pressurized Mating Adapter 2");
    range = GameObject.Find("RangeValue1");
  }

  // Update is called once per frame
  void Update()
  {
    var r = range.GetComponent<TextMeshProUGUI>();
    r.text = Vector3.Distance(port.transform.position, spacecraft.transform.position).ToString() + "m";
  }
}
