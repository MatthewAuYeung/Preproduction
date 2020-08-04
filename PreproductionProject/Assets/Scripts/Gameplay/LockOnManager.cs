using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Invector.CharacterController;

public class LockOnManager : MonoBehaviour
{
    [SerializeField]
    private Image aim;

    [SerializeField]
    private Image lockon;

    [SerializeField]
    private Image warplockon;

    public Vector2 ui_offset;
    public List<Transform> targets;

    private Camera cam;
    public CinemachineVirtualCamera freeCam;
    public CinemachineVirtualCamera lockOnCam;

    public Transform lockOnTarget;

    [SerializeField]
    private float range = 10.0f;

    private Vector3 closestEnemyPos;
    private GameObject closestObj;

    private Vector3 LockOnTargetPos;

    private Vector3 originalPos;
    private vThirdPersonController cc;

    private WarpController warpController;

    private bool islockon = false;
    private bool isSelected = false;
    private bool isToggled = false;

    private bool isFirstTime = true;
    void Start()
    {
        cam = Camera.main;
        originalPos = aim.transform.position;
        cc = GetComponent<vThirdPersonController>();
        warpController = GetComponent<WarpController>();
        targets.Clear();
    }

    private void LateUpdate()
    {
        if (isFirstTime)
        {
            targets.Clear();
            isFirstTime = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            targets.Clear();
        }

        if (targets.Count == 0)
        {
            islockon = false;
            isToggled = false;
            TurnoffLockOn();
            ToggledLockInterface(false);
            return;
        }
        if(!isSelected)
        {
            warplockon.color = Color.clear;
            closestObj = targets[targetIndex()].gameObject;

            float dist = Vector3.Distance(transform.position, closestObj.transform.position);

            if (dist <= range)
            {
                lockOnTarget.gameObject.transform.position = closestObj.gameObject.transform.position;

                AimInterface(true);
                Vector3 screenPos = cam.WorldToScreenPoint(closestObj.transform.position + closestObj.transform.up);
                aim.transform.position = screenPos;

                if(Input.GetButtonDown("LockOn"))
                {
                    LockInterface(true);
                }
                if (Input.GetButtonUp("LockOn") && !isToggled)
                {
                    islockon = false;
                    isToggled = false;
                    //cc.isStrafing = false;
                    //lockOnCam.gameObject.SetActive(false);
                    //freeCam.gameObject.SetActive(true);
                    LockInterface(false);
                }

                if (Input.GetButton("LockOn") || isToggled)
                {
                    islockon = true;
                    transform.LookAt(new Vector3(closestObj.transform.position.x, transform.position.y, closestObj.transform.position.z));
                    lockOnTarget.gameObject.transform.position = (isToggled) ? LockOnTargetPos : closestObj.transform.position;
                    //freeCam.gameObject.SetActive(false);
                    //lockOnCam.gameObject.SetActive(true);
                    //cc.isStrafing = true;

                    if (Input.GetButtonDown("ToggleLockOn"))
                    {
                        if (isToggled)
                        {
                            ToggledLockInterface(false);
                            isToggled = false;
                            islockon = false;
                            LockOnTargetPos = closestObj.transform.position;

                            return;
                        }

                        isToggled = true;
                        ToggledLockInterface(true);
                        LockOnTargetPos = closestObj.transform.position;
                    }
                }
                else
                {
                    islockon = false;
                    isToggled = false;
                    //cc.isStrafing = false;
                    //lockOnCam.gameObject.SetActive(false);
                    //freeCam.gameObject.SetActive(true);
                }
            }
            else
            {
                islockon = false;
                isToggled = false;
                //cc.isStrafing = false;
                AimInterface(false);
                LockInterface(false);
            }

            if(islockon || isToggled)
            {
                transform.LookAt(new Vector3(closestObj.transform.position.x, transform.position.y, closestObj.transform.position.z));
            }
        }
        else
        {
            islockon = false;
            isToggled = false;
            AimInterface(false);
            LockInterface(false);
            warplockon.color = Color.white;
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

    void ToggledLockInterface(bool state)
    {
        float size = state ? 1.0f : 2.0f;
        float fade = state ? 1.0f : 0.0f;
        lockon.DOFade(fade, .15f);
        lockon.transform.DOScale(size, .15f).SetEase(Ease.OutBack);

        aim.DOFade(fade, .15f);
    }

    public int targetIndex()
    {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; ++i)
        {

            if(TryGetSkinnedMeshRenderer(targets[i]))
            {
                if (targets[i].GetComponent<SkinnedMeshRenderer>().isVisible)
                    distances[i] = Vector3.Distance(targets[i].position, transform.position);
            }
            else
            {
                if (targets[i].GetComponent<MeshRenderer>().isVisible)
                    distances[i] = Vector3.Distance(targets[i].position, transform.position);
            }
            //if (targets[i].GetComponent<MeshRenderer>().isVisible)
            //    distances[i] = Vector3.Distance(targets[i].position, transform.position);
            //distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(targets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
            //distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(targets[i].position), new Vector2(transform.position.x, transform.position.z));

            //distances[i] = Vector3.Distance(Camera.main.WorldToScreenPoint(targets[i].position), transform.position);

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
     
    public void TurnoffLockOn()
    {
        //freeCam.gameObject.SetActive(true);
        //lockOnCam.gameObject.SetActive(false);
        //cc.isStrafing = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + transform.up, transform.forward);
    }

    bool TryGetSkinnedMeshRenderer(Transform obj)
    {
        return (obj.GetComponent<SkinnedMeshRenderer>() != null) ? true : false;
    }
}
