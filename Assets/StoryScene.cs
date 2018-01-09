using UnityEngine.SceneManagement;
using UnityEngine;

public class StoryScene : MonoBehaviour {
    void Update () {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        bool targetsAlive = false;
        if(targets != null)
        {
            foreach (GameObject target in targets)
            {
                if (target.activeSelf)
                    targetsAlive = true;
            }

            if (!targetsAlive)
            {
                SceneManager.LoadScene(2);
            }
        }
	}
}
