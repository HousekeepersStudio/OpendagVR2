using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour {

    public GameObject towerDragons;
    public GameObject towerSerpents;
    public GameObject towerVikings;
    public GameObject towerRavens;

    public GameObject controllerRight;
    public string unbuildTowerTag;


    private SteamVR_LaserPointer pointer;
    private SteamVR_TrackedController buttons;
    
	void Update () {
        try
        {
            if (pointer != null && buttons != null)
                Raycast();
            else
                pointer = controllerRight.GetComponent<SteamVR_LaserPointer>();

            if (buttons == null)
                buttons = controllerRight.GetComponent<SteamVR_TrackedController>();
        }
        catch { }
	}

    void Raycast()
    {
        RaycastHit hit;
        Physics.Raycast(pointer.pointer.transform.position, pointer.pointer.transform.forward, out hit);
        if (hit.collider.tag == unbuildTowerTag && buttons.triggerPressed)
        {
            Vector3 pos = hit.transform.parent.position;
            Quaternion rot = hit.transform.parent.rotation;

            Destroy(hit.transform.parent.gameObject);

            string selectedHouse = PlayerPrefs.GetString("house");
            switch (selectedHouse)
            {
                case "dragons":
                    Instantiate(towerDragons, pos, rot);
                    break;

                case "serpents":
                    Instantiate(towerSerpents, pos, rot);
                    break;

                case "vikings":
                    Instantiate(towerVikings, pos, rot);
                    break;

                case "ravens":
                    Instantiate(towerRavens, pos, rot);
                    break;
            }
        }
    }
}
