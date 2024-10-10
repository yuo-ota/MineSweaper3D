using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLangRadioBtn : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject _signOfEnglish;
    [SerializeField] private GameObject _signOfJapanese;
    public bool IsEnglish
    {
        set 
        {
            UpdateRadioBtn(value);
        }
    }
    public void UpdateRadioBtn(bool b)
    {
        if (b)
        {
            _signOfEnglish.SetActive(true);
            _signOfJapanese.SetActive(false);
        }
        else
        {
            _signOfEnglish.SetActive(false);
            _signOfJapanese.SetActive(true);
        }
    }
}
