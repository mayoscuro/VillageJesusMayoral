using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KodamaCollider : MonoBehaviour {
    public bool jugadorCerca;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag  == "Player") {
            jugadorCerca = true;
        }
    }
}
