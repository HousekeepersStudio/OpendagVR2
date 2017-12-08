using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takeDamage : MonoBehaviour {

    private float maxHealth, currentHealth;

    public Image healthBar;
    public float damage;

	// Use this for initialization
	void Start () {
        this.maxHealth = 500;
        this.currentHealth = this.maxHealth;

        this.damage = 10;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            if (this.CanTakeDamage())
                this.TakeDamage(this.damage);
            else
                this.Die();
        }
	}

    public void TakeDamage(float amount)
    {
        this.currentHealth -= amount;

        this.healthBar.fillAmount = this.currentHealth / this.maxHealth;
    }

    public bool CanTakeDamage()
    {
        if (this.currentHealth > 0)
            return true;

        return false;
    }

    public void Die()
    {
        this.healthBar.color = new Color(1f, 0.25f, 0.25f);
        this.healthBar.fillAmount = 1f;
    }
}
