using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitCounterUI : MonoBehaviour
{
    public Text hitcounter;
    NewPlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<NewPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        hitcounter.text = playerScript.comboCount.ToString();
    }
}
