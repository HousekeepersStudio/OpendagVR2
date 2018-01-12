using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class viveinput : MonoBehaviour {
    private void Awake()
    {
        SteamVR_TrackedController buttons = GetComponent<SteamVR_TrackedController>();
    }
}
