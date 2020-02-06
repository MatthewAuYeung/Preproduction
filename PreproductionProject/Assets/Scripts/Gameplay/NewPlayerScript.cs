using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public SimpleHealthBar manaBar;
    public float AttckDamage;
    public int hitcount = 0;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float mana;
    [SerializeField]
    private float maxMana;
    [SerializeField]
    float warpCooldown;

    private void Awake()
    {
        health = maxHealth;
        mana = maxMana;
    }

    void Update()
    {
        if(mana < maxMana)
        {
            mana += 1.0f;
        }
        healthBar.UpdateBar(health, maxHealth);
        manaBar.UpdateBar(mana, maxMana);
    }

    //IEnumerator RegenerateOverTime()
    //{

    //}
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void UseMana(float amount)
    {
        mana -= amount;
    }

    public float GetWarpCooldown()
    {
        return warpCooldown;
    }
}
