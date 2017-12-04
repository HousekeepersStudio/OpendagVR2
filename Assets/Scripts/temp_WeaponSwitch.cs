using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_WeaponSwitch : MonoBehaviour {
    public GameObject controllerLeft;
    public GameObject controllerRight;

    SteamVR_TrackedController buttonsLeft;
    SteamVR_TrackedController buttonsRight;

    bool pressed = false;

    bool teleport = false;
    bool grab = true;

    private void Awake()
    {
        buttonsLeft = controllerLeft.GetComponent<SteamVR_TrackedController>();
        buttonsRight = controllerRight.GetComponent<SteamVR_TrackedController>();
        ChangeToTeleporting();
    }

    private void FixedUpdate()
    {
        if (!pressed)
        {
            if (buttonsLeft.gripped || buttonsRight.gripped)
            {
                pressed = true;
                ChangeWeapon();
                //StartCoroutine(WeaponSwitchDelay());
            }
        }
        else
        {
            if (!buttonsLeft.gripped && !buttonsRight.gripped)
            {
                pressed = false;
                //StartCoroutine(WeaponSwitchDelay());
            }
        }
        
    }
    
    void ChangeWeapon()
    {
        teleport = !teleport;
        grab = !grab;

        if (teleport)
        {
            if (!controllerRight.GetComponent<Teleportation>().enabled)
            {
                ChangeToTeleporting();
            }
        }

        if (grab)
        {
            if (!controllerRight.GetComponent<RWVR_InteractionController>().enabled)
            {
                ChangeToGrab();
            }
        }
    }

    void ChangeToTeleporting()
    {
        controllerRight.GetComponent<SteamVR_LaserPointer>().enabled = true;
        controllerRight.transform.Find("New Game Object").gameObject.SetActive(true);
        controllerRight.GetComponent<Teleportation>().enabled = true;
        controllerRight.GetComponent<RWVR_InteractionController>().enabled = false;
        controllerLeft.GetComponent<RWVR_InteractionController>().enabled = false;
        controllerRight.transform.Find("Origin").gameObject.SetActive(false);
        controllerLeft.transform.Find("Origin").gameObject.SetActive(false);
    }

    void ChangeToGrab()
    {
        controllerRight.GetComponent<SteamVR_LaserPointer>().enabled = false;
        controllerRight.transform.Find("New Game Object").gameObject.SetActive(false);
        controllerRight.GetComponent<Teleportation>().enabled = false;
        controllerRight.GetComponent<RWVR_InteractionController>().enabled = true;
        controllerLeft.GetComponent<RWVR_InteractionController>().enabled = true;
        controllerRight.transform.Find("Origin").gameObject.SetActive(true);
        controllerLeft.transform.Find("Origin").gameObject.SetActive(true);
    }
    
    //IEnumerator WeaponSwitchDelay()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    ChangeWeapon();
    //}
}
