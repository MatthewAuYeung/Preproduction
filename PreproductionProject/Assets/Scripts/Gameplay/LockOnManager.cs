using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnManager : MonoBehaviour
{
    public Image aim;
    public Image lockon;
    public Vector2 ui_offset;
    public Transform target;
    Camera cam;
    public List<Transform> targets;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        aim.transform.position = screenPos;

        if(Input.GetMouseButtonDown(1))
        {
            LockInterface(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            LockInterface(false);
        }
    }

    void LockInterface(bool state)
    {
        float size = state ? 1.0f : 2.0f;
        float fade = state ? 1.0f : 0.0f;
        lockon.DOFade(fade, .15f);
        lockon.transform.DOScale(size, .15f).SetEase(Ease.OutBack);
        lockon.transform.DORotate(Vector3.forward * 180, .15f, RotateMode.FastBeyond360).From();
        aim.transform.DORotate(Vector3.forward * 90, .15f, RotateMode.LocalAxisAdd);
    }

    public int targetIndex()
    {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(targets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
        }

        float minDistance = Mathf.Min(distances);
        int index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }

        return index;

    }
}
