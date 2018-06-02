using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JefeAldeaEcosistema : MonoBehaviour {
    [Header("Datos semana actual")]
    public Text zorroTextSemana;
    public Text loboTextSemana;
    public Text ciervoTextSemana;
    public Text insectoTextSemana;
    public Text frutaTextSemana;
    public Text hierbaTextSemana;


    [Header("Ecosistema")]
    public Ecosystem ecosistema;

    [Header("Datos semana pasada")]
    public Text zorroTextSemanaPasada;
    public Text loboTextSemanaPasada;
    public Text ciervoTextSemanaPasada;
    public Text insectoTextSemanaPasada;
    public Text frutaTextSemanaPasada;
    public Text hierbaTextSemanaPasada;

    [Header("Diferencia entre semanas")]
    public Text zorroTextDiferencia;
    public Text loboTextDiferencia;
    public Text ciervoTextDiferencia;
    public Text insectoTextDiferencia;
    public Text frutaTextDiferencia;
    public Text hierbaTextDiferencia;

    public GameObject triggerDialogos;

    // Use this for initialization
    void Awake() {

    }

    public void updateTextosSemanaActual (){
        zorroTextSemana.text = "" + ecosistema.obtenerDatosDelGrafo("Zorro").numeroVivos;
        loboTextSemana.text = "" + ecosistema.obtenerDatosDelGrafo("Lobo").numeroVivos;
        ciervoTextSemana.text = "" + ecosistema.obtenerDatosDelGrafo("Ciervo").numeroVivos;
        insectoTextSemana.text = "" + ecosistema.obtenerDatosDelGrafo("Insectos").numeroVivos;
        frutaTextSemana.text = "" + ecosistema.obtenerDatosDelGrafo("Frutas").numeroVivos;
        hierbaTextSemana.text = "" + ecosistema.obtenerDatosDelGrafo("Plantas").numeroVivos;

        zorroTextDiferencia.text = "" + (ecosistema.obtenerDatosDelGrafo("Zorro").numeroVivos - ecosistema.obtenerDatosDelGrafo("Zorro").numVivosSemanaPasada);
        loboTextDiferencia.text = "" + (ecosistema.obtenerDatosDelGrafo("Lobo").numeroVivos - ecosistema.obtenerDatosDelGrafo("Lobo").numVivosSemanaPasada);
        ciervoTextDiferencia.text = "" + (ecosistema.obtenerDatosDelGrafo("Ciervo").numeroVivos - ecosistema.obtenerDatosDelGrafo("Ciervo").numVivosSemanaPasada);
        insectoTextDiferencia.text = "" + (ecosistema.obtenerDatosDelGrafo("Insectos").numeroVivos - ecosistema.obtenerDatosDelGrafo("Insectos").numVivosSemanaPasada);
        frutaTextDiferencia.text = "" + (ecosistema.obtenerDatosDelGrafo("Frutas").numeroVivos - ecosistema.obtenerDatosDelGrafo("Frutas").numVivosSemanaPasada);
        hierbaTextDiferencia.text = "" + (ecosistema.obtenerDatosDelGrafo("Plantas").numeroVivos - ecosistema.obtenerDatosDelGrafo("Plantas").numVivosSemanaPasada);

        zorroTextSemanaPasada.text = "" + ecosistema.obtenerDatosDelGrafo("Zorro").numVivosSemanaPasada;
        loboTextSemanaPasada.text = "" + ecosistema.obtenerDatosDelGrafo("Lobo").numVivosSemanaPasada;
        ciervoTextSemanaPasada.text = "" + ecosistema.obtenerDatosDelGrafo("Ciervo").numVivosSemanaPasada;
        insectoTextSemanaPasada.text = "" + ecosistema.obtenerDatosDelGrafo("Insectos").numVivosSemanaPasada;
        frutaTextSemanaPasada.text = "" + ecosistema.obtenerDatosDelGrafo("Frutas").numVivosSemanaPasada;
        hierbaTextSemanaPasada.text = "" + ecosistema.obtenerDatosDelGrafo("Plantas").numVivosSemanaPasada;
    }

    public bool estadoDelEcosistema() { //True si buena, false si malo.
        if (int.Parse(zorroTextDiferencia.text) > ((int.Parse(zorroTextSemanaPasada.text) * 70) / 100) || int.Parse(loboTextDiferencia.text) > ((int.Parse(loboTextSemanaPasada.text) * 70) / 100) ||
            int.Parse(ciervoTextDiferencia.text) > ((int.Parse(ciervoTextSemanaPasada.text) * 70) / 100)) {
            return false;
        }
        return true;
    }

    // Update is called once per frame
    public void exitPanelButton () {
        triggerDialogos.SetActive(true);
    }
}
