using UnityEngine;

public class Entity : MonoBehaviour{
    protected string type;
    protected int curHealth;
    protected int maxHealth;
    protected int damage;
    protected int level;

    public Entity(string type, int maxHealth, int damage, int level)
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

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }

    public string GetType()
    {
        return this.type;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return curHealth;
    }

}
