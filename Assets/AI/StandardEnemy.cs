using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StandardEnemy : Enemy {
    NavMeshAgent nav;
    static string type = "Standard";
    static int maxHealth = 100;
    static int damage = 2;
    bool mainTowerAttack = false;

    public StandardEnemy() : base(type, maxHealth, damage)
    {
    }

    private void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
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
            nav.ActivateCurrentOffMeshLink(true);
           // nav.mes
            nav.enabled = true;
            if(nav.isOnNavMesh && nav.isStopped)
                MoveTo(GameObject.FindGameObjectWithTag("Target"), nav);
            else if (!nav.isOnNavMesh)
            {
                NavMeshHit closestHit;
                NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas);
                transform.position = closestHit.position;
                nav.enabled = false;
                nav.enabled = true;
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
}
