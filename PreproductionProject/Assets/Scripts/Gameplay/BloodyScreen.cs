//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class BloodyScreen : MonoBehaviour
//{
//    private Image bloodyScreen;
//    private NewPlayerScript player;
//    private float currentHP;
//    private float maxHP;
//    private float maxHPRatio;
//    private Color currentColor;
//    // Start is called before the first frame update
//    void Start()
//    {
//        bloodyScreen = GetComponent<Image>();
//        player = FindObjectOfType<NewPlayerScript>();

//        var tempcolor = bloodyScreen.color;
//        tempcolor.a = 0.0f;
//        currentColor = bloodyScreen.color = tempcolor;

//        currentHP = player.GetHealth();
//        maxHP = player.GetMaxHealth();
//        maxHPRatio = 1.0f / maxHP; 
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        currentHP = player.GetHealth();
//        if (currentHP == maxHP)
//            return;
//        currentColor.a = 1.0f - currentHP * maxHPRatio;
//        bloodyScreen.color = currentColor;
//    }
//}
