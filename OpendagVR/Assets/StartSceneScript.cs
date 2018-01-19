using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneScript : MonoBehaviour {


    private SoundController controller;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;
    // Use this for initialization


    void Start () {
		
	}

    void Awake()
    {
        audioSource1 = this.GetComponentsInChildren<AudioSource>()[0];
        audioSource2 = this.GetComponentsInChildren<AudioSource>()[1];
        audioSource3 = this.GetComponentsInChildren<AudioSource>()[2];
        controller = new SoundController();

        controller.PlaySound("ambient_sound", audioSource1, 0.4f, true);
        controller.PlaySound("Tutorial_choose_faction", audioSource2);
        controller.PlaySound("Fire_torch", audioSource3, 0.5f, true);


    }

    // Update is called once per frame
    void Update () {
		
	}
}
