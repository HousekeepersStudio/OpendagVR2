using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StoryScene : MonoBehaviour
{
    private SoundController controller;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = this.GetComponentInParent<AudioSource>();
        controller = new SoundController();
        controller.PlaySound("Story", audioSource, 0.03f, false);
        Debug.Log(this.gameObject.name);
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
	}
}
