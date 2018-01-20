using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Pointer
{
    public string name;
    public Color color;
}

[System.Serializable]
public class Control {
	public string name;
	public KeyCode key;
	public string joyName;
   
	public SteamVR_TrackedController steamVR2;
    public SteamVR_TrackedController steamVR;

    /*public Control(SteamVR_TrackedController steamVR)
    {
        this.steamVR = steamVR;
    }*/


    public bool GetJoyKey() 
	{
        
		bool result = false;

		switch (this.joyName) 
		{
		case "menu_button":
			result = steamVR.menuPressed;
			if (!result && steamVR2 != null) {
				result = steamVR2.menuPressed;
			}
			break;
		case "steam_button":
			result = steamVR.steamPressed;
			if (!result && steamVR2 != null) {
				result = steamVR2.steamPressed;
			}
			break;
		case "trigger_button":
			result = steamVR.triggerPressed;
			if (!result && steamVR2 != null) {
				result = steamVR2.triggerPressed;
			}
			break;
		case "pad_button":
			result = steamVR.padPressed;
			if (!result && steamVR2 != null) {
				result = steamVR2.padPressed;
			}
			break;
		case "pad_touch":
			result = steamVR.padTouched;
			if (!result && steamVR2 != null) {
				result = steamVR2.padTouched;
			}
			break;
		case "hold_grip":
			result = steamVR.gripped;
			if (!result && steamVR2 != null) {
				result = steamVR2.gripped;
			}
			break;
		}

		return result;
	}

	/*public void ShowMapping() {
		this.kbBtn.GetComponentInChildren<Text> ().text = this.key.ToString ();
		this.jkBtn.GetComponentInChildren<Text> ().text = this.joyName.ToString ();
	}*/


}

public class MyManager : MonoBehaviour {
	public Control[] controls;
    public SteamVR_LaserPointer pointer;
	public int score;
    public int balance;
    public string house;
    public Pointer[] colors;

	[Header("Tower")]
	public GameObject[] towers;

    public Points points;
    

    public SteamVR_TrackedController steamVR;

    private void Awake()
    {
        points = GameObject.Find("Points").GetComponent<Points>();
        house = PlayerPrefs.GetString("house");

        Pointer result = new Pointer();
        switch (house)
        {
            case "dragons":
                for (int i = 0; i < colors.Length; i++)
                {
                    if ("dragons" == colors[i].name)
                    {
                        pointer.color = colors[i].color;
                    }
                }
                break;
            case "serpents":
                for (int i = 0; i < colors.Length; i++)
                {
                    if ("serpents" == colors[i].name)
                    {
                        pointer.color = colors[i].color;
                    }
                }
                break;
            case "vikings":
                for (int i = 0; i < colors.Length; i++)
                {
                    if ("vikings" == colors[i].name)
                    {
                        pointer.color = colors[i].color;
                    }
                }
                break;
            case "ravens":
               
                for (int i = 0; i < colors.Length; i++)
                {
                    if ("ravens" == colors[i].name)
                    {
                        pointer.color = colors[i].color;
                    }
                }
                break;
        }
    }

	public GameObject[] GetTowers() {
		return this.towers;
	}

    public void UpdatePoints()
    {
        score = points.GetScore();
        balance = points.GetBalance();
    }



    public Control GetControl(string name) {
		Control result = new Control();
		Debug.Log (name);
		result.name = "none";
		result.key = KeyCode.None;
		result.joyName = "none";

		for (int i = 0; i < controls.Length; i++) {
			if (name == controls [i].name) {
				result = controls [i];
			}
		}

		//result.steamVR = this.steamVR;

		return result;
	}
}
