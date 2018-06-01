using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAtacarLobos : MonoBehaviour {

    public bool collidingPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collidingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collidingPlayer = false;
        }
    }
}
