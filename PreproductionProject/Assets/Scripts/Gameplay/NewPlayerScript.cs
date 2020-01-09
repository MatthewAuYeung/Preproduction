using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerScript : MonoBehaviour
{
    public SimpleHealthBar healthBar;

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
}
