using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    Turret curTurret;
    public void SetTurret(Turret curTurret)
    {
        this.curTurret = curTurret;
    }

    void OnCollisionEnter (Collision target)
    {
        Debug.Log("collision");
        if (target.gameObject.tag.Contains("Enemy"))
        {
            target.gameObject.GetComponent<StandardEnemy>().TakeDamage(curTurret.damage);
        }
        Destroy(gameObject);
    }
}
