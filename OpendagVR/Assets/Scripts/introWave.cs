using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introWave : MonoBehaviour
{
    // set this variable to false to disable all the console logs on this page 
    // or set this to true to enable all the console logs on this page
    public bool consoleLogs = true;
    private bool enemySpawned = false;
    private bool enemyDied = false;
    List<GameObject>  enemies;
    public GameObject WaveController;
    public GameObject enemyPrefab;
    Vector3           spawnLocation;

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
        DebugLog("Enemy Spawned", consoleLogs);
        enemySpawned = true;
        StartCoroutine(Story());
    }
    // Part one of the story
    IEnumerator Story()
    {
        // Ben.Say(voicelinesIntro);

        yield return new WaitForSeconds(3);
        DebugLog("Waituntil started", consoleLogs);
        yield return new WaitUntil(() => enemyDied == true);
        DebugLog("Waituntil ended", consoleLogs);
<<<<<<< Updated upstream
        WaveController.gameObject.SetActive(true);

=======
        GameObject.Find("Controller (right)").GetComponent<Teleportation>().StartingPosition = GameObject.Find("Tower Prefab (5)").transform.Find("TeleportZone").Find("TeleportPosition").gameObject;
        GameObject.Find("Controller (right)").GetComponent<Teleportation>().animationCanvas = GameObject.Find("TeleportAnimationCanvas");
>>>>>>> Stashed changes
    }

    void Update()
    {
        if (GameObject.Find("IntroSceneEnemy") != null && enemySpawned == true && GameObject.Find("IntroSceneEnemy").GetComponent<StandardEnemy>().GetCurrentHealth() <= 0)
        {
            DebugLog("Enemy died", consoleLogs);
            enemyDied = true;
        }
    }

    //WaveController.gameObject.SetActive(true);

    // Function to disable all the logs in this script
    // and simplify the bracket stuff 
    void DebugLog(string message, bool on)
    {
        if (on)
        {
            message = "[introwave]" + message;
            Debug.Log(message);
        }
    }
}