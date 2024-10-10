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
    [Header("")]
    [SerializeField] private int _selectOption;
    [Header("gameObject")]
    [SerializeField] private GameObject _ExportCodeUIObject;
    [SerializeField] private GameObject _checkBoxControlObject;

    private void Start()
    {
        MapSeed = GameStatus.MapSeed;
        MapSize = GameStatus.MapSize;
        Stage = GameStatus.Stage;
        StageStatus = GameStatus.StageStatus;
        DiggedGridNum = GameStatus.DiggedGridNum;
        UsedHintNum = GameStatus.UsedHintNum;
        Timer = GameStatus.Timer;
        Score = GameStatus.Score;
    }
    override
    public void MoveScene(string sceneName)
    {
        GameStatus.BeforeSceneName = "ExportCode";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
    public void UpdateCodeText()
    {
        _ExportCodeUIObject.GetComponent<ExportCodeUI>().ExportCode = "aaaa";   //ここでコードを生成する。
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
    public void UpdateOption()
    {
        _checkBoxControlObject.GetComponent<ControlCheckBox>().SelectOption = SelectOption;
    }
}

