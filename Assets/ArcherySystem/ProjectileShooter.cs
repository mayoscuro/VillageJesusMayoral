using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour {
    public GameObject arrow;
    private Animator arcoAnim;
    public static bool apuntando;
    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter playerController;

    // Use this for initialization
    void Start () {
        arcoAnim = transform.GetComponentInParent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) && playerController.hayFlechas && !playerController.enMenu)
        {
            GameObject projectile = Instantiate(arrow) as GameObject;
            projectile.transform.position = transform.position /*+ Camera.main.transform.forward * 2*/;
            projectile.transform.rotation = transform.rotation;
            //projectile.GetComponent<Arrow>().rotacionTiro = Camera.main.transform.rotation;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 60;
            arcoAnim.SetBool("Apuntar", false);
            apuntando = false;
        }

        if (Input.GetMouseButtonDown(0) && playerController.hayFlechas && !playerController.enMenu) {
            arcoAnim.SetBool("Apuntar",true);
        }
	}
}
