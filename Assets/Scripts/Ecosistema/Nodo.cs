using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour {
    [Header("Datos que se van actualizando")]//Estos datos tienen que ocultarse en el inspector
    [HideInInspector]
public int numeroVivos;
    [HideInInspector] public int numeroCaidos;

    [HideInInspector]public int numVivosSemanaPasada;
    [HideInInspector] public int numCaidosSemanaPasada;

    [Header("Depredadores")]
    [Tooltip("Depredadores que se alimentan de este animal")]public List<Nodo> depredadores;
    [Tooltip("porcentajes de probabilidad que tiene el depredador de cazar a este animal(en orden: el porcentaje del animal que ocupa la primera posición de la lista de depredadores, tambien tiene que ocupar el primer lugar" +
        "en la lista de probabilidades)")]
    public List<int> porcentajeProbabilidades;
    [Tooltip("Numero de depredadores activos en la escena")]public List<int> numeroDepredadores;

    [Header("Datos iniciales")]
    public int numeroVivosInicioSemana;
    
    //Datos para recorrer el grafo
    [Header("Datos necesarios para recorrer el grafo")]
    public bool visitado;
    [Tooltip("Otros nodos del grafo, que son alimento del animal actual")]public List<Nodo> comida;


    public void ActualizarDepredadores() {
        numeroDepredadores.Clear();//En teoria esto lo borra todo
        foreach (Nodo depredador in depredadores) {
            numeroDepredadores.Add(depredador.numeroVivos);
            //Debug.Log(gameObject.name + " , depredadores: " + depredador.gameObject.name + "numeroDepredadores" + numeroDepredadores.Count);
        }
    }

    private void ActualizarDepredadoresPrimeraVez() {
        foreach (Nodo depredador in depredadores)
        {
            numeroDepredadores.Add(depredador.numeroVivosInicioSemana);
            //Debug.Log(gameObject.name + " , depredadores: " + depredador.gameObject.name + "numeroDepredadores" + numeroDepredadores.Count);

        }
    }

    public void inicializarVivos() {
        numeroVivos = numeroVivosInicioSemana;
    }

    private void Start()
    {
        inicializarVivos();
        ActualizarDepredadoresPrimeraVez();
    }


}
