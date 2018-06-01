using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityStandardAssets.Utility
{
    public class ActivateTrigger : MonoBehaviour
    {

        public enum Mode
        {
            Trigger = 0,    
            Replace = 1,    
            Activate = 2,   
            Enable = 3,    
            Animate = 4,    
            Deactivate = 5  
        }

        public Mode action = Mode.Activate;         
        public GameObject target;                      
        public GameObject source;
        public int triggerCount = 1;
        public bool repeatTrigger = false;
        private bool dentro;
        public GameObject character;
        public GameObject Interactuar;


        private void DoActivateTrigger()
        {
            if (dentro && Input.GetKeyDown(KeyCode.Space) && !target.activeInHierarchy) {
                character.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
                
                triggerCount--;
                    Object currentTarget = target ?? gameObject;
                    Behaviour targetBehaviour = currentTarget as Behaviour;
                    GameObject targetGameObject = currentTarget as GameObject;
                    if (targetBehaviour != null)
                    {
                        targetGameObject = targetBehaviour.gameObject;
                    }

                    switch (action)
                    {
                        case Mode.Trigger:
                            if (targetGameObject != null)
                            {
                                Debug.Log("Dentro*2");
                                targetGameObject.BroadcastMessage("DoActivateTrigger");
                            }
                            break;
                        case Mode.Replace:
                            if (source != null)
                            {
                                if (targetGameObject != null)
                                {
                                    Instantiate(source, targetGameObject.transform.position,
                                                targetGameObject.transform.rotation);
                                    DestroyObject(targetGameObject);
                                }
                            }
                            break;
                        case Mode.Activate:
                            if (targetGameObject != null)
                            {
                                targetGameObject.SetActive(true);
                            }
                            break;
                        case Mode.Enable:
                            if (targetBehaviour != null)
                            {
                                targetBehaviour.enabled = true;
                            }
                            break;
                        case Mode.Animate:
                            if (targetGameObject != null)
                            {
                                targetGameObject.GetComponent<Animation>().Play();
                            }
                            break;
                        case Mode.Deactivate:
                            if (targetGameObject != null)
                            {
                                targetGameObject.SetActive(false);
                            }
                            break;
                    }
                }
        }

        private void Update()
        {
            DoActivateTrigger();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") {
                dentro = true;
                Interactuar.SetActive(true);
            }
            
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.tag == "Player")
            {
                dentro = false;
                Interactuar.SetActive(false);
            }
        }
    }
}
