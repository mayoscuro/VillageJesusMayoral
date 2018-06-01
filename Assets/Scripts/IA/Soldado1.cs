using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Soldado1 : MonoBehaviour
{
    public DayNightController dianoche;
    public List<Transform> nodosSalida;
    public List<Transform> nodosPatrulla;
    private List<Transform> caminoDeVuelta = new List<Transform>();
    private NavMeshAgent nav;
    private Animator anim;
    private float speed = 2;
    private bool final1;
    private bool final2;
    int x = 0;
    private bool animacionAntesDeParar;

    public void pararRutaParaHablar()
    {//Se llama desde el flowChart (Dialogos).
        nav.isStopped = true;
        animacionAntesDeParar = anim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        anim.SetBool("isIdle", true);
        
        
    }

    public void reanudarRutaDespuesDeHablar()
    {
        nav.isStopped = false;
        if (animacionAntesDeParar)
        {
            anim.SetBool("isIdle", true);
        }
        else {
            anim.SetBool("isIdle",false);
        }
    }

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        //Crear la lista para volver a esconderse:
        for (int y = nodosPatrulla.Count-1; y >= 0; y--)
        {
            caminoDeVuelta.Add(nodosPatrulla[y]);
        }
        for (int y = nodosSalida.Count-1; y >= 0; y--)
        {
            caminoDeVuelta.Add(nodosSalida[y]);
        }
        InvokeRepeating("customUpdate", 0,1);
    }

    // Update is called once per frame
    void customUpdate()
    {
        if (dianoche.currentTimeOfDay >= 0.20f && dianoche.currentTimeOfDay < 0.455 && !final1)
        {

            recorrerNodos(nodosSalida);
            if (final1)
            {
                x = 0;
            }
        }
        else if (dianoche.currentTimeOfDay >= 0.455f && dianoche.currentTimeOfDay < 0.6655 && !final2)
        {

            //Patruya de arriba a bajo de la colina y quedarse allí un tiempo.
            recorrerNodos(nodosPatrulla);
            if (final2)
            {
                x = 0;
            }
        }
        else if (dianoche.currentTimeOfDay >= 0.6655f && dianoche.currentTimeOfDay < 1 && final2)
        {
            recorrerNodos(caminoDeVuelta);
            //Vuelta la guarida.
            if (!final1)
            {//Nuevo
                x = 0;
            }
        }
    }


    private void recorrerNodos(List<Transform> listaNodos)
    {
        if (x < listaNodos.Count)
        {
            var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        else if (nav.remainingDistance < 0.6f)
        {
            if (!final1)
            {
                final1 = true;
            }
            else if (final1 && final2)
            {
                final1 = false;
                final2 = false;
            }
            else if (final1)
            {
                final2 = true;
            }

            //Debug.Log(final1 + "," + final2);
            anim.SetBool("isIdle", true);
        }


        if (nav.remainingDistance < 0.9f && x < listaNodos.Count)
        {
            nav.SetDestination(listaNodos[x].position);
            anim.SetBool("isIdle", false);
            //gameObject.transform.LookAt(nodos[x].position);
            x++;
        }
    }
}
