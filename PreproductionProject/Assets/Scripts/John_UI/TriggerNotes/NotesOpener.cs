using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject CloseButton;

    public void OpenNotes()
    {
        if (Panel != null && CloseButton != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
            CloseButton.SetActive(!isActive);
        }
    }
}
