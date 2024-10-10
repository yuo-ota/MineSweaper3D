using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSetting : MonoBehaviour
{
    [Header("gameObject")]
    [SerializeField] private GameObject _settingControllerObject;
    public void EnglishActive()
    {
        _settingControllerObject.GetComponent<SettingController>().IsEnglish = true;
    }
    public void JapaneseActive()
    {
        _settingControllerObject.GetComponent<SettingController>().IsEnglish = false;
    }
}
