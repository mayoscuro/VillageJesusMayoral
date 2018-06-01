using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour {
    public int damage = 50;
    public bool recogible;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lobo" || other.tag == "Ciervo" || other.tag == "Zorro")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            /*if (other.tag == "Lobo")
            {
                //GameObject.FindGameObjectWithTag("LoboObjetoPadre").GetComponent<Animal>().restarVida(damage);
            } else if (other.tag == "Ciervo") {
                GameObject.FindGameObjectWithTag("CiervoObjetoPadre").GetComponent<Animal>().restarVida(damage);
            }*/
            other.gameObject.GetComponentInParent<Animal>().restarVida(damage);

            //Para que las flechas se queden clavadas:
            gameObject.transform.parent.GetComponent<ArrowPhysics>().enabled = false;
            transform.parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.parent.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<ArrowTrigger>().enabled = false;
            transform.parent.parent = other.gameObject.transform;
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.parent.GetComponent<Rigidbody>().isKinematic = true;
        } else if (other.tag == "Flecha" || other.tag == "Player"  || other.isTrigger || other.tag == "Barrera") {
            //Si choca contra otra flecha que no haga nada 
        }
        else {
            gameObject.transform.parent.GetComponent<ArrowPhysics>().enabled = false;
            transform.parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<ArrowTrigger>().enabled = false;
            transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.parent.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent.GetComponent<Rigidbody>().useGravity = false;
            recogible = true;
        }
    }


}
