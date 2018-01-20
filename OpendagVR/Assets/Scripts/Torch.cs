using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Torch : MonoBehaviour
{

    private int amountOfBanners = 4;
    private bool dragonBanner = true;
    private bool vikingBanner = true;
    private bool ravenBanner = true;
    private bool serpentBanner = true;

    private bool loadSceneStarted;

    private float _fadeDuration = 2f;
    public GameObject particle;
    public GameObject lighting;
    public GameObject fireParticle;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.StartsWith("Controller"))
        {
            CreateFire(this.gameObject);
        }

        if (col.gameObject.name == "dragonBanner" && dragonBanner)
        {
            CreateSmoke(col.gameObject);
            StartCoroutine(RemoveFlag(col.gameObject, 2f));
            amountOfBanners -= 1;
            dragonBanner = false;
        }
        else if (col.gameObject.name == "ravenBanner" && ravenBanner)
        {
            CreateSmoke(col.gameObject);
            StartCoroutine(RemoveFlag(col.gameObject, 2f));
            amountOfBanners -= 1;
            ravenBanner = false;
        }
        else if (col.gameObject.name == "vikingBanner" && vikingBanner)
        {
            CreateSmoke(col.gameObject);
            StartCoroutine(RemoveFlag(col.gameObject, 2f));
            amountOfBanners -= 1;
            vikingBanner = false;
        }
        else if (col.gameObject.name == "serpentBanner" & serpentBanner)
        {
            CreateSmoke(col.gameObject);
            StartCoroutine(RemoveFlag(col.gameObject, 2f));
            amountOfBanners -= 1;
            serpentBanner = false;
        }
    }

    private void FixedUpdate()
    {
        if (amountOfBanners == 1)
        {
            if (!loadSceneStarted)
            {
                SetHouse();
                StartCoroutine(LoadNextScene(2f));
            }
        }
    }

    private void CreateSmoke(GameObject banner)
    {
        GameObject smoke = GameObject.Instantiate(particle, banner.transform);
        smoke.transform.localPosition = new Vector3(0, 0, 0.5f);
    }

    private void CreateLight(GameObject banner)
    {
        GameObject light = GameObject.Instantiate(lighting, banner.transform);
        light.transform.localPosition = new Vector3(0, 0, 0.5f);
    }

    private void CreateFire(GameObject banner)
    {
        GameObject fire = GameObject.Instantiate(fireParticle, banner.transform);
        fire.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
        fire.transform.localPosition = new Vector3(0, 0, 0.5f);
    }

    private void FadeOut()
    {
        //set start color
        SteamVR_Fade.Start(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.black, _fadeDuration, true);
    }

    private void SetHouse()
    {
        if (serpentBanner)
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
        FadeOut();
        SceneManager.LoadScene(3);
    }

    IEnumerator RemoveFlag(GameObject flag, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(flag);
    }

    IEnumerator LoadNextScene(float time)
    {
        loadSceneStarted = true;
        yield return new WaitForSeconds(time);
        LoadIntroScene();
    }

}