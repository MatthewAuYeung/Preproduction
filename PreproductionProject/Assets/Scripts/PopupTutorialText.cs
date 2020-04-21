using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTutorialText : MonoBehaviour
{
    public GameObject tutorialText;

    private void OnTriggerEnter(Collider other)
    {
        tutorialText.SetActive(true);
        Time.timeScale = 0.5f;
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialText.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
