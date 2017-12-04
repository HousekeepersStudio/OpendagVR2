using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour {
    public GameObject cameraRig;

    private void FixedUpdate()
    {
        if (!cameraRig.GetComponentInChildren<Teleportation>().isActiveAndEnabled)
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void Teleport(Transform camera, GameObject prevTeleport)
    {
        if(prevTeleport != null)
            prevTeleport.SetActive(true);
        Vector3 pos = this.gameObject.transform.position;
        camera.position = pos;
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    public Vector3 GetPos()
    {
        return this.gameObject.transform.position;
    }
}
