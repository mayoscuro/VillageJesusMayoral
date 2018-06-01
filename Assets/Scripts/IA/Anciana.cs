using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anciana : MonoBehaviour {
    private NavMeshAgent nav;
    private Animator anim;
    public float speedRotation = 2;

    public List<Transform> nodosJefeAldea;
    public List<Transform> nodosPozo;
    public List<Transform> nodosSalirAldea;

    int x = 0;
    public bool unaVez = false;

    public enum rutinas {
        VisitarJefeAldea,
        IrAlPozo,
        SalirDeLaVaya
    }

    public rutinas rutina;

    public void pararRutaParaHablar()
    {//Se llama desde el flowChart (Dialogos).
        nav.isStopped = true;
        anim.SetBool("Walking", false);
        anim.SetBool("Sitting", false);
        anim.SetBool("Talking", true);
    }

    public void reanudarRutaDespuesDeHablar()
    {
        nav.isStopped = false;
        anim.SetBool("Walking", true);
        anim.SetBool("Sitting", false);
        anim.SetBool("Talking", false);
    }

    // Use this for initialization
    void Start () {
        nav = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();


        calcularAleatoriamenteAccionDelDia();
        InvokeRepeating("customUpdate", 0, 0.5f);
    }

    void calcularAleatoriamenteAccionDelDia() {
        unaVez = true;
        if (Random.value > 0.6f)
        {
            rutina = rutinas.IrAlPozo;
        }
        else if (Random.value > 0.5f)
        {
            rutina = rutinas.VisitarJefeAldea;
        }
        else {
            rutina = rutinas.SalirDeLaVaya;
        }
    }
	
	// Update is called once per frame
	void customUpdate () {
        if (gameObject.activeInHierarchy) {
            if (DayNightController.day)
            {
                if (rutina == rutinas.IrAlPozo) {
                    irALugar(nodosPozo);
                }
                else if (rutina == rutinas.VisitarJefeAldea) {
                    irALugar(nodosJefeAldea);
                }
                else {
                    irALugar(nodosSalirAldea);
                }
            }
            else {
                if (rutina == rutinas.IrAlPozo)
                {
                    volverACasa(nodosPozo);
                }
                else if (rutina == rutinas.VisitarJefeAldea)
                {
                    volverACasa(nodosJefeAldea);
                }
                else
                {
                    volverACasa(nodosSalirAldea);
                }
            }
        }
	}

    void volverACasa(List<Transform> listaNodos) {
        if (x > listaNodos.Count)
        {
            var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        else if (nav.remainingDistance < 0.3f)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Sitting", false);
            anim.SetBool("Talking", false);
        }

        if (nav.remainingDistance < 0.9f && x > 0)
        {
            nav.SetDestination(listaNodos[x - 1].position);
            anim.SetBool("Walking", true);
            anim.SetBool("Sitting", false);
            anim.SetBool("Talking", false);
            //gameObject.transform.LookAt(nodos[x].position);
            x--;
        }

        if (x == 0 && !unaVez)
        {
            calcularAleatoriamenteAccionDelDia();
        }

    }


    void irALugar(List<Transform> listaNodos) {
        if (x < listaNodos.Count)
        {
            var targetRotation = Quaternion.LookRotation(listaNodos[x].transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        else if (nav.remainingDistance < 0.6f)
        {
            //Debug.Log(listaNodos[0].transform.parent.gameObject.name);
            if (listaNodos[0].transform.parent.gameObject.name == "CaminoAlJefeDeLaAldea")
            {
                anim.SetBool("Sitting", false);
                anim.SetBool("Walking", false);
                anim.SetBool("Talking", true);
            }
            else if (listaNodos[0].transform.parent.gameObject.name == "CaminoAElPozo")
            {
                anim.SetBool("Talking", false);
                anim.SetBool("Walking", false);
                anim.SetBool("Sitting", false);
            }
            else
            {
                anim.SetBool("Sitting", false);
                anim.SetBool("Talking", false);
                anim.SetBool("Walking", false);
            }
            unaVez = false;
        }
        if (!nav.pathPending && nav.remainingDistance < 0.9f && x < listaNodos.Count)
        {
            nav.SetDestination(listaNodos[x].position);
            x++;
            anim.SetBool("Walking", true);
            anim.SetBool("Sitting", false);
            anim.SetBool("Talking", false);
            //gameObject.transform.LookAt(nodos[x].position);
            
        }
    }
}
