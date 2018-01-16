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
    private bool bowIsInHand = false;
    private SoundController controller;
    private AudioSource audioSource;
    List<GameObject>  enemies;
    public GameObject WaveController;
    public GameObject cameraRig;
    public GameObject enemyPrefab;
    Vector3 spawnLocation;

    void Awake()
    {
        audioSource = this.GetComponentInParent<AudioSource>();
        controller = new SoundController();
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
        yield return new WaitForSeconds(1.5f);
        controller.PlaySound(2, audioSource);
        
        yield return new WaitForSeconds(6);
        // show image
        yield return new WaitUntil(() => enemyDied == true);
        // WaitFor bow is child of controller
        controller.PlaySound(3, audioSource);
        DebugLog("Waituntil enemydied started", consoleLogs);
        yield return new WaitUntil(() => enemyDied == true);
        DebugLog("Waituntil enemydied ended", consoleLogs);
        WaveController.gameObject.SetActive(true);

    }

    void Update()
    {
        if (GameObject.Find("IntroSceneEnemy") != null && enemySpawned == true && GameObject.Find("IntroSceneEnemy").GetComponent<StandardEnemy>().GetCurrentHealth() <= 0)
        {
            DebugLog("Enemy died", consoleLogs);
            enemyDied = true;
        }

        if (cameraRig.transform.Find("Gun").gameObject != null)
            bowIsInHand = true;
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