using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    float closeDoor = 15;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        closeDoor -= Time.deltaTime;
        Vector3 door = GameObject.Find("openDoor").transform.position;
        if(door.y >= -9 && closeDoor > 0)
            transform.Translate(Vector3.forward * Time.deltaTime);
        if(closeDoor <= 0)
            if(door.y <= 3.5)
                transform.Translate(Vector3.back * Time.deltaTime);

    }
}
