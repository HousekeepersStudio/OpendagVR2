using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Torch : MonoBehaviour {

    private int amountOfBanners = 4;
    private bool dragonBanner = true;
    private bool vikingBanner = true;
    private bool ravenBanner = true;
    private bool serpantBanner = true;
    public string house;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "dragonBanner")
        {
            amountOfBanners -= 1;
            dragonBanner = false;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "ravenBanner")
        {
            amountOfBanners -= 1;
            ravenBanner = false;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "vikingBanner")
        {
            amountOfBanners -= 1;
            vikingBanner = false;
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "serpantBanner")
        {
            amountOfBanners -= 1;
            serpantBanner = false;
            Destroy(col.gameObject);
        }

        if (amountOfBanners == 1)
        {
            SetHouse();
        }
    }

    private void SetHouse()
    {
        if (serpantBanner)
        {
            house = "Serpant";
            LoadIntroScene();
        }
        else if (vikingBanner)
        {
            house = "Viking";
            LoadIntroScene();
        }
        else if (dragonBanner)
        {
            house = "Dragon";
            LoadIntroScene();
        }
        else if (ravenBanner)
        {
            house = "Raven";
            LoadIntroScene();
        }
    }

    private void LoadIntroScene()
    {
        SceneManager.LoadScene(1);
    }
}
