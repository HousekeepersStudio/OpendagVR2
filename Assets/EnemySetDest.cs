using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetDest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<NavMeshAgent>().SetDestination(GameObject.FindGameObjectWithTag("Target").transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
