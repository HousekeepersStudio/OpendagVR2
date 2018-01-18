using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour {


    public GameObject controllerRight;
    public string towerTag;

    private SteamVR_LaserPointer pointer;
    private SteamVR_TrackedController buttons;

    void Update()
    {
        try
        {
            if (pointer != null && buttons != null)
                Raycast();

            if (pointer == null)
                pointer = controllerRight.GetComponent<SteamVR_LaserPointer>();

            if (buttons == null)
                buttons = controllerRight.GetComponent<SteamVR_TrackedController>();
        }
        catch (Exception e)
        {
            //Debug.Log(e);
        }
    }

    void Raycast()
    {
        RaycastHit hit;
        if (pointer.holder.activeSelf)
        {
            Physics.Raycast(pointer.pointer.transform.position, pointer.pointer.transform.forward, out hit);
            if (hit.collider.tag == towerTag && buttons.triggerPressed)
            {
                if (true)
                {
                    Turret t = hit.transform.GetChild(1).GetComponent<Turret>();
                }
            }
        }
    }
}
