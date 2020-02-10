using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public Transform bombSpawnTranform;
    public float throwPower = 100f;
    public Animator animator;
    [SerializeField]
    float manaUsed;

    private NewPlayerScript _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<NewPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.HasMana(manaUsed))
            return;

        if (Input.GetKeyDown(KeyCode.B))
        {
            _player.UseMana(manaUsed);
            animator.SetTrigger("Throw");
            Vector3 throwDirection = Camera.main.transform.forward;
            Vector3 spawnPosition = bombSpawnTranform.position + (throwDirection * 1f);
            GameObject newBomb = Instantiate(bomb, spawnPosition, Quaternion.identity);
            Vector3 throwForce = throwDirection * throwPower;
            newBomb.GetComponent<Rigidbody>().AddForce(throwForce);
        }
    }
}
