using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlCheckBox : MonoBehaviour
{
    [SerializeField] private int _selectOption;
    [SerializeField] private bool[] _checkBoxStatus = { true, false, false, false };
    [Header("gameObject")]
    [SerializeField] private GameObject _exportCodeControllerObject;
    [SerializeField] private TMP_Dropdown _selectBoxObject;
    [SerializeField] private GameObject[] _checkBoxObject;
    [SerializeField] private GameObject[] _buttonObject;

    void Start()
    {
        UpdateCheckBox(); 
    }
    public int SelectOption
    {
        get { return _selectOption; }
        set
        {
            _selectOption = value;
            switch (value)
            {
                case 0:
                    _checkBoxStatus[0] = true;
                    _checkBoxStatus[1] = true;
                    _checkBoxStatus[2] = true;
                    _checkBoxStatus[3] = false;
                    break;
                case 1:
                    _checkBoxStatus[0] = true;
                    _checkBoxStatus[1] = false;
                    _checkBoxStatus[2] = true;
                    _checkBoxStatus[3] = false;
                    break;
                case 2:
                    _checkBoxStatus[0] = true;
                    _checkBoxStatus[1] = true;
                    _checkBoxStatus[2] = false;
                    _checkBoxStatus[3] = true;
                    break;
                default:
                    break;
            }
            UpdateCheckBox();
        }
    }
    public void UpdateCheckBox()
    {
        for (int i = 0; i < 4; i++)
        {
            _checkBoxObject[i].SetActive(_checkBoxStatus[i]);
        }
        _exportCodeControllerObject.GetComponent<ExportCodeController>().UpdateCodeText();
    }
    public void SetSelectBoxOption()
    {
        _selectBoxObject.value = 3;
        _exportCodeControllerObject.GetComponent<ExportCodeController>().UpdateCodeText();
    }
    public void SetBourdButton()
    {
        _checkBoxStatus[0] = !_checkBoxStatus[0];
        _checkBoxObject[0].SetActive(_checkBoxStatus[0]);
        SetSelectBoxOption();
    }
    public void SetPregressButton()
    {
        _checkBoxStatus[1] = !_checkBoxStatus[1];
        _checkBoxObject[1].SetActive(_checkBoxStatus[1]);
        SetSelectBoxOption();
    }
    public void SetTimerButton()
    {
        _checkBoxStatus[2] = !_checkBoxStatus[2];
        _checkBoxObject[2].SetActive(_checkBoxStatus[2]);
        SetSelectBoxOption();
    }
    public void SetStatusButton()
    {
        _checkBoxStatus[3] = !_checkBoxStatus[3];
        _checkBoxObject[3].SetActive(_checkBoxStatus[3]);
        SetSelectBoxOption();
    }
}
