using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersAnimales : MonoBehaviour {
    public bool collidingPlayerOrWolves;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Flecha" && other.transform.GetChild(0).gameObject.GetComponent<ArrowTrigger>().recogible) {
            collidingPlayerOrWolves = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Flecha" && other.transform.GetChild(0).gameObject.GetComponent<ArrowTrigger>().recogible)
        {
            collidingPlayerOrWolves = false;
        }
    }
}
