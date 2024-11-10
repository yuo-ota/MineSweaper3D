using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreDisplay : MonoBehaviour
{
    [SerializeField] private int _diggedScoreRate = 100;
    [SerializeField] private int _usedHintScoreRate = 200;
    [SerializeField] private int _timerMaxNum = 10000;
    [SerializeField] private int _timerScoreRate = 100;
    [SerializeField] private int _clearBonusNum = 1000;
    [SerializeField] private int _totalScore;
    [SerializeField] private GameObject _resultControllerObject;
    [SerializeField] private GameObject[] _displayElementObject;
    [SerializeField] private TextMeshProUGUI _scoreBodyTextObject;
    [SerializeField] private TextMeshProUGUI[] _displayNumTextObject;
    [SerializeField] private TextMeshProUGUI[] _displayCalcTextObject;

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
        Vector3 position = _displayElementObject[index].transform.position;
        position.y = i * (-44) + 609;
        _displayElementObject[index].transform.position = position;
    }
    public void SetDiggedText(int i)
    {
        _displayNumTextObject[0].text = i.ToString();
        _displayCalcTextObject[0].text = i.ToString() + " * " + _diggedScoreRate + " = " + (i * _diggedScoreRate) + "p";
        _totalScore += i * _diggedScoreRate;
    }
    public void SetHintText(int i)
    {
        _displayNumTextObject[1].text = i.ToString();
        _displayCalcTextObject[1].text = i.ToString() + " * " + _usedHintScoreRate + " = " + (i * _usedHintScoreRate) + "p";
        _totalScore -= i * _usedHintScoreRate;
    }
    public void SetTimerText(int i)
    {
        _displayNumTextObject[2].text = i.ToString() + "s";
        _displayCalcTextObject[2].text = "(" + _timerMaxNum + " - " + i.ToString() + ") * " + _timerScoreRate + " = " + ((_timerMaxNum - i) * _diggedScoreRate) + "p";
        _totalScore += (_timerMaxNum - i) * _diggedScoreRate;
    }
    public void SetClearText()
    {
        _displayCalcTextObject[3].text = _clearBonusNum + "p";
        _totalScore += _clearBonusNum;
    }
}
