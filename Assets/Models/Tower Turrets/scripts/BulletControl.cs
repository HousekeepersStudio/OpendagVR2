using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    private Transform target;
    public GameObject weapon;
    private turret turretscript;
    
    private void Start()
    {
        turretscript = weapon.GetComponent<turret>();
    }

    // Update is called once per frame
    void Update () {
		

      
	}
    void OnCollisionEnter (Collision target)
    {
        if (target.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);

            target.gameObject.GetComponent<enemy>().health -= turretscript.damage;
            
        }
    }
}
