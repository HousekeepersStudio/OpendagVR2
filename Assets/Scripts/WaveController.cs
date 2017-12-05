using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    List<Vector3> spawnLocations;
    List<GameObject> enemies;
    public GameObject enemyPrefab;
    public float waitingTime;
    int waveNr = 0;
    int enemiesCount = 3;
    float enemyMultiply = 1.09f;
    float enemyLevelMultiply = 1.01f;
    bool waveInitialized = false;

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
        if (enemies.Count == 0 && !waveInitialized)
        {
            waveNr++;
            InitWave();
        }

        if(enemies.Count == 0 && waveInitialized)
        {
            Debug.Log("Wave Won!");
            StartCoroutine(WaveWaiter(waitingTime));
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
            int level = rnd.Next(1, (int)(waveNr * enemyLevelMultiply));
            GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnLocations[rnd.Next(0, spawnLocations.Count)], new Quaternion(0, 0, 0, 0));
            enemy.GetComponent<StandardEnemy>().SetLevel(level, enemy.GetComponent<StandardEnemy>().GetNavMeshAgent());
            enemy.name = "Enemy[" + i + "]";
            enemies.Add(enemy);
            yield return new WaitForSeconds(1f);
        }
        waveInitialized = true;
    }

    IEnumerator WaveWaiter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        waveInitialized = false;
    }
}
