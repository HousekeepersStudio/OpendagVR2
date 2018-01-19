using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : Entity {
    private Tower target;
    public Text text;

    public Tower(string type, float maxHealth, float damage, int level, Tower target)
        :base (type, maxHealth, damage, level)
    {
        this.target = target;
    }
    
    // Use this for initialization
    void Start () {
        this.target = this.GetComponent<Tower>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = this.level.ToString();
	}
}
