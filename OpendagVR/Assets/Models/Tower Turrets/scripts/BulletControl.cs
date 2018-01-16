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
        if (target.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
            target.gameObject.GetComponent<StandardEnemy>().TakeDamage(curTurret.damage);
            
        }
    }
}
