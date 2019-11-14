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
        _cameraCount = 1;
    }

    private void Start()
    {
        CamOnePos = new Vector3(XPos1, YPos1, ZPos1);
        CamOneRotate = new Vector3(XRotate1, YRotate1, ZRotate1);

        CamTwoPos = new Vector3(XPos2, YPos2, ZPos2);
        CamTwoRotate = new Vector3(XRotate2, YRotate2, ZRotate2);
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
