using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
[RequireComponent(typeof(SteamVR_TrackedObject))]

public class TouchpadCross : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }


    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    // Update is called once per frame
    void Update()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0));
            print("Pressing Touchpad");

            if (touchpad.y > 0.7f)
            {
                print("Moving Up");
            }

            else if (touchpad.y < -0.7f)
            {
                print("Moving Down");
            }

            if (touchpad.x > 0.7f)
            {
                print("Moving Right");

            }

            else if (touchpad.x < -0.7f)
            {
                print("Moving left");
            }

        }
    }
}
