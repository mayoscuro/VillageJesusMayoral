using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Aldeana : MonoBehaviour {
    public List<Transform> caminoALaAldea;
    public List<Transform> caminoAlCocinero;
    public List<Transform> caminoAJefeDeLaAldea;
    public List<Transform> caminoAHuerto;
    public List<Transform> caminoDeHuerto;
    private List<Transform> caminoDeVuelta = new List<Transform>();
    private float speedRotation;
    private Animator anim;
    private NavMeshAgent nav;
    int camino1 = 0;
    int camino2 = 0;
    int camino3 = 0;
    int camino4 = 0;
    int camino5 = 0;
    int camino6 = 0;
    private bool primeraTarea = true;
    private bool cuidarDelHuerto = false;
    private bool hablarCocinero = false;
    private bool hablarConJefe = false;
    private bool llegado;

    public AldeanaCollider colliderAldeana;

    public void pararRutaParaHablar()
    {//Se llama desde el flowChart (Dialogos).
        nav.isStopped = true;
        anim.SetBool("Walk", false);
        anim.SetBool("Talk", true);
        //anim.SetBool("isIdle", true);
    }

    public void reanudarRutaDespuesDeHablar()
    {
        nav.isStopped = false;
        //anim.SetBool("isIdle", false);
        anim.SetBool("Walk", true);
        anim.SetBool("Talk", false);
    }

    public enum tareas {
        hablarCocinero,
        hablarJefeAldea,
        huerto,
        nada
    }
    private tareas tarea;
    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        tarea = tareas.nada;
        for (int y = caminoALaAldea.Count - 1; y >= 0; y--)
        {
            caminoDeVuelta.Add(caminoALaAldea[y]);
        }
        //InvokeRepeating("customUpdate", 0, 1);
    }

    void nuevaTareaAleatoria() {

        CancelInvoke();

        if (Random.value > 0.8f || hablarConJefe && hablarCocinero)
        {
            tarea = tareas.huerto;
            Debug.Log("Me voy al huerto");
        }
        else if (Random.value > 0.3f && !hablarConJefe || hablarCocinero)
        {
            tarea = tareas.hablarJefeAldea;
            Debug.Log("Me voy al jefe");
        }
        else if(!hablarCocinero || hablarConJefe)
        {
            tarea = tareas.hablarCocinero;
            Debug.Log("Hablo con el cocinero");
        }
        Debug.Log(tarea.ToString() +"    hjgjhgjhgjhgjhgjhgjhgjhgjhgjhghfdgtñlkkjhsdftgykh");
        //InvokeRepeating("customUpdate", 0, 1);
    }

    void esperarHacerTarea() {
        nuevaTareaAleatoria();
    }

    void recorrerNodosConPausa()
    {
        anim.SetBool("Walk", true);
        llegado = true;
        if (camino5 >= caminoDeHuerto.Count)
        {
            anim.SetBool("Walk", true);
            camino5 = 0;
            if (Random.value > 0.5f && !hablarCocinero || Random.value > 0.5f && !hablarConJefe || Random.value > 0.5f && !hablarConJefe && !hablarCocinero)
            {
                cuidarDelHuerto = false;
                llegado = false;
                anim.SetBool("Walk", false);
                nuevaTareaAleatoria();
            }
        }
        else
        {
            anim.SetBool("Walk", true);
            if (!nav.pathPending) {
                nav.SetDestination(caminoDeHuerto[camino5].position);
                camino5++;
                InvokeRepeating("llegadoANodoDeParada", 0, 3);
            }
            Debug.Log("malaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
        

    }

    private void llegadoANodoDeParada() {
        if (nav.remainingDistance < 0.5f) {
            CancelInvoke("llegadoANodoDeParada");
            anim.SetBool("Walk", false);
            
        }
    }

    // Update is called once per frame
    void Update () {
        if (DayNightController.day)
        {
            camino6 = 0;//Se reinicia el camino de por la noche.
            if (cuidarDelHuerto && !llegado)
            {
                InvokeRepeating("recorrerNodosConPausa",0,30);
            }
            else if (primeraTarea)
            {
                recorrerNodos(caminoALaAldea, ref camino1);
            }
            else if (tarea == tareas.hablarCocinero)
            {
                recorrerNodos(caminoAlCocinero, ref camino2);
            }
            else if (tarea == tareas.hablarJefeAldea)
            {
                recorrerNodos(caminoAJefeDeLaAldea, ref camino3);
            }
            else if(tarea == tareas.huerto)
            {
                recorrerNodos(caminoAHuerto, ref camino4);
            }
        }
        else {
            camino1 = 0;//Se reinician todos los caminos y booleanos usados durante el dia.
            camino2 = 0;
            camino3 = 0;
            cuidarDelHuerto = false;
            CancelInvoke();
            llegado = false;
            primeraTarea = true;
            hablarCocinero = false;
            hablarConJefe = false;
            camino4 = 0;
            camino5 = 0;
            tarea = tareas.nada;
            if (!colliderAldeana.aldeanaEnCasa)
            {
                recorrerNodos(caminoDeVuelta, ref camino6);
                
            }
        }


	}


    void recorrerNodos(List<Transform> listaNodos,ref int x) {
        //Debug.Log(listaNodos.Count);

        

        if (x < listaNodos.Count)
        {
            anim.SetBool("Talk", false);
            anim.SetBool("Walk", true);
            
            // Debug.Log("rotando y tal");
            var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        else if (nav.remainingDistance < 0.4f && primeraTarea && DayNightController.day)
        {
            primeraTarea = false;
            Debug.Log("Decidiendo");
            nuevaTareaAleatoria();
        }
        else if (nav.remainingDistance < 0.4f && tarea == tareas.huerto && DayNightController.day && !cuidarDelHuerto)
        {
            cuidarDelHuerto = true;
            Debug.Log("Huerto");
        }
        else if(nav.remainingDistance < 0.4f && tarea == tareas.hablarCocinero && DayNightController.day && !hablarCocinero) {
            hablarCocinero = true;
            anim.SetBool("Walk", false);
            anim.SetBool("Talk", true);
            Invoke("esperarHacerTarea", 80);
            Debug.Log("cocinero");
            
        }
        else if (nav.remainingDistance < 0.4f && tarea == tareas.hablarJefeAldea && DayNightController.day && !hablarConJefe)
        {
            hablarConJefe = true;
            anim.SetBool("Walk", false);
            anim.SetBool("Talk", true);
            Invoke("esperarHacerTarea", 80);
            Debug.Log("Jefe");

        }
        //Debug.Log(x);
        //Debug.Log(x);

        if (!nav.pathPending && nav.remainingDistance < 0.5f && x < listaNodos.Count)
        {
            //Debug.Log("Recorriendo");
            nav.SetDestination(listaNodos[x].position);
            //  Debug.Log("Nodo: " + listaNodos[x].name);

            anim.SetBool("Talk", false);
            anim.SetBool("Walk", true);
            
            x++;
        }

    }
}
