using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraIntro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 camera = GameObject.Find("[CameraRig]").transform.position;
        if (camera.z <= 5)
            transform.Translate(Vector3.back * Time.deltaTime);
    }
}
