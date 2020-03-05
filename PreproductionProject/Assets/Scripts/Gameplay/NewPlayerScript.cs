using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class NewPlayerScript : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public SimpleHealthBar manaBar;
    public GameObject winningCanvas;
    public Image bloodyscreen;
    public float AttckDamage;
    public int hitcount = 0;
    public float delay = 3.0f;
    public float WinScreenLast = 3.0f;
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
    [SerializeField]
    protected ParticleSystem hitEffect;
    [SerializeField]
    private float bloodyscreendelay;
    [SerializeField, Range(0.0f, 1.0f)]
    private float bloodyscreenalpha;

    private float timer;
    private float WinScreenTimer; 
    private EnemyManager enemyManager;
    private bool ShowWinUI = false;

    private void Awake()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        health = maxHealth;
        mana = maxMana;
        InvokeRepeating("Regenerate", 1.0f, manaRegenDelay);
    }

    private void Start()
    {
    }

    void Update()
    {
        if (health <= 0.0f)
            DebugRestart.Restart();

        healthBar.UpdateBar(health, maxHealth);
        manaBar.UpdateBar(mana, maxMana);
        resetCounter();
        resetWinScreenCounter();
        if (enemyManager.CheckBattleDone())
        {
            if(!ShowWinUI)
            {
                WinScreenTimer = Time.time + WinScreenLast;
                winningCanvas.SetActive(true);
            }
            ShowWinUI = true;            
        }
        if (hitEffect.isPlaying)
            StartCoroutine(StopHitEffect());
    }

    IEnumerator StopHitEffect()
    {
        yield return new WaitForSeconds(0.5f);
        hitEffect.Stop();
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
        hitEffect.Play();
        bloodyscreen.DOFade(bloodyscreenalpha, 0.15f);
        StartCoroutine(DisableBloodyScreen());
    }

    IEnumerator DisableBloodyScreen()
    {
        yield return new WaitForSeconds(bloodyscreendelay);
        bloodyscreen.DOFade(0.0f, 0.15f);
    }

    public void UseMana(float amount)
    {
        mana -= amount;
    }

    public void HealthPickup(float heal)
    {
        if (health > maxHealth - heal)
        {
            health = maxHealth;
        }
        else
        {
            health += heal;
        }
    }

    public void ManaPickup(float manaRe)
    {
        mana += manaRe;
        if (mana > maxMana - manaRe)
        {
            mana = maxMana;
        }
        else
        {
            mana += manaRe;
        }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetWarpCooldown()
    {
        return warpCooldown;
    }

    public void IncrementHitPoint()
    {
        timer = Time.time + delay;
        hitcount++;
    }
    private void resetCounter()
    {
        if (timer < Time.time)
        {
            hitcount = 0;
            timer = Time.time + delay;
        }

    }
    private void resetWinScreenCounter()
    {
        if (WinScreenTimer < Time.time)
        {
            winningCanvas.SetActive(false);
        }
    }

    public bool HasMana(float amountUse)
    {
        return (mana >= amountUse);
    }
}
