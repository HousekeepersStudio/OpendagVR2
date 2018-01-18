using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAnnouncement : MonoBehaviour {
    private Text elText;

	// Use this for initialization
	void Start () {
        elText = this.GetComponent<Text>();
    }

	
    public void SetText(int wave)
    {
        elText.enabled = true;
        string waveText = wave.ToString();
        elText.text = "Wave " + waveText;
    }

    public IEnumerator Waiter(int wave)
    {
        yield return new WaitForSeconds(5);
        SetText(wave);

        yield return new WaitForSeconds(5);
        elText.enabled = false;
    }
}
