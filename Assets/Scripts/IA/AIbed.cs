using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIbed : MonoBehaviour {
    public GameObject gameObjectDormido;
    public GameObject gameObjectDespierto;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.tag != "Player" && other.gameObject.name != "SpeackTrigger" && !DayNightController.day) {
            gameObjectDormido.transform.GetChild(0).GetComponent<Animator>().SetBool("Dormir",true);
            gameObjectDespierto.SetActive(false);
            gameObjectDormido.SetActive(true);
            gameObjectDormido.transform.GetChild(0).GetComponent<Animator>().SetBool("Dormir", true);
        }
    }

    private void Update()
    {
        if (DayNightController.day && !gameObjectDespierto.activeInHierarchy) {//Si es de dia y despierto aun no esta activo
            gameObjectDormido.SetActive(false);
            gameObjectDespierto.SetActive(true);
        }
    }
}
