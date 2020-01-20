using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public vThirdPersonCamera TPSCamera;
    private void Start()
    {
        TPSCamera = GetComponent<vThirdPersonCamera>();
    }
}
