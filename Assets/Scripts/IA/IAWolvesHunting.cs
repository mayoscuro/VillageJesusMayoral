using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAWolvesHunting : MonoBehaviour {

    //Nota--> Este script tiene en cuenta que la manada de lobos ya ha sido previamente spawneada en el mapa.

    [Header ("Game Objects")]
    [Tooltip("El game object del lobo alfa")]public GameObject alfa;
    [Tooltip("Los game object del resto de lobos que le acompañan")] public GameObject[] wolves;
    [Tooltip("El camino que realizará el lobo alfa")] public List<GameObject> alfaPath;//Este se aproxima al centro del circulo para combatir.
    [Tooltip("Recorrido que haran el resto de lobos mientras el alfa pelea")] public List<GameObject> wolvesPath; //Estos dan vueltas en circulos.

    //Variables privadas:
    private bool isAtaking = true;//Para saber si el lobo alfa va a atacar(true de prueba,para que ataque siempre poner a false cuando toda la ia este montada).

    int currentWayPoint = 0;
    float rotSpeed = 0.2f;
    float speed = 1.5f;
    float accuracyWayPoint = 5.0f;

    // Use this for initialization
    void Start () {
		
	}

    private void activatePaths() {
        wolvesPath[0].transform.parent.gameObject.SetActive(true);
        alfaPath[0].transform.parent.gameObject.SetActive(true);
    }

    private void desActivatePaths()
    {
        wolvesPath[0].transform.parent.gameObject.SetActive(false);
        alfaPath[0].transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //Vector3 direction = 
        
        if (isAtaking)
        {
            //wolvesIa.state = wolvesIa.state.Formation;
            activatePaths();//Activa los WayPoints para los caminos de los lobos.
            posicionarLobos();//Los posiciona en el lugar correcto.
            posicionarAlfa();//Lo posiciona en el lugar correcto.

            moverAlfa();//Lo mueve a la posición del centro del circulo, donde tendrá lugar la pelea.
            //wolvesIa.state = wolvesIa.state.Ataking;
            moverLobos(wolves[0]);

            
        }
        else {
            desActivatePaths();//Activa los WayPoints para los caminos de los lobos.
        }
	}

    void posicionarLobos() {
        for (int x = 0; x<wolves.Length;x++)
        {
            wolves[x].GetComponent<NavMeshAgent>().speed = 10f;
            wolves[x].GetComponent<NavMeshAgent>().destination = wolvesPath[x].transform.position;
            //moverLobos(wolves[x]);//Los mueve de casilla en casilla.
        }
    }

    void posicionarAlfa() {
        alfa.GetComponent<NavMeshAgent>().destination = alfaPath[0].transform.position;

    }

    void moverLobos(GameObject wolf) {
        for (int x = 0; x < wolvesPath.Count; x++ ) {
            if (Vector3.Distance(wolvesPath[currentWayPoint].transform.position,wolf.transform.position) <= accuracyWayPoint) {
                currentWayPoint++;
                if (currentWayPoint >=wolvesPath.Count) {
                    currentWayPoint = 0;
                }
                //Debug.Log("Dentro");
                wolf.GetComponent<NavMeshAgent>().destination = wolvesPath[currentWayPoint].transform.position;
            }
        }
    }

    void moverAlfa() {

    }

    public bool getIsAtaking() {
        return isAtaking;
    }
}

