using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PolluelosReyIA : MonoBehaviour {
    public Transform centro;
    private NavMeshAgent nav;
    private Animator anim;

    public GameObject plumaPolluelo;
	// Use this for initialization
	void Start () {
        
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        nav.SetDestination(centro.position);

        InvokeRepeating("soltarPluma", 50,50);
    }
	
	// Update is called once per frame
	void Update () {
        if (nav.remainingDistance < Random.Range(0.5f, 1.5f)) {
            Vector3 newPosition = RandomNavmeshLocation(Random.Range(20,30));
            nav.SetDestination(newPosition);
        }
		
	}

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += centro.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
            /*finalPosition.x += Random.Range(0, 10);
            finalPosition.z += Random.Range(0, 20);*/
        }
        return finalPosition;
    }
    private void soltarPluma() {
        if (Random.value > 0.7f) {
            GameObject plumaPollueloObj = Instantiate(plumaPolluelo);
            plumaPollueloObj.transform.position = transform.position;
        }
    }
}
