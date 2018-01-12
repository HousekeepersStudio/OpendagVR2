using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class MenuItem {
	public Button kb;
	public Button jk;
	public string key;

	public void UpdateKeyLabels(MyManager mm) {
		Control c = mm.GetControl (this.key);
		this.kb.GetComponentInChildren<Text> ().text = c.key.ToString ();
		this.jk.GetComponentInChildren<Text> ().text = c.joyName.ToString ();
	}
		
}

public class TestingMenuScript : MonoBehaviour 
{


	[Header("Menu objects")]
	public GameObject[] menus;
	public int openMenu = 0;
	bool menuActive = false;
	bool timerPassed = true;

	public MyManager myManager;
    
	public MenuItem[] menuItems;

	public Text scoreLabel;
    public Text balanceLabel;

	[Header("Sound objects")]
	public Slider volumeSlider;
	public Text volumeText;

	[Header("Controls")]
	public Control menu;
	public Control trigger;

	public bool loaded = false;
	public bool keyPressed = false;
	public bool joyPressed = false;

	// Use this for initialization
	void Start () 
	{
		
		menu = myManager.GetControl("menu_button");
		trigger = myManager.GetControl ("trigger_button");
		CloseAll ();
	}
	
	// Update is called once per frame
	void Update() 
	{
        if (myManager == null)
        {
            myManager = GameObject.Find("GameManager").GetComponent<MyManager>();
        }
        /*==================== Main menu controls==========================*/


        if (!joyPressed) {
			if (menu.GetJoyKey ()) {
				ChangeMenu ();
				joyPressed = true;
			}
		} else {
			if (!menu.GetJoyKey ()) {
				joyPressed = false;
			}
		}

		if (Input.GetKeyDown(menu.key))
		{
			ChangeMenu();
			keyPressed = false;
		}


		/*===================== Menu active ================================*/
		if (menuActive)
		{
			Time.timeScale = 0.7f;
			menus [openMenu].SetActive (true);
			if (!loaded) {
				switch (openMenu) {
				case 0:
					this.scoreLabel.text = FormatScore();
                        this.balanceLabel.text = FormatBalance();
					break;
				case 4:
					foreach (MenuItem m in menuItems) {
						m.UpdateKeyLabels (myManager);

						//m.kb.GetComponentInChildren<Text>().text = myManager.GetControl (m.key).key.ToString();
						//m.jk.GetComponentInChildren<Text>().text = myManager.GetControl (m.key).joyName.ToString();
					}
					break;
				}

				loaded = true;
			}
            if (openMenu == 0)
            {
                UpdatePoints();
                this.scoreLabel.text = FormatScore();
                this.balanceLabel.text = FormatBalance();
            }
        }
		else
		{
			CloseAll ();
			Time.timeScale = 1;
		}
	}

    void UpdatePoints()
    {
        myManager.UpdatePoints();
    }

    string FormatBalance()
    {
        return myManager.balance.ToString() + " Cash";
    }

	string FormatScore() {
		return myManager.score.ToString () + " Points";
	}

	void CloseAll()
	{
		for (int i = 0; i < menus.Length; i++) 
		{
			menus[i].SetActive(false);
		}
	}

	public void OpenPanel(int id) 
	{
		for (int i = 0; i < menus.Length; i++) 
		{
			if (i == id) 
			{
				menus[i].SetActive(true);
				openMenu = id;
				loaded = false;
			}
			else
			{
				menus[i].SetActive(false);
			}
		}
	}

	void ChangeMenu()
	{
		menuActive = !menuActive;
	}
		



	public void ChangeQuality(int index) 
	{
		QualitySettings.SetQualityLevel(index);
	}

	/* 0 = Fastest
	 * 1 = Fast
	 * 2 = Simple
	 * 3 = Good
	 * 4 = Beautiful
	 * 5 = Fantastic
	 * */

	public void ChangeVolume() 
	{
		AudioListener.volume = volumeSlider.value;
		volumeText.text = volumeSlider.value.ToString() + " %";
	}

	public void ExitGame() 
	{
		#if UNITY_EDITOR
		if (EditorApplication.isPlaying) 
		{
			EditorApplication.isPlaying = false;
		}
		#elif UNITY_STANDALONE
		Application.Quit();
		#endif

	}
}
