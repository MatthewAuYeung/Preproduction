using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    public PlayerScript Player;

    [Header("Cameras")]
    public GameObject CameraOne;
    public GameObject CameraTwo;

    AudioListener cameraOneLis;
    AudioListener cameraTwoLis;

    private int _cameraCount;
    // Start is called before the first frame update
    void Start()
    {
        cameraOneLis = CameraOne.GetComponent<AudioListener>();
        cameraTwoLis = CameraTwo.GetComponent<AudioListener>();

        _cameraCount = 1;
        CameraTwo.SetActive(false);
        cameraTwoLis.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerTag"))
        {
            switch (_cameraCount)
            {
                case 1:
                    {
                        CameraOne.SetActive(false);
                        cameraOneLis.enabled = false;

                        CameraTwo.SetActive(true);
                        cameraTwoLis.enabled = true;
                        _cameraCount = 2;
                        break;
                    }
                case 2:
                    {
                        CameraOne.SetActive(true);
                        cameraOneLis.enabled = true;

                        CameraTwo.SetActive(false);
                        cameraTwoLis.enabled = false;

                        _cameraCount = 1;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
