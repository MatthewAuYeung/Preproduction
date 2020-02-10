using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float time = 60;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        float minutes = Mathf.Floor(t / 60f);
        float seconds = Mathf.RoundToInt(t % 60f);
        timerText.text = minutes + ":" + seconds;

        if (t >= time)
        {
            DebugRestart.Restart();
        }
    }
}
