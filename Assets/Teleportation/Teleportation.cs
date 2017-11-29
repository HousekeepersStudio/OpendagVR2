//This Script is created by Patrick

using System.Collections;
using UnityEngine;

public class Teleportation : MonoBehaviour {
    [Header("Setup:")]
    public GameObject cameraRig;
    public GameObject animationCanvas;
    
    [Header("Automaticaly Setup:")]
    [SerializeField]
    private SteamVR_TrackedController buttons;
    [SerializeField]
    private SteamVR_LaserPointer pointer;
    
    private void FixedUpdate()
    {
        while (buttons == null || pointer == null)
        {
            SetButtons();
            SetPointer();
        }

        Raycast();
        
    }

    void Raycast()
    {
        RaycastHit hit;
        Physics.Raycast(pointer.pointer.transform.position, pointer.pointer.transform.forward, out hit);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "TeleportZone")
            {
                if (buttons.triggerPressed)
                {
                    if (!(cameraRig.transform.position == hit.transform.position))
                    {
                        StartCoroutine(Teleport(hit));
                    }
                }
            }
        }
    }


    void SetButtons()
    {
        if (buttons == null)
        {
            buttons = GetComponent<SteamVR_TrackedController>();
        }
    }

    void SetPointer()
    {
        if (pointer == null)
        {
            pointer = GetComponent<SteamVR_LaserPointer>();
        }
    }

    IEnumerator Teleport(RaycastHit hit)
    {
        animationCanvas.GetComponentInChildren<Animator>().SetBool("Teleport", true);
        yield return new WaitForSeconds(0.2f);
        cameraRig.transform.position = hit.transform.position;
        animationCanvas.GetComponentInChildren<Animator>().SetBool("Teleport", false);
    }
}
