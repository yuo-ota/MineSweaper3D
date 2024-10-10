using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimer : MonoBehaviour
{
    [Header("textObject")]
    [SerializeField] private TextMeshProUGUI _displayMin;
    [SerializeField] private TextMeshProUGUI _displaySec;
    public int Timer
    {
        set
        {
            UpdateTimer(value);
        }
    }
    public void UpdateTimer(int t)
    {
        OutPutTime(_displayMin, t / 60);
        OutPutTime(_displaySec, t % 60);

    }
    public void OutPutTime(TextMeshProUGUI t, int i)
    {
        if (i < 10)
        {
            t.text = "0" + i;
            return;
        }
        t.text = i.ToString();
    }
}
