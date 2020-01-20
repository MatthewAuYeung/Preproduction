using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject OpenButton;
    public GameObject CloseButton;
    
    public vThirdPersonCamera TPSCamera;

    public void OpenNotes()
    {
        if (Panel != null && CloseButton != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
            CloseButton.SetActive(!isActive);
            OpenButton.SetActive(false);
        }
    }

    public void CloseNotes()
    {
        if (Panel != null && OpenButton != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
            OpenButton.SetActive(!isActive);
            CloseButton.SetActive(false);
            TPSCamera.lockCamera = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
