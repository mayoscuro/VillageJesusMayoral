using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GrupoDeSoldados2 : MonoBehaviour {
    //public DayNightController dayNight;
    public List<Transform> listaDeNodosDuranteElDia;
    public List<Transform> listaDeNodosDuranteLaNoche;
    private NavMeshAgent nav;
    private float speed = 2;
    private Animator anim;
    int camino1 = 0;
    int camino2 = 0;
    private bool llegado;
    private bool caminoDiaVuelta = false;
    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        anim.SetBool("isIdle", false);
        nav.SetDestination(listaDeNodosDuranteLaNoche[camino2].position);
    }

    public void pararRutaParaHablar()
    {//Se llama desde el flowChart (Dialogos).
        nav.isStopped = true;
        anim.SetBool("isIdle", true);
    }

    public void reanudarRutaDespuesDeHablar()
    {
        nav.isStopped = false;
        anim.SetBool("isIdle", false);
    }

    // Update is called once per frame
    void Update () {
        if (!nav.pathPending) {
            if (DayNightController.day)
            {
                camino2 = 0;
                if (!llegado) {
                    anim.SetBool("isIdle", false);
                }
                recorrerNodos(listaDeNodosDuranteElDia, ref camino1);
            }
            else {
                camino1 = 0;
                llegado = false;
                CancelInvoke();
                anim.SetBool("isIdle", false);
                recorrerNodos(listaDeNodosDuranteLaNoche, ref camino2);
            }
        }
	}

    void tiempoEspera() {
        //anim.SetBool("isIdle", true);
        anim.SetBool("isIdle", false);
        if (!caminoDiaVuelta)
        {
            if (camino1 >= listaDeNodosDuranteElDia.Count)
            {
                //camino1 = 0;
                caminoDiaVuelta = true;
            }
            else
            {
                nav.SetDestination(listaDeNodosDuranteElDia[camino1].position);
                anim.SetBool("isIdle", false);
                camino1++;
            }
        }
        else {
            if (camino1 <= 0)
            {
                //camino1 = 0;
                caminoDiaVuelta = false;
            }
            else
            {
                camino1--;
                nav.SetDestination(listaDeNodosDuranteElDia[camino1].position);
            }
        }
    }

    private void recorrerNodos(List<Transform> listaNodos,ref int x)
    {
        if (DayNightController.day && nav.remainingDistance < 0.9f && x < listaNodos.Count && !llegado)
        {

            llegado = true;
            anim.SetBool("isIdle", false);
            InvokeRepeating("tiempoEspera", 0, 25);

        }else if (DayNightController.day && nav.remainingDistance < 0.4 && llegado) {
            anim.SetBool("isIdle", true);
        } else if (!DayNightController.day) {
            if (x < listaNodos.Count)
            {
                var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            }

            
            if (nav.remainingDistance < 0.9f && x < listaNodos.Count)
            {
                //Debug.Log(x);
                nav.SetDestination(listaNodos[x].position);
                anim.SetBool("isIdle", false);
                //gameObject.transform.LookAt(nodos[x].position);
                x++;
            }
            else if (nav.remainingDistance < 0.4f)
            {
                anim.SetBool("isIdle", true);
            }
        }
    }
}
