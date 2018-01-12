using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introWave : MonoBehaviour {

    List<GameObject> enemies;
    public GameObject WaveController;
    public GameObject enemyPrefab;
    Vector3 spawnLocation;

    void Awake()
    {
        enemies = new List<GameObject>();
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("IntrowaveSpawn"))
        {
            spawnLocation = spawn.transform.position;
        }
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnLocation, new Quaternion(0, 0, 0, 0));
        StandardEnemy enemyScript = enemy.GetComponent<StandardEnemy>();
        enemyScript.SetLevel(1, enemy.GetComponent<StandardEnemy>().GetNavMeshAgent(), true);
        enemy.name = "IntroSceneEnemy";
        enemies.Add(enemy);
        StartCoroutine(enemyScript.TurnOnNavMeshAgent());

        StartCoroutine(Story());
    }

    IEnumerator Story()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("End Reached");
        WaveController.gameObject.SetActive(true);
    }
}
