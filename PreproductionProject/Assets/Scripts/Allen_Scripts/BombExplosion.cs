using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombExplosion : MonoBehaviour
{
    public float enemyAgentSlowSpeed = 0.5f;
    public float slowEffectDuration = 5f;
    public float stunEffectDuration = 2f;
    public float lifeSpan = 0.25f;
    public SlowingBomb slowingBomb;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruct", lifeSpan);
        slowingBomb = FindObjectOfType<SlowingBomb>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyTag"))
        {
            var enemy = other.GetComponent<BaseEnemyScript>();
            stunEffectDuration = 3.0f;
            enemy.StunFromBomb(enemyAgentSlowSpeed, stunEffectDuration);
            //StartCoroutine(enemy.SlowFromBomb(stunEffectDuration, enemyAgentSlowSpeed, slowEffectDuration));
            //enemy.SlowFromBomb(enemyAgentSlowSpeed, slowEffectDuration);
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
