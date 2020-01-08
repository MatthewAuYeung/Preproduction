using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public float Range;
    public GameObject uiObject;

    private GameObject _player;
    private float _distanceToPlayer;
    public vThirdPersonCamera TPSCamera;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerTag");
    }
    void Start()
    {
        uiObject.SetActive(false);
    }

    private void Update()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (_distanceToPlayer <= Range && Input.GetKey(KeyCode.O))
        {
            TPSCamera.lockCamera = true;
            uiObject.SetActive(true);
            ShowMouseCursor();
        }
    }

    void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    // Update is called once per frame
    //void OnTriggerEnter (Collider player)
    //{
    //    Debug.Log("Hit");
    //    if(player.gameObject.tag == "PlayerTag")
    //    {
    //        uiObject.SetActive(true);
    //        StartCoroutine("WaitForSec");
    //    }
    //}
    IEnumerator WaitForSec()//wait for secs to read text
    {
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }


}
