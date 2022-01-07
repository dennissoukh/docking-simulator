using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingPortTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Has triggered");

        if (other.gameObject.name == "Spacecraft")
        {
            Debug.Log("Spacecraft has triggered");
        }
    }
}
