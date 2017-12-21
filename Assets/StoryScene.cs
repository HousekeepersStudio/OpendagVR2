using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScene : MonoBehaviour {
    void Update () {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        bool targetsAlive = false;
        foreach(GameObject target in targets)
        {
            if (target.activeSelf)
                targetsAlive = true;
        }
	}
}
