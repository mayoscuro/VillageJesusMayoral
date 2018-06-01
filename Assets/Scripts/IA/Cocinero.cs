using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cocinero : MonoBehaviour {
    public List<Transform> caminoACocina;
    public List<Transform> caminoEnLaCocina;
    public List<Transform> caminoAlHuerto;
    private List<Transform> vueltaACasa = new List<Transform>();
    private float speedRotation;
    private Animator anim;
    private NavMeshAgent nav;
    int camino1 = 0;
    int camino2 = 0;
    int camino3 = 0;
    int camino4 = 0;
    bool cocinas = false;
    bool alHuerto = false;
    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        for (int y = caminoACocina.Count - 1; y >= 0; y--)
        {
            vueltaACasa.Add(caminoACocina[y]);
        }
    }

    public void pararRutaParaHablar()
    {//Se llama desde el flowChart (Dialogos).
        nav.isStopped = true;
        //anim.SetBool("isIdle", true);
    }

    public void reanudarRutaDespuesDeHablar()
    {
        nav.isStopped = false;
        //anim.SetBool("isIdle", false);
    }

    // Update is called once per frame
    void Update () {
        if (DayNightController.day)
        {
            if (alHuerto)
            {
                recorrerNodos(caminoAlHuerto, ref camino3);
            }
            else
            {
                camino4 = 0;
                recorrerNodos(caminoACocina, ref camino1);
            }
        }
        else {
            camino1 = 0;
            camino2 = 0;
            CancelInvoke();
            camino3 = 0;
            recorrerNodos(vueltaACasa, ref camino4);
        }
	}

    private void recorrerNodos(List<Transform> listaNodos,ref int x) {
        if (x < listaNodos.Count)
        {

            // Debug.Log("rotando y tal");
            var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        else if (DayNightController.day && nav.remainingDistance < 0.4f && !cocinas)
        {
            cocinas = true;
            InvokeRepeating("esperarHacerTarea", 0, 50);
        }
        else if(DayNightController.day && nav.remainingDistance < 0.4f && alHuerto)
        {
            alHuerto = false;
        }

        if (nav.remainingDistance < 0.5f && x < listaNodos.Count)
        {
            //Debug.Log("Recorriendo");
            nav.SetDestination(listaNodos[x].position);
            //Debug.Log("Nodo: " + listaNodos[x].name);
            anim.SetBool("isIdle", false);
            x++;
        }
    }

    void esperarHacerTarea() {
        anim.SetBool("isIdle", false);
        if (camino2 >= caminoEnLaCocina.Count)
        {
            camino2 = 0;
            if (Random.value > 0.5f)
            {
                cocinas = false;
                CancelInvoke();
                alHuerto = true;
            }
        }
        else
        {
            nav.SetDestination(caminoEnLaCocina[camino2].position);
            camino2++;
        }
    }
}
