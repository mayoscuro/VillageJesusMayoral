using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AnimalsBasicIA : MonoBehaviour
{

    public GameObjectList animal = new GameObjectList();
    private NavMeshAgent nav;
    private Animator anim;
    public GameObject esferaPequeña;
    public GameObject esferaGrande;
    private float speedRotation = 2;
    public bool calculando;
    int aleatorio;
    Vector3 nuevaDir;
    private bool pausa;
    private bool paradaDeNavMesh;
    private GameObject player;
    private bool escapando;
    [Header("Para saber si es loboo alfa")]
    public bool alfa;
    [Tooltip("Para saber cual es el alfa al que deben seguir el resto")]public GameObject alfagameObject;
    [Tooltip("Para saber si un lobo esta reproduciendo la animación de atacar")] public bool atacando;

    private void Start()
    {
       
        nav = GetComponent<NavMeshAgent>();
        
        anim = /*transform.GetChild(0).*/GetComponent<Animator>();
        

        player = GameObject.FindGameObjectWithTag("Player");
        
        aleatorio = Random.Range(0, animal.nodosDisponibles.Count - 1);
        if (animal.tipo == "Zorro" || animal.tipo == "Ciervo" || animal.tipo == "Lobo" && alfa)
        {
            nav.destination = animal.nodosDisponibles[aleatorio].position;
            alfagameObject = null;
        }
        else if (animal.tipo == "Lobo" && !alfa) {
            nav.destination = alfagameObject.transform.position;
        }
        if (animal.tipo == "Zorro")
        {
            animal.speed = Random.Range(3, 20);
            nav.speed = animal.speed;
            if (nav.speed < 11)
            {
                anim.SetBool("Andar", true);
                anim.SetBool("Correr", false);
                //anim.SetBool("Comer", false);
                anim.SetBool("Mirar", false);
            }
            else
            {
                anim.SetBool("Correr", true);
                anim.SetBool("Andar", false);
                anim.SetBool("Mirar", false);
                // anim.SetBool("Comer", false);
            }

        }
        else if (animal.tipo == "Ciervo")
        {
            animal.speed = Random.Range(3, 14);
            nav.speed = animal.speed;
            anim.SetBool("Andar", true);
            anim.SetBool("Correr", false);
            anim.SetBool("Mirar", false);

            if (nav.speed < 11)
            {
                nav.speed = 2;
            }
            else {
                nav.speed = 4;
            }

        }
        else if (animal.tipo == "Lobo")
        {
            animal.speed = Random.Range(3, 15);
            nav.speed = animal.speed;
            anim.SetBool("Andar", true);
            anim.SetBool("Correr", false);
            anim.SetBool("Mirar", false);

            anim.speed = 1;
            if (nav.speed < 11)
            {
                //nav.speed = 2;
            }
            else
            {
                //nav.speed = 4;
            }
            if (!alfa) {
                nav.speed = alfagameObject.GetComponent<NavMeshAgent>().speed - Random.Range(0.5f,0.9f);
            }
        }
        if (animal.tipo == "Zorro" || animal.tipo == "Ciervo" || animal.tipo == "Lobo" && alfa)
        {
            InvokeRepeating("llegandoANodo", 0, 2);
        }
        else if (animal.tipo == "Lobo" && !alfa) {
            InvokeRepeating("followAlfa", 0, 2);
        }
    }

    void followAlfa() {
        if (!nav.pathPending && nav.remainingDistance <= 10f && !calculando) {
            nav.destination = alfagameObject.transform.position;
            nav.speed = alfagameObject.GetComponent<NavMeshAgent>().speed - Random.Range(0.5f, 0.9f);
        }

        if (!calculando)
        {
            var targetRotation = Quaternion.LookRotation(animal.nodosDisponibles[aleatorio].transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
    }

    private void llegandoANodo()
    {
        
        if (aleatorio < animal.nodosDisponibles.Count && !calculando)
        {

            // Debug.Log("rotando y tal");
            var targetRotation = Quaternion.LookRotation(animal.nodosDisponibles[aleatorio].transform.position - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
        if (!nav.pathPending && nav.remainingDistance <= 10f && !calculando)
        {
            
            escapando = false;
            calculando = true;
            aleatorio = Random.Range(0, animal.nodosDisponibles.Count - 1);
            nav.destination = animal.nodosDisponibles[aleatorio].position;

            if (animal.tipo == "Zorro")
            {
                anim.speed = 1;
                nav.speed = Random.Range(3, 20);
                if (nav.speed < 11)
                {
                    
                    anim.SetBool("Andar", true);
                    anim.SetBool("Correr", false);
                    // anim.SetBool("Comer", false);
                    anim.SetBool("Mirar", false);
                }
                else
                {
                    anim.SetBool("Correr", true);
                    anim.SetBool("Andar", false);
                    anim.SetBool("Mirar", false);
                    //anim.SetBool("Comer", false);
                }
            }
            else if (animal.tipo == "Ciervo")
            {
                anim.speed = 2;
                nav.speed = Random.Range(3, 10);
                anim.SetBool("Andar", true);
                anim.SetBool("Mirar", false);
            }
            else if(animal.tipo == "Lobo") {
                anim.speed = 1;
                nav.speed = Random.Range(3, 15);
                anim.SetBool("Andar", true);
                anim.SetBool("Mirar", false);
            }
            //x++;
            //Debug.Log(x);
        }
        if (animal.tipo == "Zorro") {
            IAZorro();
        } else if (animal.tipo == "Lobo") {
            IALobo();
        } else if (animal.tipo == "Ciervo") {
            IACiervo();
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
            finalPosition.x += Random.Range(0,10);
            finalPosition.z += Random.Range(0,20);
        }
        return finalPosition;
    }

    IEnumerator continuar()
    {
        
        
        if (animal.tipo == "Zorro" && !escapando)
        {
            nav.isStopped = true;
            //Debug.Log("Segundo");
            anim.SetBool("Andar", false);
            anim.SetBool("Correr", false);
            anim.SetBool("Mirar", false);
            //anim.SetBool("Comer", true);
                
           
            yield return new WaitForSeconds(10f);
            nav.isStopped = false;
            //Debug.Log("Tercero");
            animal.speed = Random.Range(3, 15);
            nav.speed = animal.speed;


            if (!nav.isStopped)
            {
                if (nav.speed < 11)
                {
                    //anim.SetBool("Comer", false);
                    anim.SetBool("Correr", false);
                    anim.SetBool("Mirar", false);
                    anim.SetBool("Andar", true);
                }
                else
                {
                    anim.SetBool("Andar", false);
                    anim.SetBool("Mirar", false);
                    //anim.SetBool("Comer", false);
                    anim.SetBool("Correr", true);
                }
            }



           
            
            //if (!nav.pathPending)
            //{
            nuevaDir = RandomNavmeshLocation(Random.Range(100f,180f));
            nav.SetDestination(nuevaDir);

            InvokeRepeating("bucle",0,5);
            //}

            
        } else if (animal.tipo == "Ciervo" && !escapando) {

            nav.isStopped = true;
            //Debug.Log("Segundo");
            anim.SetBool("Andar", false);
            anim.SetBool("Mirar", false);
            //anim.SetBool("Comer", true);


            yield return new WaitForSeconds(Random.Range(20,30));
            nav.isStopped = false;
            //Debug.Log("Tercero");
            animal.speed = Random.Range(3, 10);
            nav.speed = animal.speed;


            if (!nav.isStopped)
            {
                //anim.SetBool("Correr", false);
                anim.SetBool("Mirar", false);
                anim.SetBool("Andar", true);
                
            }





            //if (!nav.pathPending)
            //{
            nuevaDir = RandomNavmeshLocation(Random.Range(50f, 100f));
            nav.SetDestination(nuevaDir);

            InvokeRepeating("bucle", 0, 5);
        }
        else if (animal.tipo == "Lobo" && !escapando)
        {
            nav.isStopped = true;
            //Debug.Log("Segundo");
            anim.SetBool("Andar", false);
            anim.SetBool("Mirar", false);
            //anim.SetBool("Comer", true);


            yield return new WaitForSeconds(10f);
            nav.isStopped = false;
            //Debug.Log("Tercero");
            animal.speed = Random.Range(3, 15);
            nav.speed = animal.speed;


            if (!nav.isStopped)
            {
                //anim.SetBool("Correr", false);
                anim.SetBool("Mirar", false);
                anim.SetBool("Andar", true);

            }





            //if (!nav.pathPending)
            //{
            nuevaDir = RandomNavmeshLocation(Random.Range(100f, 180f));
            nav.SetDestination(nuevaDir);

            InvokeRepeating("bucle", 0, 5);
        }
    }

    private void Update()
    {
        if (animal.tipo == "Lobo" && !alfa && alfagameObject !=null && alfagameObject.GetComponent<Animal>().muerto || animal.tipo == "Lobo" && !alfa && alfagameObject == null)
        {
            nav.speed = 30;
            nav.destination = animal.nodosDisponibles[1].position;
            if (!nav.pathPending && nav.remainingDistance <= 1f)
            {
                Destroy(gameObject);//Si el lobo no es alfa y el alfa muere, entonces se va a un nodo aleatorio y se destruye (no cuenta como muerte del animal, solo representa que estos han huido).
            }
        }
        else
        {

            if (gameObject.GetComponent<Animal>().muerto)
            {
                CancelInvoke();
                nav.destination = transform.position;
                nav.isStopped = true;
                escapando = false;
                calculando = false;
                anim.SetBool("Mirar", false);
                anim.SetBool("Andar", false);
                anim.SetBool("Correr", false);
                anim.SetBool("Comer", false);

            }
            else
            {
                Vector3 auxiliar = nav.destination;
                if (esferaGrande.GetComponent<CollidersAnimales>().collidingPlayerOrWolves && player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().runing && !escapando)
                {
                    anim.SetBool("Correr", false);
                    anim.SetBool("Andar", false);
                    anim.SetBool("Mirar", true);
                    nav.isStopped = true;

                    if (esferaPequeña.GetComponent<CollidersAnimales>().collidingPlayerOrWolves)
                    {
                        anim.SetBool("Mirar", false);
                        escapando = true;
                        nav.isStopped = false;
                        if (animal.tipo == "Zorro")
                        {
                            anim.SetBool("Correr", true);
                            anim.speed = 2;
                            nav.speed = 30;
                        }
                        else if (animal.tipo == "Ciervo")
                        {
                            anim.SetBool("Andar", true);
                            anim.speed = 3;
                            nav.speed = 17;
                        }
                        else if (animal.tipo == "Lobo")
                        {
                            anim.SetBool("Andar", true);
                            //anim.enabled = false;
                            anim.speed = 1;
                            nav.speed = 20;
                        }
                        calculando = false;
                        if (animal.tipo != "Lobo")
                        {
                            aleatorio = Random.Range(0, animal.nodosDisponibles.Count - 1);
                            Debug.Log(aleatorio);
                            nav.destination = animal.nodosDisponibles[aleatorio].position;
                        }
                        else
                        {
                            InvokeRepeating("persecucion", 0, 0.1f);
                            Invoke("pararPersecucionLobo", 20);
                        }
                        InvokeRepeating("prueba", 0, 2);
                    }
                }
                else if (!escapando)
                {
                    nav.isStopped = false;
                    nav.destination = auxiliar;
                }
            }
        }
    }
    void persecucion() {
        nav.destination = player.transform.position;

        // Debug.Log("rotando y tal");
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);

        if (gameObject.transform.GetChild(4).GetComponent<ColliderAtacarLobos>().collidingPlayer) {//el cuarto hijo es el collider del ataque.
            //nav.isStopped = true;
            anim.SetBool("Atacar",true);//Poner a true el booleano del script de atacar de los lobos.
            atacando = true;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Atacar"))
        {
            anim.SetBool("Atacar", false);
            atacando = false;
        }
    }

    void pararPersecucionLobo() {
        Debug.Log("cancelo persecución");
        calculando = true;
        escapando = false;
        CancelInvoke("persecucion");
        CancelInvoke("pararPersecucionLobo");
        if (alfa)
        {
            nav.destination = animal.nodosDisponibles[aleatorio].position;//Como no se ha cambiado, esta sería su posición antes de la persecución.
            InvokeRepeating("llegandoANodo", 0, 2);
        }
        else if(!alfa && alfagameObject != null) {
            nav.destination = alfagameObject.transform.position;
            nav.speed = alfagameObject.GetComponent<NavMeshAgent>().speed - Random.Range(0.5f, 0.9f);
            InvokeRepeating("followAlfa", 0, 2);
        }
    }

    void bucle() {
        if (!nav.pathPending && nav.remainingDistance <= 1f)
        {
            CancelInvoke();
            pausa = false;
            //InvokeRepeating("IAZorro", 0, 10);
            if (animal.tipo == "Zorro") {
                IAZorro();
            } else if (animal.tipo == "Lobo") {
                IALobo();
            } else if (animal.tipo == "Ciervo") {
                IACiervo();
            }
           // Debug.Log("4º");
        }
    }

    private void IAZorro() {
        if (!nav.pathPending && nav.remainingDistance <= 4f && calculando && !pausa || nav.remainingDistance >= 4f && calculando && !pausa)
        {
            CancelInvoke();
            //InvokeRepeating("continuar",0,20);
            //Debug.Log("Primero");
            StartCoroutine("continuar");

            pausa = true;
            

            /*if (paradaDeNavMesh)
            {
                
                nav.isStopped = true;
                paradaDeNavMesh = false;
            }*/

        }

        if (calculando)
        {
            var targetRotation = Quaternion.LookRotation(nuevaDir - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
    }

    private void IALobo()
    {
        if (!nav.pathPending && nav.remainingDistance <= 4f && calculando && !pausa || nav.remainingDistance >= 4f && calculando && !pausa)
        {
            CancelInvoke();
            //InvokeRepeating("continuar",0,20);
            //Debug.Log("Primero");
            StartCoroutine("continuar");

            pausa = true;


            /*if (paradaDeNavMesh)
            {
                
                nav.isStopped = true;
                paradaDeNavMesh = false;
            }*/

        }

        if (calculando)
        {
            var targetRotation = Quaternion.LookRotation(nuevaDir - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
    }

    private void IACiervo()
    {
        if (!nav.pathPending && nav.remainingDistance <= 4f && calculando && !pausa || nav.remainingDistance >= 4f && calculando && !pausa)
        {
            CancelInvoke();
            //InvokeRepeating("continuar",0,20);
            //Debug.Log("Primero");
            StartCoroutine("continuar");

            pausa = true;


            /*if (paradaDeNavMesh)
            {
                
                nav.isStopped = true;
                paradaDeNavMesh = false;
            }*/

        }

        if (calculando)
        {
            var targetRotation = Quaternion.LookRotation(nuevaDir - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
    }

    [System.Serializable]
    public class GameObjectList
    {
        public string tipo;
        public List<Transform> nodosDisponibles;
        [HideInInspector] public int speed;
    }
}