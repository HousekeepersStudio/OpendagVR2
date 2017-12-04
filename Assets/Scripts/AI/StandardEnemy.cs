using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StandardEnemy : Enemy {
    NavMeshAgent nav;
    static string enemyType = "Standard";
    static int baseHealth = 100;
    static int baseDamage = 2;
    static float sHealthMultiplier = healthMultiplier;
    static float sDamageMultiplier = damageMultiplier;

    static int startLevel = 1;
    static int health = (int)((baseHealth * sHealthMultiplier) * startLevel);
    static int enemyDamage = (int)((baseDamage * sDamageMultiplier) * startLevel);
    bool mainTowerAttack = false;

    public StandardEnemy() : base(enemyType, health, enemyDamage, startLevel)
    {
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        MoveTo(GameObject.FindGameObjectWithTag("Target"), nav);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            curHealth -= 100;

        if(curHealth <= 0)
        {
            Die();
        }
        else
        {
            if (!nav.isOnNavMesh)
            {
                nav.enabled = false;
                nav.enabled = true;
                NavMeshHit closesthit;
                NavMesh.SamplePosition(gameObject.transform.position, out closesthit, 500f, NavMesh.AllAreas);
                transform.position = closesthit.position;
                nav.isStopped = false;
                MoveTo(GameObject.FindGameObjectWithTag("Target"), nav);
            }
        }
    }

    private void Die()
    {
        StartCoroutine(DieT(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            StopMove(nav);
            mainTowerAttack = true;
            StartCoroutine(EnemyAttackTower(mainTowerAttack, other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            mainTowerAttack = false;
        }
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return nav;
    }
}
