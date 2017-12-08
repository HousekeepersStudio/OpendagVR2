using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour{
    protected string type;
    protected float curHealth;
    protected float maxHealth;
    protected float damage;
    protected int level;
    protected Image healthBar;

    public Entity(string type, float maxHealth, float damage, int level)
    {
        this.type = type;
        this.maxHealth = maxHealth;
        this.curHealth = this.maxHealth;
        this.damage = damage;
    }
    public Entity(string type, int maxHealth)
    {
        this.type = type;
        this.maxHealth = maxHealth;
        this.curHealth = this.maxHealth;
        this.damage = 0;
    }
    public void Attack(GameObject mainTower)
    {
        mainTower.GetComponent<Target>().TakeDamage(this.damage);
        Debug.Log("Main Tower Health: " + mainTower.GetComponent<Target>().curHealth);
    }

    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        healthBar.fillAmount = curHealth / maxHealth;
    }

    public string GetEnemyType()
    {
        return this.type;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return curHealth;
    }

}
