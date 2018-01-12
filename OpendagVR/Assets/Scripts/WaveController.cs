using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    List<Vector3> spawnLocations;
    List<GameObject> enemies;
    public GameObject enemyPrefab;
    public float waitingTime;
    int waveNr = 1;
    int enemiesCount = 3;
    float enemyMultiply = 1.2f;
    float enemyLevelMultiply = 1.05f;
    bool waveInitialized = false;
    bool timerStarted = false;

    void Awake () {
        enemies = new List<GameObject>();
        spawnLocations = new List<Vector3>();
        foreach(GameObject spawn in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            spawnLocations.Add(spawn.transform.position);
            //Debug.Log(string.Format("Spawn Location Added (X: {0}, Y: {1}, Z: {2})", spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z));
        }
        //Debug.Log("Spawn Locations: " + spawnLocations.Count);
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
            if (!timerStarted)
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
        if (timerStarted)
            timerStarted = false;
        enemiesCount = (int)(enemiesCount * (waveNr * enemyMultiply));
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < enemiesCount; i++)
        {
            System.Random rnd = new System.Random();
            int level = 1;
            if ((waveNr / 2 * enemyLevelMultiply) > 1)
                level = rnd.Next(1, (int)(waveNr / 2 * enemyLevelMultiply));
            //Debug.Log(spawnLocations[rnd.Next(0, spawnLocations.Count - 1)]);
            GameObject enemy = GameObject.Instantiate(enemyPrefab, spawnLocations[rnd.Next(0, spawnLocations.Count -1)], new Quaternion(0, 0, 0, 0));
            StandardEnemy enemyScript = enemy.GetComponent<StandardEnemy>();
            enemyScript.SetLevel(level, enemy.GetComponent<StandardEnemy>().GetNavMeshAgent());
            enemy.name = "Enemy[" + i + "]";
            enemies.Add(enemy);
            StartCoroutine(enemyScript.TurnOnNavMeshAgent());
            yield return new WaitForSeconds(1f);
        }
        waveInitialized = true;
    }

    IEnumerator WaveWaiter(float seconds)
    {
        timerStarted = true;
        yield return new WaitForSeconds(seconds);
        waveInitialized = false;
    }
}
