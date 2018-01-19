using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObject : MonoBehaviour {
    GameObject cameraRig;

    GameObject parent;

    private void Awake()
    {
        
        //parent = this.gameObject.transform.parent.gameObject;
    }

    public void Teleport(Transform camera, GameObject prevTeleport)
    {
        cameraRig = GameObject.Find("[CameraRig]");
        this.parent = this.transform.parent.gameObject;
        if (prevTeleport != null)
            prevTeleport.SetActive(true);
        Vector3 pos = this.gameObject.transform.position;
        camera.position = pos;
        parent.SetActive(false);
    }

    public Vector3 GetPos()
    {
        return this.gameObject.transform.position;
    }
}
