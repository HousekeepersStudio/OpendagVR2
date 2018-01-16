﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Target : Entity {
    bool EnemyInside = false;
    float timeToWait = 3.0f;

    public Target() : base("MainTower", 1000){}

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (this.curHealth <= 0)
            {
                WaitAndPlaySound(timeToWait);
                ResetGame();

                LoadStartScene();
            }
        }
    }

    private void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    private void ResetGame()
    {
        PlayerPrefs.DeleteAll();
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
