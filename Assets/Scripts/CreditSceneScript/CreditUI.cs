using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditUI : MonoBehaviour
{
    [SerializeField] private GameObject _creditControllerObject;
    public void gotoPreScene()
    {
        _creditControllerObject.GetComponent<CreditController>().MoveScene(GameData.BeforeSceneName);
    }
}
