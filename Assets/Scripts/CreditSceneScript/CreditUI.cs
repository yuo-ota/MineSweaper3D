using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditUI : MonoBehaviour
{
    [SerializeField] private GameObject _creditControllerObject;
    public void gotoHome()
    {
        _creditControllerObject.GetComponent<CreditController>().MoveScene("Home");
    }
    public void gotoSetting()
    {
        _creditControllerObject.GetComponent<CreditController>().MoveScene("Setting");
    }
}
