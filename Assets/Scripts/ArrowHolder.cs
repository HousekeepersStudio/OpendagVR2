using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHolder : MonoBehaviour {
    public GameObject arrowPrefab;
    private GameObject spawnedObject;

    private void Update()
    {
        if(spawnedObject != null)
        {
            if(spawnedObject.GetComponent<RWVR_SnapToController>().IsFree())
                spawnedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(spawnedObject == null && other.tag == "controller")
            spawnedObject = Instantiate(arrowPrefab, other.transform.position, Quaternion.identity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (spawnedObject != null && other.tag == "controller")
            Destroy(spawnedObject);
    }
}
