using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ExportCodeController : SceneController
{
    [Header("data")]
    [SerializeField] private int _mapSeed;
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int[,,] _stage;
    [SerializeField] private int[,,] _stageStatus;
    [SerializeField] private int _diggedGridNum;
    [SerializeField] private int _usedHintNum;
    [SerializeField] private int _timer;
    [SerializeField] private int _score;
    [SerializeField] private int _gameStatus;
    [SerializeField] private bool _isEnglish;
    [Header("")]
    [SerializeField] private int _selectOption;
    [Header("gameObject")]
    [SerializeField] private GameObject _ExportCodeUIObject;
    [SerializeField] private GameObject _checkBoxControlObject;
    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;

    void Awake()
    {
        MapSeed = GameData.MapSeed;
        MapSize = GameData.MapSize;
        Stage = GameData.Stage;
        StageStatus = GameData.StageStatus;
        DiggedGridNum = GameData.DiggedGridNum;
        UsedHintNum = GameData.UsedHintNum;
        Timer = GameData.Timer;
        Score = GameData.Score;
        GameStatus = GameData.GameStatus;
        IsEnglish = GameData.IsEnglish;
    }
    override
    public void MoveScene(string sceneName)
    {
        GameData.BeforeSceneName = "ExportCode";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
    public void UpdateCodeText(string exportCode)
    {
        _ExportCodeUIObject.GetComponent<ExportCodeUI>().ExportCode = exportCode;   //ここでコードを生成する。
    }
    public int SelectOption
    {
        get { return _selectOption; }
        set
        {
            _selectOption = value;
            UpdateOption();
        }
    }
    public int MapSeed
    {
        get { return _mapSeed; }
        set { _mapSeed = value; }
    }

    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }

    public int[,,] Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }

    public int[,,] StageStatus
    {
        get { return _stageStatus; }
        set { _stageStatus = value; }
    }

    public int DiggedGridNum
    {
        get { return _diggedGridNum; }
        set { _diggedGridNum = value; }
    }

    public int UsedHintNum
    {
        get { return _usedHintNum; }
        set { _usedHintNum = value; }
    }

    public int Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    public int GameStatus
    {
        get { return _gameStatus; }
        set { _gameStatus = value; }
    }
    public bool IsEnglish
    {
        get { return _isEnglish; }
        set
        {
            _isEnglish = value;
            UpdateLanguage();
        }
    }
    public void UpdateOption()
    {
        _checkBoxControlObject.GetComponent<ControlCheckBox>().SelectOption = SelectOption;
    }
    public void UpdateLanguage()
    {
        if (IsEnglish)
        {
            foreach (GameObject g in _enTextObject)
            {
                g.SetActive(true);
            }
            foreach (GameObject g in _jpTextObject)
            {
                g.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject g in _enTextObject)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in _jpTextObject)
            {
                g.SetActive(true);
            }
        }
    }
}

