using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BombThrower : MonoBehaviour
{
    public GameObject bomb;
    public Transform bombSpawnTranform;
    public float throwPower = 1500f;
    public Animator animator;
    public float downTime, upTime, pressTime = 0;
    public float countDown = 1.0f;
    public bool ready = false;
    public Image bombBar;
    [SerializeField]
    float manaUsed;
    private float waitTime;

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

        if (Input.GetButtonDown("Bomb") && ready == false)
        {
            downTime = Time.time;
            pressTime = downTime + countDown;
            ready = true;
        }
        if (Input.GetButtonUp("Bomb"))
        {
            ready = false;
        }

        if (Time.time >= pressTime && ready == true)
        {
            ready = false;
            _player.UseMana(manaUsed);
            animator.SetTrigger("Throw");
            Vector3 throwDirection = Camera.main.transform.forward;
            Vector3 spawnPosition = bombSpawnTranform.position + (throwDirection * 1f);
            //Vector3 spawnPosition = Camera.main.transform.position;
            GameObject newBomb = Instantiate(bomb, spawnPosition, Quaternion.identity);
            Vector3 throwForce = throwDirection * throwPower;
            newBomb.GetComponent<Rigidbody>().AddForce(throwForce);
            //waitTime = 0.0f;
        }
        if (ready)
        {
            bombBar.fillAmount = Time.time - downTime / countDown;
        }
        else
        {
            bombBar.fillAmount = 0;
        }
    }
}
