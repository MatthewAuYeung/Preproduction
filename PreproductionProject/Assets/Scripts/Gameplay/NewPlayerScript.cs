using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public float AttckDamage;
    public int hitcount = 0;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateBar(health, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
