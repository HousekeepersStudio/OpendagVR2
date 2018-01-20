using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Target : Entity {
    List<GameObject> enemies = new List<GameObject>();
    float timeToWait = 3.0f;
    public Target() : base("MainTower", 130) {}

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (this.curHealth <= 0)
            {
                PlayerPrefs.SetInt("Score", GameObject.Find("Points").GetComponent<Points>().GetScore());
                WaitAndPlaySound(timeToWait);
                LoadKeyboardScene();
            }
        }
        else
        {
            if (this.curHealth <= 0)
            {
                Destroy(gameObject);
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<StandardEnemy>().mainTowerAttack = false;
                }
            }
        }
    }

    private void LoadKeyboardScene()
    {
        SceneManager.LoadScene(4);
    }

    private IEnumerable WaitAndPlaySound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // Add sound for gameover
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter " + other.tag);
        if(other.tag.Contains("Enemy"))
        {
            enemies.Add(other.transform.parent.parent.gameObject);
            StandardEnemy se = other.transform.parent.parent.GetComponent<StandardEnemy>();
            se.mainTowerAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit " + other.tag);
        if (other.tag.Contains("Enemy"))
        {
            StandardEnemy se = other.transform.parent.parent.GetComponent<StandardEnemy>();
            se.mainTowerAttack = false;
        }
    }
}
