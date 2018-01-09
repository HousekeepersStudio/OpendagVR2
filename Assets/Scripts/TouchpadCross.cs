using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
[RequireComponent(typeof(SteamVR_TrackedObject))]

public class TouchpadCross : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    bool pressed;
    public GameObject bowPrefab;

    GameObject cameraRig;
    GameObject bow;

    GameObject controllerLeft;
    GameObject controllerRight;

    SteamVR_TrackedController buttonsLeft;
    SteamVR_TrackedController buttonsRight;

    bool teleport = false;
    bool grab = true;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        SetupGameObjects();
        ChangeToTeleporting();
    }


    // Use this for initialization
    void FixedUpdate()
    {
        SetupGameObjects();
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed == false)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                pressed = true;
                Vector2 touchpad = (device.GetAxis(EVRButtonId.k_EButton_Axis0));

                if (touchpad.y > 0.7f)
                {
                    Debug.Log("Moving Up");
                }

                else if (touchpad.y < -0.7f)
                {
                    Debug.Log("Moving Down");
                }

                if (touchpad.x > 0.7f)
                {
                    if (!controllerRight.GetComponent<Teleportation>().enabled)
                    {
                        ChangeToTeleporting();
                    }
                }

                else if (touchpad.x < -0.7f)
                {
                    if (!controllerRight.GetComponent<RWVR_InteractionController>().enabled)
                    {
                        ChangeToGrab();
                    }
                }

            }
        }
        else
        {
            if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
                pressed = false;
        }
    }

    void ChangeToTeleporting()
    {
        teleport = !teleport;
        grab = !grab;
        RemoveBow();
        controllerRight.GetComponent<SteamVR_LaserPointer>().enabled = true;
        if (controllerRight.transform.Find("New Game Object") != null)
            controllerRight.transform.Find("New Game Object").gameObject.SetActive(true);
        controllerRight.GetComponent<Teleportation>().enabled = true;
        controllerRight.GetComponent<RWVR_InteractionController>().enabled = false;
        controllerLeft.GetComponent<RWVR_InteractionController>().enabled = false;
        controllerRight.transform.Find("Origin").gameObject.SetActive(false);
        controllerLeft.transform.Find("Origin").gameObject.SetActive(false);
    }

    void ChangeToGrab()
    {
        teleport = !teleport;
        grab = !grab;
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

    void SetupGameObjects()
    {
        if (cameraRig == null)
            cameraRig = GameObject.Find("[CameraRig]");

        if (controllerLeft == null)
            controllerLeft = cameraRig.transform.Find("Controller (left)").gameObject;

        if (controllerRight == null)
            controllerRight = cameraRig.transform.Find("Controller (right)").gameObject;
    }
}
