using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity {

    protected static float healthMultiplier = 1.2f;
    protected static float damageMultiplier = 1.3f;
    protected static float speedMultiplier = 1.001f;
    protected static float maxSpeed = 1.8f;
    

    public Enemy(string type, int maxHealth, int damage, int level) : base(type, maxHealth, damage, level)
    {

    }

    public void MoveTo(GameObject target, NavMeshAgent pathFinder)
    {
        pathFinder.isStopped = false;
        pathFinder.SetDestination(target.transform.position);
        //ani.SetBool("isWalking", true);
    }

    public void StopMove(NavMeshAgent pathFinder)
    {
        pathFinder.isStopped = true;
        //ani.SetBool("isWalking", false);
    }

    protected IEnumerator DieT(GameObject enemy)
    {
        transform.SetPositionAndRotation(new Vector3(0, -20, 0), new Quaternion(0, 0, 0, 0));
        yield return new WaitForSeconds(1);
        GameObject.Find("WaveController").GetComponent<WaveController>().RemoveFromWave(enemy.name);
        Debug.Log("Enemy Died");
        Destroy(enemy);
    }
    protected IEnumerator EnemyAttackTower(bool mainTowerAttack, Collider tower)
    {
        if (mainTowerAttack)
        {
            Attack(tower.gameObject);
            yield return new WaitForSeconds(1);
            StartCoroutine(EnemyAttackTower(mainTowerAttack, tower));
        }
    }

    public void SetLevel(int level, NavMeshAgent pathFinder)
    {
        this.level = level;
        if((pathFinder.speed * speedMultiplier) * this.level <= maxSpeed)
            pathFinder.speed = ((pathFinder.speed * speedMultiplier) * this.level);
        maxHealth = (int)((maxHealth * healthMultiplier) * this.level);
        curHealth = maxHealth;
        damage = (int)((damage * damageMultiplier) * this.level);
    }
}
