using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestablecerPlatasYRecursos : MonoBehaviour {
    [HideInInspector]public bool restablecerRecursos = false;
    public List<GameObject> recursosDelMapa;

	// Update is called once per frame
	void Update () {
        if (restablecerRecursos) {
            restablecerRecursos = false;
            foreach (GameObject recurso in recursosDelMapa) {
                recurso.SetActive(true);
            }
        }
	}
}
