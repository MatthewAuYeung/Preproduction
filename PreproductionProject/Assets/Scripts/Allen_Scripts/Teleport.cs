using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string levelToLoad;

    void Start()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerTag")
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
