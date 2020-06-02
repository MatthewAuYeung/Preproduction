using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField]
    private Image cooldownMeter;
    [SerializeField]
    private GameObject icon;
    private float abilityCooldown;
    private float waitTime;
    private bool done = true;

    private void Awake()
    {
        cooldownMeter.gameObject.SetActive(false);
    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        cooldownMeter.fillAmount = waitTime / abilityCooldown;

        if(cooldownMeter.fillAmount == 1.0f)
        {
            cooldownMeter.gameObject.SetActive(false);
            done = true;
        }
    }

    public void SetAbilityCooldown(float cooldown)
    {
        abilityCooldown = cooldown;
    }

    public void AbilityUsed()
    {
        GetComponent<Image>().enabled = true;
        cooldownMeter.gameObject.SetActive(true);
        waitTime = 0.0f;
        done = false;
    }

    public bool CheckAbilityCooldown()
    {
        return done;
    }
}
