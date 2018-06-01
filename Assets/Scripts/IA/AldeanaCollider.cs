using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldeanaCollider : MonoBehaviour {
    public bool aldeanaEnCasa;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Aldeana")
        {
            aldeanaEnCasa = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Aldeana")
        {
            aldeanaEnCasa = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag == "Aldeana")
        {
            aldeanaEnCasa = false;
        }
    }
}
