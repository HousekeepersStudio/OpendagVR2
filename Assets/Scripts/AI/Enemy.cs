using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity {

    protected static float healthMultiplier = 1.2f;
    protected static float damageMultiplier = 1.3f;
    protected static float speedMultiplier = 1.001f;
    protected static float maxSpeed = 10f;
    protected NavMeshAgent agent;
    protected GameObject[] targets;


    public Enemy(string type, float maxHealth, float damage, int level) : base(type, maxHealth, damage, level)
    {

    }

    public void MoveTo(GameObject target)
    {
        if(agent.isStopped)
        {

        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
        
        //Debug.Log(agent.speed);
        //ani.SetBool("isWalking", true);
    }

    public void StopMove()
    {
        agent.isStopped = true;
        //ani.SetBool("isWalking", false);
    }

    protected IEnumerator DieT(GameObject enemy)
    {
        transform.SetPositionAndRotation(new Vector3(0, -20, 0), new Quaternion(0, 0, 0, 0));
        yield return new WaitForSeconds(1);
        GameObject.Find("WaveController").GetComponent<WaveController>().RemoveFromWave(enemy.name);
        Debug.Log("Enemy Died");
        Destroy(enemy);
        Points sn = GameObject.Find("Points").gameObject.GetComponent<Points>();
        sn.AddPoints("Bow");
    }
    protected IEnumerator EnemyAttackTower(bool mainTowerAttack, Collider tower)
    {
        if (mainTowerAttack)
        {
            if(tower.gameObject.GetComponent<Target>().GetCurrentHealth() <= 0)
            {
                tower.transform.position = new Vector3(10000,10000,1000);
                tower.gameObject.SetActive(false);
                mainTowerAttack = false;
                agent.isStopped = false;
                for (int i = 0; i < targets.Length; i++)
                {
                    if(targets[i].activeSelf)
                    {
                        MoveTo(targets[i]);
                        break;
                    }
                }
            }
            else
            {
                Attack(tower.gameObject);
                yield return new WaitForSeconds(1);
                StartCoroutine(EnemyAttackTower(mainTowerAttack, tower));
            }           
        }
    }

    public IEnumerator TurnOnNavMeshAgent()
    {
        yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
    }

    public void SetLevel(int level, NavMeshAgent pathFinder)
    {
        this.level = level;
        if((pathFinder.speed * speedMultiplier) * this.level <= maxSpeed && level > 1)
            pathFinder.speed = ((pathFinder.speed * speedMultiplier) * this.level);
        maxHealth = (float)((maxHealth * healthMultiplier) * this.level);
        curHealth = maxHealth;
        damage = (float)((damage * damageMultiplier) * this.level);
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return agent;
    }
}
