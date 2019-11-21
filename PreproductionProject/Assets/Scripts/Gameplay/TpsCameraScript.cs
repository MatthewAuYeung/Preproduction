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
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + offset;
    }

    public void ChangeActive(bool aiming)
    {
        if(aiming)
        {
            gameObject.SetActive(true);
            _listener.enabled = true;

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
}
