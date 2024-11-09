using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private GameObject _settingControllerObject;
    public void goToPreScene()
    {
        _settingControllerObject.GetComponent<SettingController>().MoveScene(GameData.BeforeSceneName);
    }
}
