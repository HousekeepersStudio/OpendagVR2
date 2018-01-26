﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StoryScene : MonoBehaviour
{
    private SoundController controller;
    private AudioSource audioSource;


    public SteamVR_TrackedController steamVR;
    // Skip tutorial stuff
    public int howLongToHoldToSkip = 250;
    private int howLongWeAreHolding = 0;


    private void Awake()
    {
        audioSource = this.GetComponentInParent<AudioSource>();
        controller = new SoundController();
        Debug.Log(this.gameObject.name);
        StartCoroutine(Story());
    }

    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        bool targetsAlive = false;
        foreach (GameObject target in targets)
        {
            if (target.activeSelf)
                targetsAlive = true;
        }

        if (targets.Length == 0)
        {
            SceneManager.LoadScene(2);
        }

        if (steamVR.gripped)
        {

            if (howLongWeAreHolding < howLongToHoldToSkip)
            {
                howLongWeAreHolding++;
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }

    }

    IEnumerator Story()
    {
        yield return new WaitForSeconds(1f);
        controller.PlaySound("Story", audioSource);
    }
}
