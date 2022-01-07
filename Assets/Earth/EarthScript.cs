using UnityEngine;
using System.Collections;

public class EarthScript : MonoBehaviour
{
	void Start()
	{
		GetComponent<Rigidbody>().AddTorque(-transform.up * 500000000f);
	}

	void OnCollisionEnter(Collision col)
	{
		if (GameManagerScript.GameState == 0 && col.gameObject.name == "Spacecraft")
		{
			GameManagerScript.UnsuccessfulDock("Got too close to earth");
		}
	}


}