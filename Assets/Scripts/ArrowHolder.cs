using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHolder : MonoBehaviour {
    public GameObject arrowPrefab;
    public Vector3 arrowSpawnPosition;
    public Quaternion arrowSpawnRotation;
    private GameObject spawnedObject;
    private bool arrowPickedUp;

    private void Update()
    {
        if(spawnedObject != null)
        {
            if(spawnedObject.GetComponent<RWVR_SnapToController>().IsFree())
                spawnedObject.GetComponent<Rigidbody>().isKinematic = true;

            if (arrowPickedUp)
                if (spawnedObject.GetComponent<RWVR_SnapToController>().IsFree())
                    spawnedObject.GetComponent<Rigidbody>().isKinematic = false;
                else
                {
                    spawnedObject = null;
                    arrowPickedUp = false;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.gameObject.tag);
        if(spawnedObject == null && other.gameObject.tag == "Controller")
        {
            spawnedObject = Instantiate(arrowPrefab, this.gameObject.transform.position + arrowSpawnPosition, Quaternion.identity);
            spawnedObject.transform.localRotation = arrowSpawnRotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (spawnedObject != null && other.gameObject.tag == "Controller" && spawnedObject.GetComponent<RWVR_SnapToController>().IsFree())
        {
            Destroy(spawnedObject);
            arrowPickedUp = false;
        }            
        else if (spawnedObject != null && !spawnedObject.GetComponent<RWVR_SnapToController>().IsFree())
            arrowPickedUp = true;
    }
}
