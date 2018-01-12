using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {

    // Use this for initialization
    
    private Transform target;

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
    public float bulletSpeed = 1000f;

    [Header("Turret lvl")]
    [Range(1,3)]
    public int lvl = 1;

    
    
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
        temporaryBulletHandler.GetComponent<BulletControl>().weapon = this.gameObject;

        Rigidbody TemporaryRigidBody;
        TemporaryRigidBody = temporaryBulletHandler.GetComponent<Rigidbody>();

        TemporaryRigidBody.AddForce(firePoint.transform.forward * bulletSpeed);


       
        Destroy(temporaryBulletHandler, 10f);



    }

    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }


   
}
