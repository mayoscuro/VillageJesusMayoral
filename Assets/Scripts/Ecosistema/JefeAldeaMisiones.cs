using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JefeAldeaMisiones : MonoBehaviour {
    public List<Text> objetonecesario;
    public List<Text> cantidadNecesaria;

    public Ecosystem ecosistema;
    public ItemDatabase itemDatabase;
    public GameObject triggerDialogos;

    public JefeDeLaAldeaEntregarObjetos entrega;

    public GameObject panelMisiones;

    public Flowchart flowChart;

    private int datosZorro;
    private int datosLobo;
    private int datosCiervo;
    private int datosInsectos;
    private int datosFrutas;
    private int datosHierbas;

    private int cantidad;

    private GameObject lobo;
    private GameObject zorro;
    private GameObject ciervo;
    private GameObject insecto;
    private GameObject frutas;
    private GameObject hierbas;

    private int rank;

    private bool primero = true;
    // Use this for initialization

    private WeekController week;

    private void Start()
    {
        week = GetComponent<WeekController>();
    }

    public void recalcularMisiones() {
        calcularMisiones();
        for (int x = 0; x < objetonecesario.Count; x++)
        {
            entrega.textoObjetoEntregado = cantidadNecesaria;
            entrega.textoNombreObjetos = objetonecesario;
        }
        panelMisiones.gameObject.SetActive(false);
    }

    public void updateDatosMisiones() {
        cantidadNecesaria = entrega.textoObjetoEntregado;
        objetonecesario = entrega.textoNombreObjetos;

    }

    public void calcularMisiones() {
        if (ecosistema.obtenerDatosDelGrafo("Zorro")!=null) 
            datosZorro = ecosistema.obtenerDatosDelGrafo("Zorro").numeroVivosInicioSemana - ecosistema.obtenerDatosDelGrafo("Zorro").numVivosSemanaPasada;
        
        if(ecosistema.obtenerDatosDelGrafo("Lobo") != null)
            datosLobo = ecosistema.obtenerDatosDelGrafo("Lobo").numeroVivosInicioSemana - ecosistema.obtenerDatosDelGrafo("Lobo").numVivosSemanaPasada;
        if(ecosistema.obtenerDatosDelGrafo("Ciervo") != null)
            datosCiervo = ecosistema.obtenerDatosDelGrafo("Ciervo").numeroVivosInicioSemana - ecosistema.obtenerDatosDelGrafo("Ciervo").numVivosSemanaPasada;
        if(ecosistema.obtenerDatosDelGrafo("Insectos") != null)
            datosInsectos = ecosistema.obtenerDatosDelGrafo("Insectos").numeroVivosInicioSemana - ecosistema.obtenerDatosDelGrafo("Insectos").numVivosSemanaPasada;
        if(ecosistema.obtenerDatosDelGrafo("Frutas") !=null)
            datosFrutas = ecosistema.obtenerDatosDelGrafo("Frutas").numeroVivosInicioSemana - ecosistema.obtenerDatosDelGrafo("Frutas").numVivosSemanaPasada;
        if(ecosistema.obtenerDatosDelGrafo("Plantas") != null)
            datosHierbas = ecosistema.obtenerDatosDelGrafo("Plantas").numeroVivosInicioSemana - ecosistema.obtenerDatosDelGrafo("Plantas").numVivosSemanaPasada;

        int maximo = 3;
        int x = 0;
        int random = 0;

        if (ecosistema.obtenerDatosDelGrafo("Ciervo") != null && datosCiervo > 0 && maximo >= 0 )
        {
            maximo--;
            random = Random.Range(1, itemDatabase.itemsCiervo.Count);
            for (int y = 0; y < random; y++)
            {
                objetonecesario[x].text = itemDatabase.itemsCiervo[y].itemName;
                cantidadNecesaria[x].text = "0/" + Random.Range(1, datosCiervo);
                x++;
                Debug.Log("Ciervo");
            }

        }

        if (ecosistema.obtenerDatosDelGrafo("Insectos") != null && datosInsectos > 0 && maximo >= 0)
        {
            maximo--;
            random = Random.Range(1, itemDatabase.itemsInsectos.Count);
            for (int y = 0; y < random; y++)
            {
                cantidadNecesaria[x].text = "0/" + Random.Range(1, datosInsectos);
                objetonecesario[x].text = itemDatabase.itemsInsectos[y].itemName;
                x++;
                Debug.Log("Insectos");
            }
        }

        if (ecosistema.obtenerDatosDelGrafo("Frutas") != null && datosFrutas > 0 && maximo >= 0)
        {
            maximo--;
            random = Random.Range(1, itemDatabase.itemsFrutas.Count);
            for (int y = 0; y < random; y++)
            {
                cantidadNecesaria[x].text = "0/" + Random.Range(1, datosFrutas);
                objetonecesario[x].text = itemDatabase.itemsFrutas[y].itemName;

                x++;
                Debug.Log("Frutas");
            }
        }
        

        if (ecosistema.obtenerDatosDelGrafo("Plantas") != null && datosHierbas > 0 && maximo >= 0)
        {
            maximo--;
            random = Random.Range(1, itemDatabase.itemsHierbas.Count);
            for (int y = 0; y < random; y++)
            {
                objetonecesario[x].text = itemDatabase.itemsHierbas[y].itemName;
                cantidadNecesaria[x].text = "0/" + Random.Range(1, datosHierbas);
                x++;
                Debug.Log("Hierbas");
            }
        }

        if (ecosistema.obtenerDatosDelGrafo("Zorro") != null && datosZorro > 0 && maximo >= 0)
        {
            maximo--;
            random = Random.Range(1, itemDatabase.itemsZorro.Count);
            while (x < random)
            {
                objetonecesario[x].text = itemDatabase.itemsZorro[x].itemName;
                cantidadNecesaria[x].text = "0/" + Random.Range(1, datosZorro);
                x++;
                Debug.Log("Zorro");
                Debug.Log(maximo);
            }
        }

        if (ecosistema.obtenerDatosDelGrafo("Lobo") != null && datosLobo > 0 && maximo >= 0)
        {
            maximo--;
            random = Random.Range(1, itemDatabase.itemsLobo.Count);
            for (int y = 0; y < random; y++)
            {
                objetonecesario[x].text = itemDatabase.itemsLobo[y].itemName;
                cantidadNecesaria[x].text = "0/" + Random.Range(1, datosLobo);
                x++;
                Debug.Log("Lobo");
            }
        }

        while (x < objetonecesario.Count)
        {
            objetonecesario[x].text = "";
            cantidadNecesaria[x].text = "";
            x++;
        }

    }

    public void buttonExit()
    {
        triggerDialogos.SetActive(true);
    }
}
