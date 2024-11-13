using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ControlInputField : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_InputField>().onValueChanged.AddListener(OnValueChanged);
    }
    private void OnValueChanged(string text)
    {

        if (text == "") return;
        int optionKind = CheckOptionKind(text.Substring(0, 1));
        text = text.Substring(1);
        if (optionKind == -1) return;

        // progress data
        if (optionKind / 4 == 1)
        {
            optionKind /= 4;
            if (text.Length < 7) return;
            int[] bourdData = CheckBourdData(text);
            text = text.Substring(7);
            if (bourdData == null) return;
        }
        if (optionKind / 2 == 1)
        {
            optionKind /= 2;
        }
    }
    public int CheckOptionKind(string s)
    {
        if (int.TryParse(s, out int result))
        {
            if (result < 0 || result > 7)
            {
                result = -1;
            }
        }
        else
        {
            result = -1;
        }
        return result;
    }
    public int[] CheckBourdData(string s)
    {
        int[] result = new int[4];

        char[] mapSizeData = s.Substring(0, 4).ToCharArray();
        char[] mapSeedData = Convert.ToInt32(s.Substring(4, 3), 16).ToString("X").ToCharArray();

        int parity = 0;
        for (int i = 0; i < 3; i++) {
            result[i] = ConvertNumber(mapSizeData[i]);
            Debug.Log(result[i]);
            parity += result[i];
        }
        if (parity != ConvertNumber(mapSizeData[3])) return null;

        Array.Reverse(mapSeedData);
        result[3] = Convert.ToInt32(new string(mapSeedData), 16);
        return result;
    }
    public int ConvertNumber(char c)
    {
        return c - 'A';
    }
}