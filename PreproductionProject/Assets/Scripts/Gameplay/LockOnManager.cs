using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnManager : MonoBehaviour
{
    [SerializeField]
    private Image aim;

    [SerializeField]
    private Image lockon;

    [SerializeField]
    private Image warplockon;

    public Vector2 ui_offset;
    Camera cam;
    public List<Transform> targets;

    [SerializeField]
    private float range = 10.0f;

    private Vector3 closestEnemyPos;
    private GameObject closestObj;
    private bool islockon = false;
    private bool isSelected = false;
    private Vector3 originalPos;
    void Start()
    {
        cam = Camera.main;
        originalPos = aim.transform.position;
    }

    void Update()
    {
        if (targets.Count == 0)
        {
            islockon = false;
            return;
        }
        if(!isSelected)
        {
            warplockon.color = Color.clear;
            closestObj = targets[targetIndex()].gameObject;

            float dist = Vector3.Distance(transform.position, closestObj.transform.position);

            if (dist <= range)
            {
                AimInterface(true);
                Vector3 screenPos = cam.WorldToScreenPoint(targets[targetIndex()].position);
                aim.transform.position = screenPos;

                if (Input.GetButtonDown("LockOn"))
                {
                    islockon = true;
                    LockInterface(true);
                }
                if (Input.GetButtonUp("LockOn"))
                {
                    islockon = false;
                    LockInterface(false);
                }
            }
            else
            {
                islockon = false;
                AimInterface(false);
                LockInterface(false);
            }
        }
        //else
        //{
        //    islockon = false;
        //    AimInterface(false);
        //    LockInterface(false);
        //    warplockon.color = Color.white;
        //}
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
            //distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(targets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
            //distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(targets[i].position), new Vector2(transform.position.x, transform.position.z));
            distances[i] = Vector3.Distance(targets[i].position, transform.position);
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

    public GameObject GetClosestObject()
    {
        return closestObj;
    }

    public bool GetIsLockOn()
    {
        return islockon;
    }

    public void SetIsSelected(bool state)
    {
        isSelected = state; islockon = false;
        AimInterface(false);
        LockInterface(false);
        aim.transform.position = originalPos;
        warplockon.color = Color.white;
    }

    public Vector3 GetAimPosition()
    {
        return aim.transform.position;
    }
}
