using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesOpener : MonoBehaviour
{
    public GameObject Panel;

    public void OpenNotes()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }
}
