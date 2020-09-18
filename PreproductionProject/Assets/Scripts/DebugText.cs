using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    public Canvas canvas;
    public Text text;

    private bool visible = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(visible);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            visible = !visible;
            canvas.gameObject.SetActive(visible);
        }
    }

    public void AddDebugText(string debuglog)
    {
        text.text = debuglog + "\n" + text.text;
    }
}
