using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_WeaponSwitch : MonoBehaviour {
    public GameObject bowPrefab;

    GameObject cameraRig;
    GameObject bow;

    GameObject controllerLeft;
    GameObject controllerRight;

    SteamVR_TrackedController buttonsLeft;
    SteamVR_TrackedController buttonsRight;



    bool pressed = false;

    bool teleport = false;
    bool grab = true;

    private void Awake()
    {
        SetupGameObjects();
        ChangeToTeleporting();
    }

    private void FixedUpdate()
    {
        SetupGameObjects();
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
        RemoveBow();
        controllerRight.GetComponent<SteamVR_LaserPointer>().enabled = true;
        if(controllerRight.transform.Find("New Game Object") != null)
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
        controllerRight.transform.Find("Origin").gameObject.SetActive(true);
        controllerLeft.transform.Find("Origin").gameObject.SetActive(true);
        controllerRight.GetComponent<RWVR_InteractionController>().enabled = true;
        controllerLeft.GetComponent<RWVR_InteractionController>().enabled = true;
        

        SpawnBow();
    }

    void RemoveBow()
    {
        if (bow != null)
            GameObject.DestroyObject(bow);
    }

    void SpawnBow()
    {
        RemoveBow();
        bow = bowPrefab;
        bow = GameObject.Instantiate(bow, new Vector3(cameraRig.transform.position.x, cameraRig.transform.position.y + 1f, cameraRig.transform.position.z), Quaternion.identity);
        bow.GetComponent<Rigidbody>().isKinematic = true;
    }

    void DropItems()
    {

    }

    void SetupGameObjects()
    {
        if(cameraRig == null)
            cameraRig = this.gameObject;

        if (controllerLeft == null)
            controllerLeft = cameraRig.transform.Find("Controller (left)").gameObject;

        if (controllerRight == null)
            controllerRight = cameraRig.transform.Find("Controller (right)").gameObject;

        if(buttonsLeft == null)
            buttonsLeft = controllerLeft.GetComponent<SteamVR_TrackedController>();

        if(buttonsRight == null)
            buttonsRight = controllerRight.GetComponent<SteamVR_TrackedController>();
    }
    
    //IEnumerator WeaponSwitchDelay()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    ChangeWeapon();
    //}
}
