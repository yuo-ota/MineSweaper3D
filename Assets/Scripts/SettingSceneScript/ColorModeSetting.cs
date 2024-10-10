using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModeSetting : MonoBehaviour
{
    [Header("gameObject")]
    [SerializeField] private GameObject _settingControllerObject;
    public void NormalActive()
    {
        _settingControllerObject.GetComponent<SettingController>().ColorMode = 0;
    }
    public void ProtoActive()
    {
        _settingControllerObject.GetComponent<SettingController>().ColorMode = 1;
    }
    public void DeuterActive()
    {
        _settingControllerObject.GetComponent<SettingController>().ColorMode = 2;
    }
    public void TritaActive()
    {
        _settingControllerObject.GetComponent<SettingController>().ColorMode = 3;
    }
}
