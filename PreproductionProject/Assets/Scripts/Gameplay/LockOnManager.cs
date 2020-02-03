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
    Camera cam;
    public List<Transform> targets;

    [SerializeField]
    private float range = 10.0f;

    private Vector3 closestEnemyPos;
    private bool islockon = false;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (targets.Count == 0)
            return;
        closestEnemyPos = targets[targetIndex()].position;

        float dist = Vector3.Distance(transform.position, closestEnemyPos);

        if(dist <= range)
        {
            AimInterface(true);
            Vector3 screenPos = cam.WorldToScreenPoint(targets[targetIndex()].position);
            aim.transform.position = screenPos;

            if (Input.GetMouseButtonDown(1))
            {
                islockon = true;
                LockInterface(true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                islockon = false;
                LockInterface(false);
            }
        }
        else
        {
            islockon = false;
            AimInterface(false);
        }
    }

    void AimInterface(bool state)
    {
        Color shown = state ? Color.white : Color.clear;

        aim.color = shown;
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

    public Vector3 GetClosestEnemy()
    {
        return closestEnemyPos;
    }

    public bool GetIsLockOn()
    {
        return islockon;
    }
}
