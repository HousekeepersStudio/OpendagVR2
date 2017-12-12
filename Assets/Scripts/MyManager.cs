using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class Control {
	public string name;
	public KeyCode key;
	public string joyName;


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
			break;
		case "steam_button":
			result = steamVR.steamPressed;
			break;
		case "trigger_button":
			result = steamVR.triggerPressed;
			break;
		case "pad_button":
			result = steamVR.padPressed;
			break;
		case "pad_touch":
			result = steamVR.padTouched;
			break;
		case "hold_grip":
			result = steamVR.gripped;
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

	public int score;
    public string House;

    public SteamVR_TrackedController steamVR;

    private void Awake()
    {
        House = PlayerPrefs.GetString("House");
        PlayerPrefs.DeleteAll();
    }

    public Control GetControl(string name) {
		Control result = new Control();
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
