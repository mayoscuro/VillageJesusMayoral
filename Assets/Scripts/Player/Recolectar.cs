using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolectar : MonoBehaviour {
	public InventorySystem inventory;
    public GameObject recoger;
    public Nodo nodoPlantes;
    public Nodo nodoFruta;
    private Animator playerAnim;
    private bool touchingRecolectable = false;
    private Collider other = null;
    private bool isRecolecting = false;
    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter playerController;

    private int numeroRecogidos;

    private float movespeedAux;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerController = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hierba")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "HierbaDeMontaña")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "HierbaCurativa")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Madera")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Bellota")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Piña")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Insecto")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Mineral")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "LoboObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "ZorroObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {

            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "CiervoObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Pluma")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Flecha" && other.transform.GetChild(0).GetComponent<ArrowTrigger>().recogible)
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
  
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hierba")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "HierbaDeMontaña")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "HierbaCurativa")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Madera")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Bellota")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Piña")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Insecto")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Mineral")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "LoboObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "ZorroObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {

            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "CiervoObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Pluma")
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
        else if (other.tag == "Flecha" && other.transform.GetChild(0).GetComponent<ArrowTrigger>().recogible)
        {
            recoger.SetActive(true);
            touchingRecolectable = true;
            this.other = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Hierba")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "HierbaDeMontaña")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "HierbaCurativa")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Madera")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Bellota")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Piña")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Insecto")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Mineral")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "LoboObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "ZorroObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {

            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "CiervoObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Pluma")
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
        else if (other.tag == "Flecha" && other.transform.GetChild(0).GetComponent<ArrowTrigger>().recogible)
        {
            recoger.SetActive(false);
            touchingRecolectable = false;
            this.other = null;
        }
    }

    IEnumerator waitForAnimation() {
        yield return new WaitForSeconds(1.9f);
        playerController.continueMovement();
        touchingRecolectable = false;
        recoger.SetActive(false);
        playerController.recolectando = isRecolecting = false;
        playerAnim.SetBool("Recoger", false);
       
    }

    private void Update()
    {


        if (Input.GetButtonUp("Recolectar") && touchingRecolectable && this.other != null && !isRecolecting && !playerController.bowGameobject.activeInHierarchy && !playerController.arcoActivo && other.tag !="Flecha")
        {
            isRecolecting = true;
            playerController.recolectando = isRecolecting;
            playerController.stopPlayer();
            // Debug.Log("Listo");

            if (other.tag == "Hierba")
            {
                inventory.AñadirItem(2);
                if (Random.value > 0.8) {//En las plantas hay baja probabilidad de coger algun bicho.
                    inventory.AñadirItem(6);
                }
                //Debug.Log("dENTRO");
                nodoPlantes.numeroVivos -= 1;
                playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "HierbaDeMontaña")
            {
                inventory.AñadirItem(1);
                if (Random.value > 0.8)
                {//En las plantas hay baja probabilidad de coger algun bicho.
                    inventory.AñadirItem(6);
                }
                nodoPlantes.numeroVivos -= 1;
                playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "HierbaCurativa")
            {
                inventory.AñadirItem(0);
                if (Random.value > 0.8)
                {//En las plantas hay baja probabilidad de coger algun bicho.
                    inventory.AñadirItem(6);
                }
                nodoPlantes.numeroVivos -= 1;
                playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "Madera")
            {
                inventory.AñadirItem(3);
                playerAnim.SetBool("Recoger", true);
                other.gameObject.SetActive(false);
            }
            else if (other.tag == "Bellota")
            {
                inventory.AñadirItem(4);
                nodoFruta.numeroVivos -= 1;
                Debug.Log("Bellotazo"); playerAnim.SetBool("Recoger", true);
                other.gameObject.SetActive(false);
            }
            else if (other.tag == "Piña")
            {
                nodoFruta.numeroVivos -= 1;
                inventory.AñadirItem(5); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "Insecto")
            {
                inventory.AñadirItem(6); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "Mineral")
            {
                inventory.AñadirItem(7); playerAnim.SetBool("Recoger", true);
                other.gameObject.SetActive(false);
            }
            else if (other.tag == "LoboObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
            {
                Debug.Log("Lobo");

                inventory.AñadirItem(Random.Range(8, 10));//Random entre las id de losobjetos que puede tener el lodo
                playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "ZorroObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
            {

                Debug.Log("Zorro");
                other.gameObject.GetComponentInParent<Animal>().numObjetos--;
                inventory.AñadirItem(Random.Range(15, 18));//Random entre las id de losobjetos que puede tener el lodo
                playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "CiervoObjetoPadre" && other.gameObject.GetComponentInParent<Animal>().muerto && 0 < other.gameObject.GetComponent<Animal>().numObjetos)
            {
                other.gameObject.GetComponentInParent<Animal>().numObjetos--;
                Debug.Log("Ciervo reocgido");
                inventory.AñadirItem(Random.Range(19, 20));//Random entre las id de losobjetos que puede tener el lodo
                playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "CarneLobo")
            {
                inventory.AñadirItem(8); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "PielLobo")
            {
                inventory.AñadirItem(9); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "HuesosLobo")
            {
                inventory.AñadirItem(10); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "CarneFenrir")
            {
                inventory.AñadirItem(11); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "PielFenrir")
            {
                inventory.AñadirItem(12); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "HuesosFenrir")
            {
                inventory.AñadirItem(13); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "DientesFenrir")
            {
                inventory.AñadirItem(14); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "CarnePequeñaZorro")
            {
                inventory.AñadirItem(15); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "HuesosZorro")
            {
                inventory.AñadirItem(16); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "ColaZorro")
            {
                inventory.AñadirItem(17); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "PielZorro")
            {
                inventory.AñadirItem(18); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "PielCiervo")
            {
                inventory.AñadirItem(19); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "CarneCiervo")
            {
                inventory.AñadirItem(20); playerAnim.SetBool("Recoger", true);
            }
            else if (other.tag == "Pluma")
            {
                inventory.AñadirItem(21); playerAnim.SetBool("Recoger", true);
            }
            if (other.tag != "Flecha")
            {
                StartCoroutine("waitForAnimation");
                if (other.tag != "ZorroObjetoPadre" && other.tag != "LoboObjetoPadre" && other.tag != "CiervoObjetoPadre")
                {
                    other.gameObject.SetActive(false);
                }
            }
        }
        else if (Input.GetButtonUp("Recolectar") && touchingRecolectable && this.other != null && !isRecolecting) {
            if (other.tag == "Flecha" && other.transform.GetChild(0).GetComponent<ArrowTrigger>().recogible)
            {
                inventory.AñadirItem(22); //playerAnim.SetBool("Recoger", true);
                Destroy(other.gameObject);
                recoger.SetActive(false);
                playerController.continueMovement();
                playerController.recolectando = isRecolecting = false;
            }
        }
    }

    public bool isHarvesting()
    {
        return isRecolecting;
    }
}
