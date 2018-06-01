using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPhysics : MonoBehaviour {
    Rigidbody rb;
    private bool unSoloGiro = false;
    [HideInInspector] public Vector3 rotacionTiro = Vector3.zero;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        transform.LookAt(transform.position - rb.velocity);
        transform.Rotate(-90, 0, 0);
    }


}
