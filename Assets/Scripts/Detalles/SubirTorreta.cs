using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubirTorreta : MonoBehaviour {
    public Transform destino;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") {
            other.transform.position = destino.position;
        }
    }
}
