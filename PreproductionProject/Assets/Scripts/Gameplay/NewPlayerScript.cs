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
    float health;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float mana;
    [SerializeField]
    float maxMana;
    [SerializeField]
    float manaRegenDelay;
    [SerializeField]
    float warpCooldown;

    private void Awake()
    {
        health = maxHealth;
        mana = maxMana;
        InvokeRepeating("Regenerate", 1.0f, manaRegenDelay);
    }

    private void Start()
    {
    }

    void Update()
    {
        healthBar.UpdateBar(health, maxHealth);
        manaBar.UpdateBar(mana, maxMana);
    }

    void Regenerate()
    {
        if(mana  < maxMana)
        {
            mana += 1.0f;
        }
    }

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
