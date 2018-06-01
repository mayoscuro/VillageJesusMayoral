using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private bool estaAbierta = false;
    private bool jugadorEnLaPuerta = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (!estaAbierta && gameObject.tag == "Enter")
        {
            estaAbierta = true;
            abrirPuerta(gameObject.tag);
        }
    }

    private void abrirPuerta(string tag) {
        
        //Debug.Log("Ahora,   " + estaAbierta);
        transform.parent.GetChild(0).GetChild(0).transform.Rotate(0,0,-90);
        cerrarPuerta();
    }

    private void cerrarPuerta() {
        

        StartCoroutine("tiempoEspera");
        

    }


    IEnumerator tiempoEspera() {
        yield return new WaitForSeconds(3f);
        if (!jugadorEnLaPuerta)
        {
            transform.parent.GetChild(0).GetChild(0).transform.localEulerAngles = new Vector3(0f, 0f, 0.0f);
            estaAbierta = false;
        }
        else {
            StartCoroutine("tiempoEspera");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        jugadorEnLaPuerta = false;
    }

    private void OnTriggerStay(Collider other)
    {
        jugadorEnLaPuerta = true;
    }
}
