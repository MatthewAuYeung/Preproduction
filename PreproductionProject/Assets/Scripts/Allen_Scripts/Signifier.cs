using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signifier : MonoBehaviour
{
    [SerializeField] private Image signifier;

    public void ShowSignifier()
    {
        signifier.enabled = true;
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        signifier.transform.position = pos;
    }
}
