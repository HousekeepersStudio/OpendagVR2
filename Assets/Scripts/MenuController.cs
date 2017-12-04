using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class MenuController : MonoBehaviour {

    public bool menuActive = false;
    public GameObject GameMenu;
    SteamVR_TrackedController buttons;
    bool timerPassed = true;

    private void Awake()
    {
        buttons = GetComponent<SteamVR_TrackedController>();
    }

    private void Update()
    {
        if (buttons.menuPressed && timerPassed)
            ChangeMenuState();

        if (menuActive)
            ShowMenu();
        else
            HideMenu();
    }

    void ChangeMenuState()
    {
        menuActive = !menuActive;
        StartCoroutine(Timer(1f));
    }

    void ShowMenu()
    {
        GameMenu.SetActive(true);
    }

    void HideMenu()
    {
        GameMenu.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#else
        Application.Quit();
#endif
    }

    IEnumerator Timer(float time)
    {
        timerPassed = false;
        yield return new WaitForSeconds(time);
        timerPassed = true;
    }
}
