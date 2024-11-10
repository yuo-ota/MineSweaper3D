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
    [SerializeField] private int _mapSeed;
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int[,,] _stage;
    [SerializeField] private int[,,] _stageStatus;
    [SerializeField] private Random random;
    [Header("need reset param")]
    [SerializeField] private int _score;
    [SerializeField] private int _usedHintNum;
    [SerializeField] private int _timer;
    [SerializeField] private int _gameStatus;
    [Header("gameObject")]
    [SerializeField] private GameObject _scaleImageCube;
    [SerializeField] private GameObject _scaleDisplayUI;
    // Start is called before the first frame update
    void Start()
    {
        random = new Random((uint)System.DateTime.Now.Ticks);
        MapSeed = GameData.MapSeed;
        MapSize = GameData.MapSize;
    }
    override
    public void MoveScene(string sceneName)
    {
        if (sceneName == "Game")
        {
            GenerateMap();
            ResetGame();
            GameData.Score = Score;
            GameData.UsedHintNum = UsedHintNum;
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
        set { 
            if (MapSeed == 0)
            {
                _mapSeed = value;
            }
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
        MapSeed = random.NextInt();
        _stage = new int[MapSize[0], MapSize[1], MapSize[2]];
        _stageStatus = new int[MapSize[0], MapSize[1], MapSize[2]];
        GetComponent<MakeMap>().Stage = Stage;
        GetComponent<MakeMap>().GenerateMap(MapSeed, MapSize, StageStatus);
    }
    public void ResetGame()
    {
        Score = 0;
        UsedHintNum = 0;
        Timer = 0;
        GameStatus = 1;
    }
}

