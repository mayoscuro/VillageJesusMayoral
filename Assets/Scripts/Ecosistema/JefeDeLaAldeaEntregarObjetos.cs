using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JefeDeLaAldeaEntregarObjetos : MonoBehaviour {
    [Header("Dropdowns de entregar objetos")]
    public List<Dropdown> lista;
    public List<Text> nombresObjetosDeUsuario;
    [Header("Objetos de usuarios")]
    public InventorySystem inventory;
    private List<string> opciones = new List<string>();

    [Header("Trigger Dialogos")]
    public GameObject triggerDialogos;

    [Header("Puntos")]
    public int puntosConseguidos;
    public Text textoPuntos;
    public Text textoRango;

    [Header("Entregada para mision")]
    [HideInInspector]public List<Text> textoObjetoEntregado;
    [HideInInspector]public List<Text> textoNombreObjetos;

    public Flowchart flowChart;

    public int rank = 1;

    // Use this for initialization
    void Start() {
        mostrar();
    }

    private void Update()
    {
        if (puntosConseguidos >= 0 && puntosConseguidos <=1999){
            rank = 1;
            textoRango.text =""+ rank;
        } else if (puntosConseguidos >= 2000 && puntosConseguidos <= 4000){
            rank = 2;
            textoRango.text = "" + rank;
        } else if (puntosConseguidos >= 4001 && puntosConseguidos <= 6000) {
            rank = 3;
            textoRango.text = "" + rank;
        } else if (puntosConseguidos >= 6001 && puntosConseguidos <= 8000) {
            rank = 4;
            textoRango.text = "" + rank;
        } else if (puntosConseguidos >= 8001 && puntosConseguidos <= 10000) {
            rank = 5;
            textoRango.text = "" + rank;
        }
        flowChart.SetIntegerVariable("rango", rank);
    }

    public void mostrar() {
        for (int x = 0; x < inventory.inventory.Count; x++)
        {
            nombresObjetosDeUsuario[x].gameObject.SetActive(true);
            lista[x].gameObject.SetActive(true);
            int y = 0;
            while (y < inventory.inventory[x].itemsNumero) { 
           
                opciones.Add((y).ToString());
                y++;
            }
            opciones.Add((y).ToString());
            Debug.Log(inventory.inventory[x].itemName);
            nombresObjetosDeUsuario[x].text = inventory.inventory[x].itemName;
            Debug.Log(nombresObjetosDeUsuario[x].text);
            lista[x].ClearOptions();
            //opciones.Add((opciones[opciones.Count]. + 1).ToString());
            lista[x].AddOptions(opciones);
            opciones.Clear();
        }

        for (int x = 0; x < nombresObjetosDeUsuario.Count; x++)
        {
            if (x >= inventory.inventory.Count)
            {
                opciones.Clear();
                nombresObjetosDeUsuario[x].gameObject.SetActive(false);
                lista[x].gameObject.SetActive(false);
            }
        }
    }

    public void entregar() {
        bool menosValor = false;
        for (int x = 0; x<inventory.inventory.Count;x++) {
            int cantidad = 0;
            for (int y = 0; y < lista[x].value; y++ ) {
                cantidad++;
                for (int z = 0; z<textoNombreObjetos.Count;z++) {
                    if (inventory.inventory.Count>0 && textoNombreObjetos[z].text == inventory.inventory[x].itemName) {
                        string total = textoObjetoEntregado[z].text;
                        if (cantidad < total[2])
                        {
                            textoObjetoEntregado[z].text = cantidad + "/" + total[2];
                        }
                        else {
                            menosValor = true;
                        }
                        //Debug.Log(textoObjetoEntregado[z].text);
                    }
                }
                if (!menosValor)
                {
                    puntosConseguidos += inventory.inventory[x].itemReward * 2;
                }
                else {
                    puntosConseguidos += inventory.inventory[x].itemReward;
                }
                inventory.deleteItems(inventory.inventory[x].itemName);
                //Debug.Log(inventory.inventory[x].itemName);
            }
        }
        for (int x = 0; x<lista.Count;x++) {
            lista[x].value = 0;
        }
       
        textoPuntos.text = puntosConseguidos+"";
        mostrar();
        //Debug.Log(puntosConseguidos);
    }

    public void salir() {
        triggerDialogos.SetActive(true);
    } 
}
