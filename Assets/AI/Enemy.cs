using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity {

    public Enemy(string type, int maxHealth, int damage) : base(type, maxHealth, damage)
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
        Debug.Log("Enemy Died");
        yield return new WaitForSeconds(1);
        GameObject.Find("WaveController").GetComponent<WaveController>().RemoveFromWave(enemy.name);
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
}
