using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introWave : MonoBehaviour
{

    private bool enemySpawned = false;
    private bool enemyDied = false;
    private bool bowIsInHand = false;
    private bool bowIsSpawned = false;
    private bool menuButtonPressed = false;
    private bool timeToShine = false;
    private SoundController controller;
    private AudioSource audioSource;
    List<GameObject>  enemies;
    Vector3 spawnLocation;

    // set this variable to false to disable all the console logs on this page 
    // or set this to true to enable all the console logs on this page
    public bool consoleLogs = true;
    public SteamVR_TrackedController steamVR;
    public GameObject WaveController;
    public GameObject leftController;
    public GameObject rightController;
    public Renderer ImageSpot;
    public GameObject enemyPrefab;

    // Textures
    public Texture triggerButton;
    public Texture touchpadLeft;
    public Texture touchpadRight;
    public Texture menuButton;

    // Skip tutorial stuff
    public int howLongToHoldToSkip = 250;
    private int howLongWeAreHolding = 0;


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

    IEnumerator Story()
    {
        yield return new WaitForSeconds(1.5f);
        controller.PlaySound(2, audioSource);
        
        yield return new WaitForSeconds(6);
        // show image
        ImageSpot.material.mainTexture = touchpadLeft;
        ImageSpot.gameObject.SetActive(true);
        yield return new WaitUntil(() => bowIsSpawned == true);
        ImageSpot.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        controller.PlaySound(3, audioSource);
        ImageSpot.material.mainTexture = triggerButton;
        yield return new WaitForSeconds(2.0f);
        ImageSpot.gameObject.SetActive(true);
        yield return new WaitUntil(() => bowIsInHand == true);
        ImageSpot.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        controller.PlaySound(4, audioSource);
        yield return new WaitForSeconds(9.0f);
        controller.PlaySound(5, audioSource);
        yield return new WaitUntil(() => enemyDied == true);
        yield return new WaitForSeconds(0.5f);
        controller.PlaySound(6, audioSource);
        ImageSpot.material.mainTexture = menuButton;
        yield return new WaitForSeconds(3.0f);
        ImageSpot.gameObject.SetActive(true);
        timeToShine = true;
        yield return new WaitUntil(() => menuButtonPressed == true);
        ImageSpot.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        controller.PlaySound(7, audioSource);
        //WaveController.gameObject.SetActive(true);

    }

    void Update()
    {
        if (GameObject.Find("IntroSceneEnemy") != null && enemySpawned == true && GameObject.Find("IntroSceneEnemy").GetComponent<StandardEnemy>().GetCurrentHealth() <= 0)
        {
            DebugLog("Enemy died", consoleLogs);
            enemyDied = true;
        }

        if (leftController.transform.childCount > 2 || rightController.transform.childCount > 3)
        {
            bowIsInHand = true;
        }

        if (steamVR.menuPressed && timeToShine)
        {
            menuButtonPressed = true;
        }

        if (steamVR.gripped)
        {
            
            if(howLongWeAreHolding < howLongToHoldToSkip)
            {
                howLongWeAreHolding++;
            }
            else
            {
                DebugLog("Wave Skipped", consoleLogs);
                StartCoroutine(PlayBadum());
            }
        }
    }

    IEnumerator PlayBadum()
    {
        
        controller.PlaySound(12, audioSource);
        yield return new WaitForSeconds(3.0f);
        WaveController.gameObject.SetActive(true);
    }




    public void ExternalInput(string input)
    {
        if(input == "BowHasBeenSpawned")
        {
            bowIsSpawned = true;
        }
    }

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