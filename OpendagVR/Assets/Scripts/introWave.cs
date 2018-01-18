using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introWave : MonoBehaviour
{

    private bool enemySpawned = false;
    private bool enemyDied = false;
    private bool bowIsInHand = false;
    private bool bowIsSpawned = false;
    private bool TeleporterHasBeenSelected = false;
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
    public Texture touchpadTop;
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
        /*yield return new WaitForSeconds(1.5f);
        // play sound EnemyInfront
        controller.PlaySound(2, audioSource);
        yield return new WaitForSeconds(6);
        // show image touch left
        ImageSpot.material.mainTexture = touchpadLeft;
        // enable the image
        ImageSpot.gameObject.SetActive(true);
        // wait for the bow is spawned
        yield return new WaitUntil(() => bowIsSpawned == true);
        // after bow has been spawned
        // disable the image
        ImageSpot.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        // Play sound pick up bow
        controller.PlaySound(3, audioSource);
        // set image to the triggerbutton image
        ImageSpot.material.mainTexture = triggerButton;
        // wait for ben to finish talking 
        yield return new WaitForSeconds(2.0f);
        // enable the image
        ImageSpot.gameObject.SetActive(true);
        // wait for the bow is child of any of the controllers
        yield return new WaitUntil(() => bowIsInHand == true);
        // disable the image 
        ImageSpot.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        // play sound how to shoot with bow
        controller.PlaySound(4, audioSource);
        // wait for ben to finish talking
        yield return new WaitForSeconds(9.0f);
        // play sound Note headshots
        controller.PlaySound(5, audioSource);
        // Wait for the player has killed the enemy
        yield return new WaitUntil(() => enemyDied == true);
        yield return new WaitForSeconds(0.5f);
        // playsound open menu
        controller.PlaySound(6, audioSource);
        // set the image to the menubutton
        ImageSpot.material.mainTexture = menuButton;
        // wait for ben to finish talking
        yield return new WaitForSeconds(3.0f);
        // enable the image
        ImageSpot.gameObject.SetActive(true);
        // set variable to listen for menu button pressed
        timeToShine = true;
        // Wait for buttonpress
        yield return new WaitUntil(() => menuButtonPressed == true);
        // When button is pressed
        // disable the image
        ImageSpot.gameObject.SetActive(false);*/
        yield return new WaitForSeconds(0.5f);
        // Play sound select build tower
        controller.PlaySound(7, audioSource);
        // wait for ben to finish talking
        yield return new WaitForSeconds(13.0f);
        // set image to towerbuild mode
        ImageSpot.material.mainTexture = touchpadTop;
        // Enable image
        ImageSpot.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        // waitfor tower mode has been selected
        yield return new WaitUntil(() => TeleporterHasBeenSelected == true);
        yield return new WaitForSeconds(0.5f);
        // disable image
        ImageSpot.gameObject.SetActive(false);
        // play sound repair tower
        controller.PlaySound(8, audioSource);
        // wait for ben to finish talking
        yield return new WaitForSeconds(4.0f);
        // wait for player to repair a tower

        //yield return new WaitForSeconds(0.5f);
        //controller.PlaySound(9, audioSource);
        // wait for ben to finish talking
        //ImageSpot.material.mainTexture = touchpadRight;
        //ImageSpot.gameObject.SetActive(false);
        // wait for ben to finish talking s'more
        // wait for player to select teleport mode
        // enable Teleportation script on controllerRight
        //yield return new WaitForSeconds(0.5f);
        // play sound point at tower
        //controller.PlaySound(10, audioSource);
        // wait for ben to finish talking
        // wait for player to teleport to another tower
        //yield return new WaitForSeconds(0.5f);
        //controller.PlaySound(11, audioSource);
        // wait for ben to finish talking
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
                StartCoroutine(skipThis());
            }
        }
    }

    IEnumerator skipThis()
    {
        
        controller.PlaySound(12, audioSource);
        yield return new WaitForSeconds(3.0f);
        WaveController.gameObject.SetActive(true);
    }




    public void ExternalInput(string input)
    {
        if (input == "BowHasBeenSpawned")
            bowIsSpawned = true;

        else if (input == "TeleporterMode")
            TeleporterHasBeenSelected = true;
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