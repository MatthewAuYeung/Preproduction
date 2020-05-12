using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugRestart : MonoBehaviour
{
    public bool usingDebug;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(usingDebug)
        {
            if (Input.GetButtonDown("DebugRestart"))
            {
                Restart();
            }
        }
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
