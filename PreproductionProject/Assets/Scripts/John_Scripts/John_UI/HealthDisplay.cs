using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private int health = 100;
    public Text healthText;

    private void Update()
    {
        healthText.text = "HEALTH : " + health;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            health--;
        }
    }
}
