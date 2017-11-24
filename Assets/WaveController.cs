using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    List<Vector3> spawnLocations;
    List<GameObject> enemies;
    public GameObject enemyPrefab;
    int waveNr = 0;
    int enemiesCount = 6;
    float enemyMultiply = 1.2f;

    void Awake () {
        spawnLocations = new List<Vector3>();
        foreach(GameObject spawn in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            spawnLocations.Add(spawn.transform.position);
        }
        InitWave();

    }
	
	// Update is called once per frame
	void Update () {
        if (enemies.Count == 0)
        {
            Debug.Log("Wave Won!");
            waveNr++;
            InitWave();
        }
	}

    public void RemoveFromWave(string name)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name == name)
                enemies.RemoveAt(i);
        }
    }

    void InitWave()
    {
        for (int i = 0; i < waveNr; i++)
            enemiesCount = (int)(enemiesCount * enemyMultiply);
        enemies = new List<GameObject>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < enemiesCount; i++)
        {
            System.Random rnd = new System.Random();
            GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnLocations[rnd.Next(0, spawnLocations.Count)], new Quaternion(0, 0, 0, 0));
            enemy.name = "Enemy[" + i + "]";
            enemies.Add(enemy);
            yield return new WaitForSeconds(2f);
        }
    }
}
