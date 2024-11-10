using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class View2D : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textObject;
    [SerializeField] private int _aroundBombNum;
    [SerializeField] private int _gridStatus;
    [SerializeField] private int[] _index;
    [SerializeField] private int[] _mapSize;
    [SerializeField] private Sprite _displayFlagImage;
    [SerializeField] private Sprite _nonDisplayFlagImage;
    [SerializeField] private Sprite _digedImage;
    [SerializeField] private Sprite _bombDridImage;
    [SerializeField] private GameObject _gameControllerObject;

    public int SetText
    {
        set
        { 
            _textObject.text = value.ToString();
            _aroundBombNum = value;
        }
    }
    public int GridStatus
    {
        get { return _gridStatus; }
        set 
        {

            switch (value)
            {
                case 0: //未着手
                    if (_gridStatus == 1 || _gridStatus == 0)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        GetComponent<Image>().sprite = _nonDisplayFlagImage;
                        _gridStatus = value;
                        transform.parent.parent.parent.GetComponent<SetGrid>().ChangeGridStatus(Index, value);
;                    }
                    break;
                case 1: //旗の設置/解除
                    if (_gridStatus == 0)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        GetComponent<Image>().sprite = _displayFlagImage;
                        if (_aroundBombNum != 27)
                        {
                            transform.parent.parent.parent.GetComponent<SetGrid>().ChangeGridStatus(Index, 4);
                            _gridStatus = 4;
                        }
                        else
                        {
                            transform.parent.parent.parent.GetComponent<SetGrid>().ChangeGridStatus(Index, 1);
                            _gridStatus = 1;
                        }
                    }
                    else if (_gridStatus == 1 || _gridStatus == 4)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        _gridStatus = 0;
                        transform.parent.parent.parent.GetComponent<SetGrid>().ChangeGridStatus(Index, 0);
                        GetComponent<Image>().sprite = _nonDisplayFlagImage;
                    }
                    break;
                case 2: //開示処理
                    if (_gridStatus == 0 || _gridStatus == 3)
                    {
                        if (_aroundBombNum == 27)
                        {
                            GetComponent<Image>().sprite = _bombDridImage;
                            _gameControllerObject.GetComponent<GameController>().DefeatGame();
                        }
                        else if (_aroundBombNum == 0)
                        {
                            GetComponent<Image>().sprite = _digedImage;
                            _gameControllerObject.GetComponent<GameController>().DiggedGrid();
                            if (!_gameControllerObject.GetComponent<GameController>().IsGameSetting)
                            {
                                AutoOpen();
                            }
                        }
                        else
                        {
                            transform.GetChild(0).gameObject.SetActive(true);
                            GetComponent<Image>().sprite = _digedImage;
                            transform.parent.parent.parent.GetComponent<SetGrid>().ChangeGridStatus(Index, value);
                            _gameControllerObject.GetComponent<GameController>().DiggedGrid();
                            if (_gridStatus == 3 && !_gameControllerObject.GetComponent<GameController>().IsGameSetting)
                            {
                                AutoOpen();
                            }
                        }
                        _gridStatus = value;
                        transform.parent.parent.parent.GetComponent<SetGrid>().ChangeGridStatus(Index, value);
                    }
                    else if (_gridStatus == 2 && !_gameControllerObject.GetComponent<GameController>().IsGameSetting)
                    {
                        AutoOpen();
                    }
                    break;
                case 3: //開示待ち
                    if (_gridStatus == 0)
                    {
                        _gridStatus = value;
                        GridStatus = 2;
                    }
                    break;
                default: //旗の誤設置
                    break;
            }
        }
    }
    public int[] Index
    {
        get { return _index; }
        set { _index = value; }
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public int AroundBombNum
    {
        get { return _aroundBombNum; }
    }
    public GameObject GameControllerObject
    {
        get { return _gameControllerObject; }
        set { _gameControllerObject = value; }
    }
    public void AutoOpen()
    {
        _gridStatus = 2;
        if (_aroundBombNum == 0)
        {
            transform.parent.parent.parent.GetComponent<SetGrid>().AutoOpen(Index);
            return;
        }
        transform.parent.parent.parent.GetComponent<SetGrid>().SearchBombNum(Index);
    }
}