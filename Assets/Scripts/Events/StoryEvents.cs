using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class StoryEvents : MonoBehaviour {
    [Header("Kodama Historia")]
    public GameObject Kodama;
    private Animator kodamaAnim;

    [Header("Inventario")]
    public InventorySystem inventario;

   // [Header("Evento Espada")]
   // public GameObject espada;//Si puedo no ponerlo mejor.

    [Header("Luces")]
    public GameObject luzVerdeAltarKodama;

    [Header("Dialogos")]
    public GameObject textosObj;
    public Flowchart flowText;

    [Header("Espada Grande")]
    public GameObject granEspadaObj;
    private bool intentoAparicion;

    [Header("libros")]
    public GameObject libroAldeana;
    public GameObject libroCocinero;


	// Use this for initialization
	void Start () {
        kodamaAnim = Kodama.GetComponent<Animator>();
        granEspadaObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (!DayNightController.day)
        {
            luzVerdeAltarKodama.SetActive(true);
            kodamaAnim.SetBool("Bailar", true);
            textosObj.SetActive(true);
        }
        else {
            luzVerdeAltarKodama.SetActive(false);
            kodamaAnim.SetBool("Bailar", false);
            textosObj.SetActive(false);
        }

        flowText.SetBooleanVariable("Sword",true);
        flowText.SetBooleanVariable("ItemLord",true);
        //flowText.SetIntegerVariable("rango", 5);
        if (!DayNightController.day && !intentoAparicion && Random.value > 0.8f) {
            intentoAparicion = true;
            granEspadaObj.SetActive(true);
        } else if (DayNightController.day) {
            intentoAparicion = false;
            granEspadaObj.SetActive(false);
        }
       // Debug.Log(flowText.GetIntegerVariable("rango"));
        if (flowText.GetIntegerVariable("rango") >= 3)
        {
            Debug.Log("DENTRO");
            libroAldeana.SetActive(true);
        }
        else {
            libroAldeana.SetActive(false);
        }
        if (flowText.GetIntegerVariable("rango") >= 4)
        {
            libroCocinero.SetActive(true);
        }
        else {
            libroCocinero.SetActive(false);
        }
	}


}
