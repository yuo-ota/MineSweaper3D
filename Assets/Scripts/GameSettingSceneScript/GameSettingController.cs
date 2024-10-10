using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameSettingController : SceneController
{
    [Header("data")]
    [SerializeField] private int _mapSeed = 0;
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int[,,] _stage;
    [SerializeField] private int[,,] _stageStatus;
    [Header("gameObject")]
    [SerializeField] private GameObject _scaleImageCube;
    [SerializeField] private GameObject _scaleDisplayUI;
    // Start is called before the first frame update
    void Start()
    {
        MapSize = GameStatus.MapSize;
    }
    override
    public void MoveScene(string sceneName)
    {
        GenerateMap();
        GameStatus.BeforeSceneName = "GameSetting";
        GameStatus.MapSize = MapSize;
        GameStatus.StageStatus = StageStatus;
        GameStatus.Stage = Stage;
        GameStatus.MapSeed = MapSeed;
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
    public int MapSeed
    {
        get { return _mapSeed; }
        set { _mapSeed = value; }
    }
    //スケールが変更した際に変更を行うリスト
    public void UpdateScale()
    {
        _scaleImageCube.GetComponent<ScaleImage>().MapSize = MapSize;
        _scaleDisplayUI.GetComponent<DisplayScaleNum>().MapSize = MapSize;
    }
    public void GenerateMap()
    {
        _stage = new int[MapSize[0], MapSize[1], MapSize[2]];
        _stageStatus = new int[MapSize[0], MapSize[1], MapSize[2]];
        GetComponent<MakeMap>().Stage = Stage;
        GetComponent<MakeMap>().GenerateMap(MapSeed, MapSize, StageStatus);
    }
}

