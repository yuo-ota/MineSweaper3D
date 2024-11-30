using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        UpdateOption();
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
        UpdateOption();
    }
    public void SetSelectBoxOption()
    {
        _selectBoxObject.value = 3;
    }
    public void SetBourdButton()
    {
        _checkBoxStatus[0] = !_checkBoxStatus[0];
        _checkBoxObject[0].SetActive(_checkBoxStatus[0]);
        UpdateOption();
        SetSelectBoxOption();
    }
    public void SetPregressButton()
    {
        _checkBoxStatus[1] = !_checkBoxStatus[1];
        _checkBoxObject[1].SetActive(_checkBoxStatus[1]);
        UpdateOption();
        SetSelectBoxOption();
    }
    public void SetTimerButton()
    {
        _checkBoxStatus[2] = !_checkBoxStatus[2];
        _checkBoxObject[2].SetActive(_checkBoxStatus[2]);
        UpdateOption();
        SetSelectBoxOption();
    }
    public void SetStatusButton()
    {
        _checkBoxStatus[3] = !_checkBoxStatus[3];
        _checkBoxObject[3].SetActive(_checkBoxStatus[3]);
        UpdateOption();
        SetSelectBoxOption();
    }
    public void UpdateOption()
    {
        string exportCode = OptionkindString();
        exportCode += BourdDataString();
        if (_checkBoxStatus[1]) exportCode += ProgressDataString();
        if (_checkBoxStatus[2]) exportCode += TimerDataString();
        if (_checkBoxStatus[3]) exportCode += StatusDataString();
        _exportCodeControllerObject.GetComponent<ExportCodeController>().UpdateCodeText(exportCode);
    }
    public string OptionkindString()
    {
        int result = 0;
        // data select
        for (int i = 1; i < 4; i++)
        {
            if (_checkBoxStatus[i])
            {
                result += (int)Mathf.Pow(2, 3 - i);
            }
        }
        return result.ToString();
    }
    public string BourdDataString()
    {
        string result = "";
        int parity = 0;

        int[] MapSize = _exportCodeControllerObject.GetComponent<ExportCodeController>().MapSize;
        int MapSeed = _exportCodeControllerObject.GetComponent<ExportCodeController>().MapSeed;

        for (int i = 0; i < MapSize.Length; i++)
        {
            result += ConvertAlphabet(MapSize[i]);
            parity += MapSize[i];
        }

        result += ConvertAlphabet(parity);
        char[] editedMapSeed = MapSeed.ToString("X").ToCharArray();
        Array.Reverse(editedMapSeed);
        result += new string(editedMapSeed).PadRight(3, '0');

        return result;
    }
    public string ProgressDataString()
    {
        string result = "";

        int[,,] stageStatus = _exportCodeControllerObject.GetComponent<ExportCodeController>().StageStatus;
        int[] mapSize = _exportCodeControllerObject.GetComponent<ExportCodeController>().MapSize;
        int useHintNum = _exportCodeControllerObject.GetComponent<ExportCodeController>().UsedHintNum;

        int count = 0;
        int blockCount = 0;
        int number = 0;
        int parityNumber = 0;
        for (int i = 0; i < mapSize[0]; i++)
        {
            for (int j = 0; j < mapSize[1]; j++)
            {
                for (int k = 0; k < mapSize[2]; k++)
                {
                    number += stageStatus[i, j, k] * (int)(Mathf.Pow(5,count % 2));
                    parityNumber += stageStatus[i, j, k];
                    if (count % 2 == 1)
                    {
                        result += ConvertAlphabet(number);
                        if (blockCount % 3 == 2)
                        {
                            result += ConvertAlphabet(parityNumber);
                            parityNumber = 0;
                        }
                        number = 0;
                        blockCount++;
                    }
                    count++;
                }
            }
        }
        if (count % 2 == 1)
        {
            result += ConvertAlphabet(number);
        }
        result = RLE(result) + 'Z';
        result += useHintNum.ToString("X") + 'g';

        return result;
    }
    public string TimerDataString()
    {
        string result = "";

        int timer = _exportCodeControllerObject.GetComponent<ExportCodeController>().Timer;
        char[] editedTimer = timer.ToString().ToCharArray();
        Array.Reverse(editedTimer);
        result += new string(editedTimer);

        return result;
    }
    public string StatusDataString()
    {
        string result = "";

        int gameStatus = _exportCodeControllerObject.GetComponent<ExportCodeController>().GameStatus;
        if (gameStatus == 1) result = "a";
        else if (gameStatus == 2) result = "b";  //Ž¸”s
        else if (gameStatus == 3) result = "c"; //ƒNƒŠƒA

        return result;
    }
    public char ConvertAlphabet(int i)
    {
        return (char)('A' + i);
    }
    public string RLE(string s)
    {
        string result = "";
        int pivot = 0;

        char[] inputString = s.ToCharArray();

        while (true)
        {
            if (inputString.Length == pivot) return result;

            char activeChar = inputString[pivot];
            int count = 1;

            while (true)
            {
                if (inputString.Length == pivot + 1 || inputString[pivot + 1] != activeChar)
                {
                    if (count >= 2)
                    {
                        result += count;
                    }
                    result += activeChar;
                    pivot++;
                    break;
                }
                if (inputString[pivot + 1] == activeChar)
                {
                    pivot++;
                    count++;
                }
            }
        }
    }
}
