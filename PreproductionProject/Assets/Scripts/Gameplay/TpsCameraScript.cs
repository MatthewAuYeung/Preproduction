using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraScript : MonoBehaviour
{
    public GameObject MainCamera;

    private GameObject _player;
    private Vector3 offset;

    private AudioListener _listener;
    private AudioListener mainListener;

    private float distance;
    Vector3 playerPrevPos;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerTag");
        _listener = GetComponent<AudioListener>();
        mainListener = MainCamera.GetComponent<AudioListener>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        _listener.enabled = false;

        offset = transform.position - _player.transform.position;
        distance = offset.magnitude;
        playerPrevPos = _player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position - _player.transform.forward * distance;
        transform.Translate(Vector3.right *offset.x, Space.Self);
        transform.Translate(Vector3.up * offset.y, Space.Self);
    }

    public void ChangeActive(bool aiming)
    {
        if(aiming)
        {
            gameObject.SetActive(true);
            _listener.enabled = true;

            SetCamera();

            MainCamera.SetActive(false);
            mainListener.enabled = false;
        }
        else
        {
            gameObject.SetActive(false);
            _listener.enabled = false;

            MainCamera.SetActive(true);
            mainListener.enabled = true;
        }
    }

    private void SetCamera()
    {
        transform.rotation = _player.transform.rotation;
    }
}
