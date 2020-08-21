using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    #region Singleton

    public static DamagePopupManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Error ! Damage popup!");
            Destroy(gameObject);
        }

    }

    #endregion


    [SerializeField]
    private GameObject damagePopupPrefab;

    public void DisplayDamagePopup(float amount, Transform popupParent)
    {
        Debug.Log("popup");
        Vector3 position= new Vector3(popupParent.transform.position.x, popupParent.transform.position.y + 1.1f, popupParent.transform.position.z);
        GameObject damagePopup = Instantiate(damagePopupPrefab, position, Quaternion.identity, popupParent);
        damagePopup.GetComponent<DamagePopup>().SetUp(amount);
        damagePopup.SetActive(true);
    }
}
