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

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "dragonBanner")
        {
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            dragonBanner = false;
        }
        if (col.gameObject.name == "ravenBanner")
        {
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            ravenBanner = false;
        }
        if (col.gameObject.name == "vikingBanner")
        {
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            vikingBanner = false;
        }
        if (col.gameObject.name == "serpantBanner")
        {
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            serpantBanner = false;
        }

        if (amountOfBanners == 1)
        {
            SetHouse();
            LoadIntroScene();
        }
    }

    private void SetHouse()
    {
        if (serpantBanner)
        {
            PlayerPrefs.SetString("house", "serpents");
        }
        else if (vikingBanner)
        {
            PlayerPrefs.SetString("house", "vikings");
        }
        else if (dragonBanner)
        {
            PlayerPrefs.SetString("house", "dragons");
        }
        else if (ravenBanner)
        {
            PlayerPrefs.SetString("house", "ravens");
        }
    }

    private void LoadIntroScene()
    {
        SceneManager.LoadScene(1);
    }
}
