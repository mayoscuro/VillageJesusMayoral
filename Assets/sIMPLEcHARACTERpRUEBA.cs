using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sIMPLEcHARACTERpRUEBA : MonoBehaviour {

    //Variables
    private Animator anim;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public GameObject arco;
    public Cloth capa;
    private Vector3 moveDirection = Vector3.zero;
    private Recolectar recolect;
    public GameObject bowParticleSystem;
    private bool arcoActivo = false;
    public GameObject fakeArrow;
    public bool enabledIK;
    public Transform rightHand;
    public Transform endpoint;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        recolect = GetComponent<Recolectar>();
        //fakeArrow.GetComponent<Animator>().SetBool("Apuntar", false);
    }
    void Update()
        {
           
        if (!recolect.isHarvesting()) {
            anim.SetFloat("Movimiento", Input.GetAxis("Vertical"));

            //Debug.Log(Input.GetAxis("Vertical"));
            var x = 0f;
            var z = 0f;
            if (anim.GetFloat("Movimiento") < 0.4)
            {
                x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
                z = -Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;

                capa.stretchingStiffness = 1;
                capa.bendingStiffness = 0.34f;
                capa.useGravity = true;
                capa.damping = 0;
                capa.worldVelocityScale = 0.1f;
                capa.worldAccelerationScale = 1;
                capa.friction = 0.5f;
                capa.collisionMassScale = 0;
                capa.clothSolverFrequency = 120;
                capa.sleepThreshold = 0.1f;
            }
            else if (anim.GetCurrentAnimatorStateInfo(1).IsName("WalkWithBow"))//Si esta caminando con el arco.
            {
                x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
                z = -Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;
            }
            else { 
                x = Input.GetAxis("Horizontal") * Time.deltaTime * 20.0f;
                z = -Input.GetAxis("Vertical") * Time.deltaTime * 20.0f;

            }

           

            transform.Translate(x, 0, -z);

            if (Input.GetKeyDown(KeyCode.E) && !arcoActivo) {
                arcoActivo = true;
                bowParticleSystem.SetActive(true);
                StartCoroutine("waitToActivateTheOrb");
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SacarArcoPuño") || anim.GetCurrentAnimatorStateInfo(2).IsName("SacarArcoPuño"))
            {
                bowParticleSystem.SetActive(false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SacarArcoCoger") || anim.GetCurrentAnimatorStateInfo(2).IsName("SacarArcoCoger"))
            {
                arco.SetActive(true);
                anim.SetBool("BowInScene",true);
            }

            if (Input.GetMouseButtonDown(0)) {
                //arco.transform.Rotate(0, 0, -25);
                Vector3 newPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z +5);
                StartCoroutine(LerpFromTo(Camera.main.transform.localPosition, newPosition, 0.2f));
                
            }
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("Apuntar",true);
                enabledIK = true;

            } else if (Input.GetMouseButtonUp(0)) {
                anim.SetBool("Apuntar",false);
                //arco.transform.Rotate(0, 0, 25);
                Vector3 newPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z - 5);
                StartCoroutine(LerpFromTo(Camera.main.transform.localPosition, newPosition, 0.2f));
                enabledIK = false;
            }

        }
        //camara.transform.LookAt(Input.mousePosition);
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");
        Vector3 lookhere = new Vector3(/*mouseInputY*/0, mouseInputX, 0);
        Camera.main.transform.Rotate(lookhere);
    }

    void OnAnimatorIK(int layerIndex)
    {
       /* if (enabledIK)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, endpoint.rotation);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            anim.SetIKPosition(AvatarIKGoal.RightHand, endpoint.position);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        }*/
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(pos1, pos2, t / duration);
            yield return 0;
        }
        Camera.main.transform.localPosition = pos2;
    }


    IEnumerator waitToActivateTheOrb() {
        yield return new WaitForSeconds(2);
        anim.SetTrigger("SacarArco");
    }
}
