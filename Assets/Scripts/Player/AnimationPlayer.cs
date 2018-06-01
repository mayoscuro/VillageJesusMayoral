using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour {
    
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x,centerPoint.eulerAngles.y, transform.eulerAngles.z);
	}
}
