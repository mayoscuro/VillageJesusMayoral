using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JefeDeLaAldea : MonoBehaviour {
    public List<Transform> nodosAPuerta;
    public List<Transform> SalirDeLaAldea;
    private List<Transform> vueltaADormir = new List<Transform>();
    private Animator anim;
    private NavMeshAgent nav;
    private float speedRotation = 2;
    public DayNightController dayNight;
    int camino1 = 0;
    int camino2 = 0;
    int camino3 = 0;

    public void pararRutaParaHablar() {//Se llama desde el flowChart (Dialogos).
        nav.isStopped = true;
        anim.SetBool("Talking", true);
        anim.SetBool("Walking", false);
    }

    public void reanudarRutaDespuesDeHablar() {
        nav.isStopped = false;
        anim.SetBool("Talking", false);
        anim.SetBool("Walking", false);
    }

    // Use this for initialization
    void Start() {
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        for (int y = SalirDeLaAldea.Count - 1; y >= 0; y--)
        {
            vueltaADormir.Add(SalirDeLaAldea[y]);
        }
        for (int y = nodosAPuerta.Count - 1; y >= 0; y--)
        {
            vueltaADormir.Add(nodosAPuerta[y]);
        }
        InvokeRepeating("customUpdate", 0, 0.5f);

    }

    // Update is called once per frame
    void customUpdate() {
        if (gameObject.activeInHierarchy) {
            if (DayNightController.day)
            {
                recorrerNodos(nodosAPuerta, ref camino1);
            }
            else if (!DayNightController.day && dayNight.currentTimeOfDay >= 0.75f && dayNight.currentTimeOfDay < 0.95f)//Al Atardecer se va a mirar a la cuesta de la aldea.
            {
                recorrerNodos(SalirDeLaAldea, ref camino2);
                camino3 = 0;
            }
            else if (!DayNightController.day && dayNight.currentTimeOfDay >= 0.96f || dayNight.currentTimeOfDay <= 0.1f && !DayNightController.day && camino1 != 0) {
                recorrerNodos(vueltaADormir, ref camino3);
            }
            /*
            if (camino1 >= nodosAPuerta.Count && camino2 >= SalirDeLaAldea.Count && camino3 >= vueltaADormir.Count) {
                camino1 = 0;
                camino2 = 0;
            }*/
        }

    }

    private void recorrerNodos(List<Transform> listaNodos, ref int x) {
        if (x < listaNodos.Count)
        {
            var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);
            anim.SetBool("Walking", true);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        
        Debug.Log(nav.remainingDistance);
        Debug.Log(x);

        if (!nav.pathPending && nav.remainingDistance < 0.5f && x < listaNodos.Count)
        {

            nav.ResetPath();
            nav.SetDestination(listaNodos[x].position);
            x++;
        }else if (nav.remainingDistance < 0.5f)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Talking", false);
        }
        ///Debug.Log(x);

    }
}
