using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    // Use this for initialization
    
    private Vector3 target;

    [Header("Turret Control")]
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
    public int damage = 10;
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
            target = new Vector3(nearestEnemy.transform.position.x, nearestEnemy.transform.position.y + ((range / shortestDistance) + 1f), nearestEnemy.transform.position.z);
        }else
        {
            target = new Vector3(0,0,0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == new Vector3(0,0,0))
            return;
        
        // target lockon
        Vector3 dir = target - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z));
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

    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }


   
}
