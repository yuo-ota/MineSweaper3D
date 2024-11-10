using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : SceneController
{
    [Header("data")]
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int _diggedGridNum;
    [SerializeField] private int _usedHintNum;
    [SerializeField] private int _bombNum;
    [SerializeField] private int _timer;
    [SerializeField] private int _score;
    [SerializeField] private int[,,] _stage;
    [SerializeField] private int[,,] _stageStatus;
    [SerializeField] private int _remainGridNum;
    [SerializeField] private bool _canMoveOtherPage;
    [SerializeField] private int _gameStatus;   //0:プレイしていない 1:プレイ中 2:失敗 3:クリア
    [SerializeField] private bool _isGameSetting = true;
    private float _milisec;
    [SerializeField] private bool _isEmphasize3Dview;
    [SerializeField] private bool _isExpand3Dview;
    [Header("gameObject")]
    [SerializeField] private GameObject _gameUIObject;
    [SerializeField] private GameObject _timerObject;
    [SerializeField] private GameObject _scoreObject;
    [SerializeField] private GameObject _setCubeObject;
    [SerializeField] private GameObject _setGridObject;
    [SerializeField] private GameObject _mouseControllObject;
    [SerializeField] private GameObject _resultScoreDisplayObject;
    [Header("textObject")]
    [SerializeField] private TextMeshProUGUI _displayScore;
    void Start()
    {
        BombNum = GameData.BombNum;
        MapSize = GameData.MapSize;
        DiggedGridNum = GameData.DiggedGridNum;
        UsedHintNum = GameData.UsedHintNum;
        Timer = GameData.Timer;
        Score = GameData.Score;
        Stage = GameData.Stage;
        StageStatus = GameData.StageStatus;
        GameStatus = GameData.GameStatus;
        _canMoveOtherPage = true;
        _setCubeObject.GetComponent<SetCube>().SettingPrefub(MapSize, Stage, StageStatus);
        _setGridObject.GetComponent<SetGrid>().SettingPrefub(MapSize, Stage, StageStatus);
        if (GameStatus == 2)
        {
            DefeatGame();
        }
        if (GameStatus == 3)
        {
            ClearGame();
        }
        _isGameSetting = false;
    }
    private void Update()
    {
        _milisec += Time.deltaTime;
        if (_milisec >= 1f)
        {
            _milisec = 0f;
            Timer++;
        }
    }
    override
    public void MoveScene(string sceneName)
    {
        if (!_canMoveOtherPage)
        {
            return;
        }
        GameData.BeforeSceneName = "Game";
        GameData.UsedHintNum = UsedHintNum;
        GameData.DiggedGridNum = DiggedGridNum;
        GameData.Timer = Timer;
        GameData.Score = Score;
        GameData.Stage = Stage;
        GameData.GameStatus = GameStatus;
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
    public bool IsGameSetting
    {
        get { return _isGameSetting; }
        set { _isGameSetting = value; }
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public int BombNum
    {
        get { return _bombNum; }
        set { _bombNum = value; }
    }
    public int DiggedGridNum
    {
        get { return _diggedGridNum; }
        set
        {
            _diggedGridNum = value;
            if (BombNum == MapSize[0] * MapSize[1] * MapSize[2] - DiggedGridNum)
            {
                ClearGame();
            }
        }
    }

    public int UsedHintNum
    {
        get { return _usedHintNum; }
        set
        {
            _usedHintNum = value;
        }
    }
    public int Timer
    {
        get { return _timer; }
        set 
        {
            _timer = value;
            UpdateTimer();
        }
    }
    public int Score
    {
        get { return _score; }
        set 
        {
            _score = value;
            UpdateScore();
        }
    }
    public bool IsEmphasize3Dview
    {
        get { return _isEmphasize3Dview; }
        set { _isEmphasize3Dview = value; }
    }
    public bool IsExpand3Dview
    {
        get { return _isExpand3Dview; }
        set 
        {
            _isExpand3Dview = value;
            UpdateCubeDist();
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
    public int GameStatus
    {
        get { return _gameStatus; }
        set { _gameStatus = value; }
    }
    public void UseHint()
    {
        if (_isGameSetting) return;
        _scoreObject.GetComponent<OperateScoreDetails>().UseHint();
        UsedHintNum++;
    }
    public void DiggedGrid()
    {
        if (_isGameSetting) return;
        _scoreObject.GetComponent<OperateScoreDetails>().DigAGrid();
        DiggedGridNum++;
    }
    public void UpdateScore()
    {
        _displayScore.text = (_score).ToString();
    }
    public void UpdateTimer()
    {
        _timerObject.GetComponent<DisplayTimer>().Timer = Timer;
    }
    public void UpdateCubeDist()
    {
        _setCubeObject.GetComponent<SetCube>().IsExpand = IsExpand3Dview;
    }
    public void ClearGame()
    {
        Debug.Log("clear");
        _mouseControllObject.GetComponent<MouseInput>().CanMouseInput = false;
        _canMoveOtherPage = false;
        GameStatus = 3;
        _gameUIObject.GetComponent<GameUI>().MoveResult();
        _setCubeObject.GetComponent<SetCube>().ActiveLayer = -1;
        _resultScoreDisplayObject.GetComponent<ResultScoreDisplay>().UpdateScore(DiggedGridNum, UsedHintNum, Timer, true);
        Invoke("CanMoveOtherPageTrue", 3f);
    }
    public void DefeatGame()
    {
        Debug.Log("Defeat");
        _mouseControllObject.GetComponent<MouseInput>().CanMouseInput = false;
        _canMoveOtherPage = false;
        GameStatus = 2;
        _gameUIObject.GetComponent<GameUI>().MoveResult();
        _setCubeObject.GetComponent<SetCube>().ActiveLayer = -1;
        _resultScoreDisplayObject.GetComponent<ResultScoreDisplay>().UpdateScore(DiggedGridNum, UsedHintNum, Timer, false);
        _setCubeObject.GetComponent<SetCube>().OpenCubes();
        Invoke("CanMoveOtherPageTrue", 3f);
    }
    public void CanMoveOtherPageTrue()
    {
        _canMoveOtherPage = true;
    }
}
