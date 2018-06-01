using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EventosDeAnimalesEspeciales : MonoBehaviour {
    public GameObject kodama1;
    public GameObject exclamacíonKodama;
    public GameObject player;
    private bool kodamaAparecido;
    private bool lordAparecido;

    [Header("Cosas para el evento tipo 1")]
    
    public KodamaCollider kodama;//Se pone a true dentro del collider de cada criatura.
    
   
    //public List<GameObject> Kodamas;

    [Header("Cosas para el evento tipo 2")]

    public KodamaCollider kodamaSmall;//Se pone a true dentro del collider de cada criatura.
    public GameObject transportadorDelCaminoKodama;
    public List<Transform> caminoDeKodama;
    private NavMeshAgent nav;
    private bool empezarCamino;
    int x = 1;//para seguir el camino.

    [Header("Cosas Lord of the forest")]
    public GameObject lordOfTheForest;
    public GameObject particulasLord;
    public GameObject exclamacionLord;
    public KodamaCollider colliderEsfera;


    [Header("Zonas y particulas")]
    public List<Transform> spawnZones;
    public GameObject particulasHojas;

    [Header("Otros")]
    private bool kodamaActivo = true;
    [HideInInspector]public bool aparicion=false;
    private Animator animatorKodama;

    public bool eventoKodamaTipo1;//Aparece un Kodama en un bosque aleatorio y solo da el objeto al jugador y se va.
    public bool eventoKodamaTipo2;//Aparece un kodama en un bosque y se escapa un poco del jugador, hasta que desaparece dejando un objeto.
    public bool eventoKodamaTipo3;//El baile de los Kodama.

    public bool eventoLordOfTheForest;//Aparece en un bosque aleatorio, si el jugador se acerca este se paraliza y el señor del bosque se va. 
                                       //Si no se acerca, dejará un objeto en el suelo y desaparecerá.


    public void eventRandomChoose() {
        if (Random.value > 0.7 && !kodamaAparecido) {//0.7
            eventoKodamaTipo1 = true;
            
            kodamaAparecido = true;
            return;
        }

        if (Random.value > 0/*.8*/ && !kodamaAparecido) {//0.8
            aparicion = true;
            eventoKodamaTipo2 = true;
            aparicion = true;
            return;
        }
        /*
        if (Random.value > 0.9) {
            eventoKodamaTipo3 = true;
            return;
        }
        */
        if (Random.value > 0.9 || Random.value >  0.5 && kodamaAparecido) {//Si aparece un kodama hay más posibilidades de que esa sepana aparezca el señor del bosque.
            eventoLordOfTheForest = true;
            aparicion = true;
            lordAparecido = true;
            return;
        }
    }

	// Use this for initialization
	void Start () {
        animatorKodama = kodama1.GetComponent<Animator>();
        nav = kodama1.GetComponent<NavMeshAgent>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (eventoKodamaTipo1) {
            eventoKodamaObjeto();
        } else if (eventoKodamaTipo2) {
            eventoKodamaJugar();
        } else if (eventoKodamaTipo3) {
            eventoKodamaBaile();
        } else if (eventoLordOfTheForest) {
            eventoLordOfTheForestObjeto();
        }

        if (WeekController.numberOfDays == 8) {//Una vez cada semana pueden volver a aparecer.
            kodamaAparecido = false;
            lordAparecido = false;
        }
    }

    IEnumerator esperarAnimacion() {
        animatorKodama.SetBool("Bailar", false);
        animatorKodama.SetBool("Entregar", true);
        animatorKodama.SetBool("Correr", false);
        yield return new WaitForSeconds(2f);
        particulasHojas.SetActive(true);
        exclamacíonKodama.SetActive(false);
        animatorKodama.SetBool("Bailar", false);
        animatorKodama.SetBool("Entregar", false);
        animatorKodama.SetBool("Correr", false);
        yield return new WaitForSeconds(2f);
        kodama1.SetActive(false);
        particulasHojas.SetActive(false);
        eventoKodamaTipo1 = false;//Esto solo debería desactivarse al final de cada semana,en un update o algo.
        eventoKodamaTipo2 = false;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().continueMovement();


    }

    IEnumerator llegada() {
        particulasHojas.SetActive(true);
        yield return new WaitForSeconds(1f);
        kodama1.SetActive(true);
        particulasHojas.SetActive(false);
        if (eventoKodamaTipo2) empezarCamino = true; 
    }

    public void eventoKodamaObjeto(){
        animatorKodama.SetBool("Bailar",false);
        animatorKodama.SetBool("Entregar",false);
        animatorKodama.SetBool("Correr", false);
        if (aparicion)
        {
            aparicion = false;
            
            Transform lugar = spawnZones[Random.Range(0, spawnZones.Count - 1)];
            kodama1.transform.position = new Vector3(lugar.transform.position.x + Random.Range(0, 30), lugar.transform.position.y, lugar.transform.position.z + Random.Range(0, 30));
            
            StartCoroutine("llegada");

        }

        if (kodama.jugadorCerca && eventoKodamaTipo1) {
            exclamacíonKodama.SetActive(true);
            exclamacíonKodama.transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z));
            kodama1.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            kodama.jugadorCerca = false;
            player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().stopPlayer();
            //parar la animación aqui.
            StartCoroutine("esperarAnimacion");
        }
    }

    public void eventoKodamaJugar() {
            if (aparicion)
            {
                kodama1.SetActive(true);
                aparicion = false;
                animatorKodama.SetBool("Bailar", false);
                animatorKodama.SetBool("Entregar", false);
                animatorKodama.SetBool("Correr", false);
                Transform lugar = spawnZones[0];//Se va al pinar
                kodama1.transform.position = new Vector3(lugar.transform.position.x + Random.Range(0, 20), kodama1.transform.position.y, lugar.transform.position.z + Random.Range(0, 20));
                nav.isStopped = false;
                StartCoroutine("llegada");

            }
            if (empezarCamino)
            {
                empezarCamino = false;
                // Debug.Log("estoy activo wei");
                nav.SetDestination(caminoDeKodama[0].position);

                //Debug.Log(nav.remainingDistance);

            }
            animatorKodama.SetBool("Correr", true);
            if (!nav.pathPending && nav.remainingDistance < 0.5f)
            {
                //Debug.Log("No puedo");
                nav.SetDestination(caminoDeKodama[x].position);
                x++;

                if (x == caminoDeKodama.Count - 1)
                {
                    x = 0;
                }

            }
            if (kodamaSmall.jugadorCerca && eventoKodamaTipo2)
            {
                nav.isStopped = true;
                exclamacíonKodama.SetActive(true);
                exclamacíonKodama.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
                kodama1.transform.LookAt(new Vector3(0, player.transform.position.y, player.transform.position.z));
                kodama.jugadorCerca = false;
                player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().stopPlayer();
                //parar la animación aqui.
                StartCoroutine("esperarAnimacion");
            }
    }

    public void eventoKodamaBaile() {




    }

    public void eventoLordOfTheForestObjeto() {
        if (aparicion)
        {
            aparicion = false;

            Transform lugar = spawnZones[Random.Range(0, spawnZones.Count - 1)];
            lordOfTheForest.transform.position = new Vector3(lugar.transform.position.x + Random.Range(0, 30), lugar.transform.position.y, lugar.transform.position.z + Random.Range(0, 30));

            StartCoroutine("llegadaLord");

        }

        if (colliderEsfera.jugadorCerca && eventoLordOfTheForest)
        {
            exclamacíonKodama.SetActive(true);
            exclamacíonKodama.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            kodama1.transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
            kodama.jugadorCerca = false;
            player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().stopPlayer();
            //parar la animación aqui.
            StartCoroutine("esperarAnimacion");
        }

    }

    IEnumerator llegadaLord()
    {
        particulasLord.SetActive(true);
        yield return new WaitForSeconds(1f);
        lordOfTheForest.SetActive(true);
        particulasLord.SetActive(false);
    }

    IEnumerator esperarAnimacionLord()
    {
        yield return new WaitForSeconds(2f);
        particulasLord.SetActive(true);
        exclamacionLord.SetActive(false);
        yield return new WaitForSeconds(2f);
        lordOfTheForest.SetActive(false);
        particulasLord.SetActive(false);
        eventoLordOfTheForest = false;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().continueMovement();
    }
}
