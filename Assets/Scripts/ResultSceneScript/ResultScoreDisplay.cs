using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreDisplay : MonoBehaviour
{
    [SerializeField] private int _diggedScoreRate = 100;
    [SerializeField] private int _usedHintScoreRate = 200;
    [SerializeField] private int _timerMaxNum = 5000;
    [SerializeField] private int _timerScoreRate = 100;
    [SerializeField] private int _clearBonusNum = 1000;
    [SerializeField] private int _totalScore;
    [SerializeField] private GameObject _resultControllerObject;
    [SerializeField] private GameObject[] _displayElementObject;
    [SerializeField] private TextMeshProUGUI _scoreBodyTextObject;

    public void UpdateScore(int diggedGridNum, int usedHintNum, int timer, bool isCleared)
    {
        _totalScore = 0;
        int elementNum = 0;
        if (diggedGridNum != 0)
        {
            SetPosition(0, elementNum);
            SetDiggedText(diggedGridNum);
            elementNum++;
        }
        if (usedHintNum != 0)
        {
            SetPosition(1, elementNum);
            SetHintText(usedHintNum);
            elementNum++;
        }
        if (isCleared)
        {
            if (timer < _timerMaxNum)
            {
                SetPosition(2, elementNum);
                SetTimerText(timer);
                elementNum++;
            }
            SetPosition(3, elementNum);
            SetClearText();
        }
        _scoreBodyTextObject.text = _totalScore.ToString() + "p";
    }
    public void SetPosition(int index, int i)
    {
        _displayElementObject[index].SetActive(true);
        _displayElementObject[index].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1f - 0.2f * i);
        _displayElementObject[index].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1f - 0.2f * i);
        _displayElementObject[index].GetComponent<RectTransform>().offsetMin = new Vector3(0f, 0f);

        _displayElementObject[index + 4].SetActive(true);
        _displayElementObject[index + 4].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1f - 0.2f * i);
        _displayElementObject[index + 4].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1f - 0.2f * i);
        _displayElementObject[index + 4].GetComponent<RectTransform>().offsetMin = new Vector3(0f, 0f);
    }
    public void SetDiggedText(int i)
    {
        _displayElementObject[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
        _displayElementObject[0].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + " * " + _diggedScoreRate + " = " + (i * _diggedScoreRate) + "p";
        _displayElementObject[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
        _displayElementObject[4].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + " * " + _diggedScoreRate + " = " + (i * _diggedScoreRate) + "p";
        _totalScore += i * _diggedScoreRate;
    }
    public void SetHintText(int i)
    {
        _displayElementObject[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
        _displayElementObject[1].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + " * " + _usedHintScoreRate + " = " + (i * _usedHintScoreRate) + "p";
        _displayElementObject[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
        _displayElementObject[5].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString() + " * " + _usedHintScoreRate + " = " + (i * _usedHintScoreRate) + "p";
        _totalScore -= i * _usedHintScoreRate;
    }
    public void SetTimerText(int i)
    {
        if (i / 60 > _timerMaxNum) return;
        _displayElementObject[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i / 60).ToString() + "m";
        _displayElementObject[2].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "(" + _timerMaxNum + " - " + (i / 60).ToString() + ") * " + _timerScoreRate + " = " + ((_timerMaxNum - (i / 60)) * _timerScoreRate) + "p";
        _displayElementObject[6].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i / 60).ToString() + "m";
        _displayElementObject[6].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "(" + _timerMaxNum + " - " + (i / 60).ToString() + ") * " + _timerScoreRate + " = " + ((_timerMaxNum - (i / 60)) * _timerScoreRate) + "p";
        _totalScore += (_timerMaxNum - (i / 60)) * _timerScoreRate;
    }
    public void SetClearText()
    {
        _displayElementObject[3].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _clearBonusNum + "p";
        _displayElementObject[7].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _clearBonusNum + "p";
        _totalScore += _clearBonusNum;
    }
}
