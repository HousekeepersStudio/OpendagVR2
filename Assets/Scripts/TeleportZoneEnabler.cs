using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZoneEnabler : MonoBehaviour {

    GameObject cameraRig;

    GameObject zone;

    private void Awake()
    {
        cameraRig = GameObject.Find("[CameraRig]");
        zone = this.gameObject.transform.Find("TeleportZone").gameObject;
    }

    private void Update()
    {
        if (!cameraRig.GetComponentInChildren<Teleportation>().enabled)
        {
            zone.SetActive(false);
        }
        else
        {
            zone.SetActive(true);
        }
    }
}
