using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signifier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer signifier;
    [SerializeField] private Sprite exclaimation;
    [SerializeField] private Sprite stunned;

    [SerializeField] private Vector3 stunSize = new Vector3();
    [SerializeField] private Vector3 exclaimationSize = new Vector3();



    public void ShowSignifier()
    {
        signifier.sprite = exclaimation;
        signifier.enabled = true;
        signifier.transform.localScale = exclaimationSize;
    }

    public void ShowStunnedSignifier()
    {
        signifier.sprite = stunned;
        signifier.enabled = true;
        signifier.transform.localScale = stunSize;
    }

    void Update()
    {
        signifier.transform.LookAt(Camera.main.transform.position, Vector3.up);
        //Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
        //signifier.transform.position = pos + new Vector3(0.0f, 1.0f, 0.0f);
    }
}
