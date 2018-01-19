using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {
    List<Vector3> spawnLocations;
    List<GameObject> enemies;
    public GameObject ravenPrefab;
    public GameObject dragonPrefab;
    public GameObject serpentPrefab;
    public GameObject vikingPrefab;
    private GameObject enemyPrefab;
    public GameObject introWaveScript;
    public float waitingTime;
    int waveNr = 1;
    int enemiesCount = 3;
    float enemyLevelMultiply = 1.05f;
    bool waveInitialized = false;
    bool timerStarted = false;
    public float enemyMultiply = 3.0f;

    void Awake () {
        introWaveScript.gameObject.SetActive(false);
        enemies = new List<GameObject>();
        spawnLocations = new List<Vector3>();
        foreach(GameObject spawn in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            spawnLocations.Add(spawn.transform.position);
            //Debug.Log(string.Format("Spawn Location Added (X: {0}, Y: {1}, Z: {2})", spawn.transform.position.x, spawn.transform.position.y, spawn.transform.position.z));
        }
        //Debug.Log("Spawn Locations: " + spawnLocations.Count);
        InitEnemy();
        InitWave();

    }
	
	// Update is called once per frame
	void Update () {
        if (enemies.Count == 0 && !waveInitialized)
        {
            waveNr++;
            RandomSoundSelectorBefore();
            InitWave();
        }

        if(enemies.Count == 0 && waveInitialized)
        {
            if (!timerStarted)
                RandomSoundSelectorAfter();
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
        enemiesCount = (int)(waveNr * enemyMultiply) +2;
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
            enemyScript.SetLevel(level, enemy.GetComponent<StandardEnemy>().GetNavMeshAgent(), false);
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

    private void InitEnemy()
    {
        switch (PlayerPrefs.GetString("house"))
        {
            case "serpents":
                enemyPrefab = dragonPrefab;
                break;
            case "vikings":
                enemyPrefab = ravenPrefab;
                break;
            case "dragons":
                enemyPrefab = serpentPrefab;
                break;
            case "ravens":
                enemyPrefab = vikingPrefab;
                break;
        }
    }
    //Check the house, and then decide a random voicelines before the wave
    private int RandomSoundSelectorBefore()
    {
        int number;
        System.Random rnd = new System.Random();
        switch (PlayerPrefs.GetString("house"))
        {
            case "serpents":
                number = rnd.Next(24, 29);
                return number;
            case "vikings":
                number = rnd.Next(34, 39);
                return number;
            case "dragons":
                number = rnd.Next(14, 19);
                return number;
            case "ravens":
                number = rnd.Next(44, 49);
                return number;
        }
        return 0;
    }
    //Check the house, and then decide a random voicelines after the wave
    private int RandomSoundSelectorAfter()
    {
        int number;
        System.Random rnd = new System.Random();
        switch (PlayerPrefs.GetString("house"))
        {
            case "serpents":
                number = rnd.Next(29, 34);
                return number;
            case "vikings":
                number = rnd.Next(39, 44);
                return number;
            case "dragons":
                number = rnd.Next(19, 24);
                return number;
            case "ravens":
                number = rnd.Next(49, 54);
                return number;
        }
        return 0;
    }
}
