using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
    public Text gradeText;
    public Text comboText;
    public Text hitText;
    public Text timeText;

    private NewPlayerScript playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = FindObjectOfType<NewPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            gradeText.text = "Grade: ";
            comboText.text = "Highest Combo: ";
            hitText.text = "Number of hits: " + playerInfo.GetTotalHitCount().ToString();
            timeText.text = "Time: ";
        }
    }
}
