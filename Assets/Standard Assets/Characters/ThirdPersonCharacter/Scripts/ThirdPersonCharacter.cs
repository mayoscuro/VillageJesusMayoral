using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_JumpPower = 12f;
        [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;

        Rigidbody m_Rigidbody;
        Animator m_Animator;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;

        public GameObject bowGameobject;
        public bool arcoActivo;
        public GameObject bowParticleSystem;
        public GameObject bowParticlesDispair;
        public bool apuntando;

        public GameObject mirillaArco;

        public bool hayFlechas;
        public bool disparado;
        public bool enMenu;

        public bool recolectando = false;

        public bool esquivar  = false;

        public FreeLookCam camaraScript;

        

        public bool runing;
        //Auxiliares para guardar la velocidad linear y angular para volver a darselos al personaje despues de que frene al recoger objetos o interactuar con NPCs:
        private Vector3 velocity;
        private Vector3 angularVelocity;

        void Start()
        {


            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ /*| RigidbodyConstraints.FreezePositionY*/;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        public void stopPlayer() {
            velocity = m_Rigidbody.velocity;
            angularVelocity = m_Rigidbody.angularVelocity;
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
            m_Rigidbody.useGravity = false;
            m_Rigidbody.isKinematic = true;
        }

        public void continueMovement() {
            m_Rigidbody.isKinematic = false;
            m_Rigidbody.velocity = velocity;
            m_Rigidbody.angularVelocity = angularVelocity;
            m_Rigidbody.useGravity = true;
        }

        private void Update()
        {
            if (!recolectando)
            {
                if (Input.GetKeyDown(KeyCode.Q)) {
                    m_Animator.SetTrigger("Esquivar");
                    esquivar = true;
                }

                if (esquivar && Input.GetKey(KeyCode.W)) {//Hacer resto.
                    
                    transform.Translate(transform.forward * 0.15f); // evasion = full speed forward
                    //moveSpeed = evadeDistance / evadeTime;
                }

                if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Esquivar") && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    esquivar = false;
                }


                if (Input.GetKeyDown(KeyCode.E) && !arcoActivo)
                {
                    arcoActivo = true;
                    bowParticleSystem.SetActive(true);

                    StartCoroutine("waitToActivateTheOrb");
                }
                else if (bowGameobject.activeInHierarchy && arcoActivo && Input.GetKeyDown(KeyCode.E))
                {
                    arcoActivo = false;
                    bowParticlesDispair.SetActive(true);
                    StartCoroutine("waitToDesapearBow");
                }

                if (m_Animator.GetCurrentAnimatorStateInfo(1).IsName("SacarArcoCoger") /*|| m_Animator.GetCurrentAnimatorStateInfo(2).IsName("SacarArcoCoger")*/)
                {
                    bowGameobject.SetActive(true);

                    //m_Animator.SetBool("BowInScene", true);
                }

                if (m_Animator.GetCurrentAnimatorStateInfo(1).IsName("SacarArcoPegar") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SacarArcoPuño"))
                {
                    bowParticleSystem.SetActive(false);
                    // Debug.Log("QUITANDO LOS EFECTOS");
                }

                if (Input.GetMouseButtonDown(0) && arcoActivo && hayFlechas  && !enMenu)
                {
                   
                    

                }
                if (Input.GetMouseButton(0) && arcoActivo && hayFlechas && !enMenu)
                {
                    m_Animator.SetBool("Apuntando", true);
                    apuntando = true;
                    //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, -Camera.main.transform.rotation.w);
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                    //enabledIK = true;
                    camaraScript.empezandoAApuntar = true;
                    //camaraScript.apuntando = true;
                    

                }
                else if (Input.GetMouseButtonUp(0) && arcoActivo && hayFlechas && !enMenu || Input.GetMouseButtonUp(0) && arcoActivo && !hayFlechas && !enMenu)
                {
                    disparado = true;

                    m_Animator.SetBool("Apuntando", false);
                    //bowGameobject.transform.Rotate(0, 0, 25);
                    //Vector3 newPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z - 5);
                    apuntando = false;
                    camaraScript.apuntando = false;
                    camaraScript.dejandoDeApuntar = true;
                    
                    //enabledIK = false;
                }
                else {
                    camaraScript.dejandoDeApuntar = false;
                    camaraScript.apuntando = false;
                    camaraScript.empezandoAApuntar = false;
                }

                if (arcoActivo)//Activar mirilla solo si el arco está activo.
                {
                    mirillaArco.SetActive(true);
                }
                else
                {
                    mirillaArco.SetActive(false);
                }
            }
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

        IEnumerator waitToDesapearBow() {
            yield return new WaitForSeconds(1f);
            m_Animator.SetTrigger("BowEnd");
            bowParticlesDispair.SetActive(false);
            bowGameobject.SetActive(false);
        }

        IEnumerator waitToActivateTheOrb()
        {
            yield return new WaitForSeconds(1);
            m_Animator.SetTrigger("BowIdlee");
        }


        public void Move(Vector3 move, bool crouch, bool jump)
		{
            if (!recolectando)
            {
                // convert the world relative moveInput vector into a local-relative
                // turn amount and forward amount required to head in the desired
                // direction.
                if (move.magnitude > 1f) move.Normalize();
			    move = transform.InverseTransformDirection(move);
			    CheckGroundStatus();
			    move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			    m_TurnAmount = Mathf.Atan2(move.x, move.z);
			    m_ForwardAmount = move.z;

			    ApplyExtraTurnRotation();

			    // control and velocity handling is different when grounded and airborne:
			    if (m_IsGrounded)
			    {
				    //HandleGroundedMovement(crouch, jump);
			    }
			    else
			    {
				    HandleAirborneMovement();
			    }
                HandleAirborneMovement();

                //ScaleCapsuleForCrouching(crouch);
                //PreventStandingInLowHeadroom();

                // send input and other state parameters to the animator

                UpdateAnimator(move);
            }
		}


		/*void ScaleCapsuleForCrouching(bool crouch)
		{
			if (m_IsGrounded && crouch)
			{
				if (m_Crouching) return;
				m_Capsule.height = m_Capsule.height / 2f;
				m_Capsule.center = m_Capsule.center / 2f;
				m_Crouching = true;
			}
			else
			{
				Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
				float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
				if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
				{
					m_Crouching = true;
					return;
				}
				m_Capsule.height = m_CapsuleHeight;
				m_Capsule.center = m_CapsuleCenter;
				m_Crouching = false;
			}
		}*/

		void PreventStandingInLowHeadroom()
		{
            // prevent standing up in crouch-only zones
            if (!recolectando)
            {
                if (!m_Crouching)
                {
                    Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                    float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                    //if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                    //{
                    //m_Crouching = true;
                    //}
                }
            }
		}


		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			//m_Animator.SetBool("Crouch", m_Crouching);
			m_Animator.SetBool("OnGround", m_IsGrounded);
			/*if (!m_IsGrounded)
			{
				m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
			}*/

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
			if (m_IsGrounded)
			{
				//m_Animator.SetFloat("JumpLeg", jumpLeg);
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (m_IsGrounded && move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
                runing = true;
			}
			else
			{
				// don't use that while airborne
				m_Animator.speed = 1;
			}
		}


		void HandleAirborneMovement()
		{
            if (!recolectando)
            {
                // apply extra gravity from multiplier:
                Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier * 5) - Physics.gravity;
                m_Rigidbody.AddForce(extraGravityForce);

                m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
            }
		}


		void ApplyExtraTurnRotation()
		{
            // help the character turn faster (this is in addition to root rotation in the animation)
            if (!recolectando)
            {
                if (!Input.GetMouseButton(0))
                {
                    float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
                    transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
                }
            }
		}


		public void OnAnimatorMove()
		{
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (!recolectando)
            {
                if (m_IsGrounded && Time.deltaTime > 0)
                {
                    Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

                    // we preserve the existing y part of the current velocity.
                    v.y = m_Rigidbody.velocity.y;
                    m_Rigidbody.velocity = v;
                }
            }
		}


		void CheckGroundStatus()
		{
            //m_IsGrounded = true;
            // increased offset from 0.1f to 0.5f to place origin of raycast further inside the character
            float rayCastOriginOffset = 0.5f;

            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * rayCastOriginOffset), transform.position + (Vector3.up * rayCastOriginOffset) + (Vector3.down * m_GroundCheckDistance));
#endif
            // rayCastOriginOffset is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * rayCastOriginOffset), Vector3.down, out hitInfo, (rayCastOriginOffset + m_GroundCheckDistance)))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
                m_Rigidbody.AddForce(0,-10,0);
            }
        }
	}
}
