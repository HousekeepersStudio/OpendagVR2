using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    // Use this for initialization
    
    private Transform target;

    [Header("Turret Control")]
    public int turretLvl = Mathf.Min(1);
    public float range = 15f;
    public float fireRate = Mathf.Min(1f);
    private float fireCountDown = 0f;

    [Header("Turret Target")]
    public string enemyTag = "Enemy";

    [Header("Turret Movement")]
    public Transform partToRotate;
    public float turnspeed =  10;

    [Header("Ammo")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float damage = 10;
    public float bulletSpeed = 1300f;

    
    
	void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach( GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance < range)
        {
            target = nearestEnemy.transform;
        }else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;
        
        // target lockon
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;

    }
    void Shoot()
    {
        GameObject temporaryBulletHandler;
        temporaryBulletHandler = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        temporaryBulletHandler.GetComponent<BulletControl>().SetTurret(this);

        Rigidbody TemporaryRigidBody;
        TemporaryRigidBody = temporaryBulletHandler.GetComponent<Rigidbody>();
        TemporaryRigidBody.AddForce(firePoint.transform.forward * bulletSpeed);


       
        //Destroy(temporaryBulletHandler, 10f);
    }

    void UpgradeTurret()
    {
        damage *= 0.05f;
        range *= 0.05f;
        fireRate *= 0.05f;
        turnspeed *= 0.05f;
        turretLvl++;
    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }


   
}
