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

    private float _fadeDuration = 2f;
    public GameObject particle;
    public GameObject light;



    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "dragonBanner")
        {
            GameObject.Instantiate(particle, col.gameObject.transform);
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            dragonBanner = false;
        }
        else if (col.gameObject.name == "ravenBanner")
        {
            GameObject.Instantiate(particle, col.gameObject.transform);
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            ravenBanner = false;
        }
        else if (col.gameObject.name == "vikingBanner")
        {
            GameObject.Instantiate(particle, col.gameObject.transform);
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            vikingBanner = false;
        }
        else if (col.gameObject.name == "serpantBanner")
        {
            GameObject.Instantiate(particle, col.gameObject.transform);
            Destroy(col.gameObject);
            amountOfBanners -= 1;
            serpantBanner = false;
        }
    }

    private void FixedUpdate()
    {
        if (amountOfBanners == 1)
        {
            BrightenFlag();
            SetHouse();
            LoadIntroScene();
        }
    }

    private void BrightenFlag()
    {
        if (serpantBanner)
        {
            GameObject.Instantiate(light, GameObject.Find("serpentBanner").transform);
        }
        else if (vikingBanner)
        {
            GameObject.Instantiate(light, GameObject.Find("vikingBanner").transform);
        }
        else if (dragonBanner)
        {
            GameObject.Instantiate(light, GameObject.Find("dragonBanner").transform);
        }
        else if (ravenBanner)
        {
            GameObject.Instantiate(light, GameObject.Find("ravenBanner").transform);
        }
    }

    private void FadeOut()
    {
        //set start color
        SteamVR_Fade.Start(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.black, _fadeDuration);
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
        BrightenFlag();
        FadeOut();
        SceneManager.LoadScene(1);
    }

}
