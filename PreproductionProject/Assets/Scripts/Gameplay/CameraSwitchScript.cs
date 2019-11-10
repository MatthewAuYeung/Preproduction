using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    public PlayerScript Player;

    [Header("MainCamera")]
    public GameObject MainCamera;

    [Header("CameraOne Transform")]
    public float XPos1;
    public float YPos1;
    public float ZPos1;

    public float XRotate1;
    public float YRotate1;
    public float ZRotate1;

    [Header("CameraTwo Transform")]
    public float XPos2;
    public float YPos2;
    public float ZPos2;

    public float XRotate2;
    public float YRotate2;
    public float ZRotate2;

    private int _cameraCount;

    private Vector3 CamOnePos;
    private Vector3 CamOneRotate;

    private Vector3 CamTwoPos;
    private Vector3 CamTwoRotate;

    private void Awake()
    {
        CamOnePos = new Vector3(0.0f, 1.0f, -10.0f);
        CamOneRotate = new Vector3(0.0f, 0.0f, 0.0f);

        CamTwoPos = new Vector3(-7.97f, 1.0f, 8.41f);
        CamTwoRotate = new Vector3(0.0f, 90.0f, 0.0f);

        _cameraCount = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerTag"))
        {
            switch (_cameraCount)
            {
                case 1:
                    {
                        MainCamera.transform.position = CamTwoPos;
                        MainCamera.transform.rotation = Quaternion.Euler(CamTwoRotate);
                        _cameraCount = 2;
                        break;
                    }
                case 2:
                    {
                        MainCamera.transform.position = CamOnePos;
                        MainCamera.transform.rotation = Quaternion.Euler(CamOneRotate);
                        _cameraCount = 1;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
