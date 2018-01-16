using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    Animator animator;
    bool mainTowerAttack = false;

    public StandardEnemy() : base(enemyType, health, enemyDamage, startLevel)
    {

    }

    private void Awake()
    {
        healthBar = this.transform.Find("HealthBarCanvas").Find("HealthBG").Find("HealthBar").GetComponent<Image>();
        agent = GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            curHealth -= 100;
		if (curHealth / maxHealth <= 0.5F) 
		{
			animator.SetTrigger ("Hurt");
		}
        if(curHealth <= 0)
        {
            Die();
        }
        else
        {
            if (agent.enabled && agent.isOnNavMesh)
                MoveTo(GameObject.FindGameObjectWithTag("Target"));

            if (!agent.isOnNavMesh)
            {
                agent.enabled = false;
                agent.enabled = true;
                NavMeshHit closesthit;
                NavMesh.SamplePosition(gameObject.transform.position, out closesthit, 500f, NavMesh.AllAreas);
                transform.position = closesthit.position;
                agent.isStopped = false;
                MoveTo(GameObject.FindGameObjectWithTag("Target"));
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
            StopMove();
            animator.SetTrigger("Attack");
            mainTowerAttack = true;
            StartCoroutine(EnemyAttackTower(mainTowerAttack, other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            animator.SetTrigger("Walk");
            mainTowerAttack = false;
        }
    }
}
