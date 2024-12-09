using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = Unity.Mathematics.Random;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameSettingController : SceneController
{
    [Header("data")]
    [SerializeField] private bool _isUseCode;
    [SerializeField] private int _mapSeed;
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int[,,] _stage;
    [SerializeField] private int[,,] _stageStatus;
    [SerializeField] private Random random;
    [Header("need reset param")]
    [SerializeField] private int _score;
    [SerializeField] private int _usedHintNum;
    [SerializeField] private int _diggedGridNum;
    [SerializeField] private int _timer;
    [SerializeField] private int _gameStatus;
    [SerializeField] private bool _isEnglish;
    [Header("gameObject")]
    [SerializeField] private GameObject _scaleImageCube;
    [SerializeField] private GameObject _scaleDisplayUI;
    [SerializeField] private GameObject _exportCodeInputBox;
    [SerializeField] private GameObject[] _enableTextObject;
    [SerializeField] private GameObject[] _disableTextObject;

    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;
    // Start is called before the first frame update
    void Start()
    {
        IsOpenEscPanel = false;
        IsUseCode = false;
        random = new Random((uint)System.DateTime.Now.Ticks);
        MapSeed = GameData.MapSeed;
        MapSize = GameData.MapSize;
        IsEnglish = GameData.IsEnglish;
        DisplayCodeStatus(0);
    }
    override
    public void MoveScene(string sceneName)
    {
        if (sceneName == "Game")
        {
            ResetGame();
            GenerateMap();
            GameData.Score = Score;
            GameData.UsedHintNum = UsedHintNum;
            GameData.DiggedGridNum = DiggedGridNum;
            GameData.Timer = Timer;
            GameData.GameStatus = GameStatus;
        }
        GameData.MapSeed = MapSeed;
        GameData.BeforeSceneName = "GameSetting";
        GameData.MapSize = MapSize;
        GameData.StageStatus = StageStatus;
        GameData.Stage = Stage;
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
    public bool IsUseCode
    {
        get { return _isUseCode; }
        set { _isUseCode = value; }
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set
        {
            _mapSize = value;
            UpdateScale();
        }
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
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    public int UsedHintNum
    {
        get { return _usedHintNum; }
        set { _usedHintNum = value; }
    }
    public int DiggedGridNum
    {
        get { return _diggedGridNum; }
        set { _diggedGridNum = value; }
    }
    public int Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }
    public int GameStatus
    {
        get { return _gameStatus; }
        set { _gameStatus = value; }
    }
    public int MapSeed
    {
        get { return _mapSeed; }
        set { _mapSeed = value; }
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
    public void DisplayCodeStatus(int i) 
    {
        if (i == 0)
        {
            _enableTextObject[0].SetActive(false);
            _disableTextObject[0].SetActive(false);
            _enableTextObject[1].SetActive(false);
            _disableTextObject[1].SetActive(false);
        }
        else if (i == 1)
        {
            _enableTextObject[0].SetActive(true);
            _disableTextObject[0].SetActive(false);
            _enableTextObject[1].SetActive(true);
            _disableTextObject[1].SetActive(false);
        }
        else
        {
            _enableTextObject[0].SetActive(false);
            _disableTextObject[0].SetActive(true);
            _enableTextObject[1].SetActive(false);
            _disableTextObject[1].SetActive(true);
        }
    }
    //スケールが変更した際に変更を行うリスト
    public void UpdateScale()
    {
        _scaleImageCube.GetComponent<ScaleImage>().MapSize = MapSize;
        _scaleDisplayUI.GetComponent<DisplayScaleNum>().MapSize = MapSize;
    }
    public void GenerateMap()
    {
        if (!IsUseCode)
        {
            MapSeed = random.NextInt(4096) % 4096;
            Stage = new int[MapSize[0], MapSize[1], MapSize[2]];
            _stageStatus = new int[MapSize[0], MapSize[1], MapSize[2]];
            GetComponent<MakeMap>().Stage = Stage;
            GetComponent<MakeMap>().GenerateMap(MapSeed, MapSize, StageStatus);
        }
        else
        {
            MapSeed = _exportCodeInputBox.GetComponent<ControlInputField>().MapSeed;
            MapSize = _exportCodeInputBox.GetComponent<ControlInputField>().MapSize;
            Stage = new int[MapSize[0], MapSize[1], MapSize[2]];
            GetComponent<MakeMap>().Stage = Stage;
            GetComponent<MakeMap>().GenerateMap(MapSeed, MapSize);
            StageStatus = _exportCodeInputBox.GetComponent<ControlInputField>().StageStatus;
            UsedHintNum = _exportCodeInputBox.GetComponent<ControlInputField>().UseHintNum;
            Timer = _exportCodeInputBox.GetComponent<ControlInputField>().Timer;
        }
    }
    public void ResetGame()
    {
        Score = 0;
        DiggedGridNum = 0;
        UsedHintNum = 0;
        Timer = 0;
        GameStatus = 1;
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

