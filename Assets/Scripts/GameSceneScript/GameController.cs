using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Random = Unity.Mathematics.Random;
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
    [SerializeField] private Random random;
    private float _milisec;
    [SerializeField] private bool _isEmphasize3Dview;
    [SerializeField] private bool _isExpand3Dview;
    [SerializeField] private bool _isEnglish;
    [Header("gameObject")]
    [SerializeField] private GameObject _gameUIObject;
    [SerializeField] private GameObject _timerObject;
    [SerializeField] private GameObject _scoreObject;
    [SerializeField] private GameObject _setCubeObject;
    [SerializeField] private GameObject _setGridObject;
    [SerializeField] private GameObject _mouseControllObject;
    [SerializeField] private GameObject _resultScoreDisplayObject;
    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;
    [Header("textObject")]
    [SerializeField] private TextMeshProUGUI _displayScore;
    void Start()
    {
        random = new Random((uint)System.DateTime.Now.Ticks);
        BombNum = GameData.BombNum;
        MapSize = GameData.MapSize;
        DiggedGridNum = GameData.DiggedGridNum;
        UsedHintNum = GameData.UsedHintNum;
        Timer = GameData.Timer;
        Score = GameData.Score;
        Stage = GameData.Stage;
        IsEnglish = GameData.IsEnglish;
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
        GameData.Score = Score;
        GameData.Stage = Stage;
        GameData.GameStatus = GameStatus;
        GameData.Timer = Timer;
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
        get {
            return _score;
        }
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
    public bool IsEnglish
    {
        get { return _isEnglish; }
        set
        {
            _isEnglish = value;
            UpdateLanguage();
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

        List<int[]> missFlagGridIndex = new List<int[]>();
        List<int[]> inactiveGridIndex = new List<int[]>();

        for (int i = 0; i < MapSize[0]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[2]; k++)
                {
                    if (StageStatus[i, j, k] == 4)
                    {
                        missFlagGridIndex.Add(new int[3]{ i, j, k });
                    }
                    else if (StageStatus[i, j, k] == 0)
                    {
                        inactiveGridIndex.Add(new int[3] { i, j, k });
                    }
                }
            }
        }
        int[] pickedIndex;
        if (missFlagGridIndex.Count > 0)
        {
            pickedIndex = missFlagGridIndex[random.NextInt(0, missFlagGridIndex.Count)];
            _setGridObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(MapSize[0] - pickedIndex[0] - 1).GetComponent<View2D>().GridStatus = 1;
            _setCubeObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(pickedIndex[0]).GetChild(2).GetComponent<View3D>().CubeStatus = 1;
            return;
        }
        pickedIndex = inactiveGridIndex[random.NextInt(0, inactiveGridIndex.Count)];
        if (_setGridObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(MapSize[0] - pickedIndex[0] - 1).GetComponent<View2D>().AroundBombNum == 27)
        {
            _setGridObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(MapSize[0] - pickedIndex[0] - 1).GetComponent<View2D>().GridStatus = 1;
            _setCubeObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(pickedIndex[0]).GetChild(2).GetComponent<View3D>().CubeStatus = 1;
        }
        else
        {
            _setGridObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(MapSize[0] - pickedIndex[0] - 1).GetComponent<View2D>().GridStatus = 2;
            _setCubeObject.transform.GetChild(pickedIndex[2]).GetChild(pickedIndex[1]).GetChild(pickedIndex[0]).GetChild(2).GetComponent<View3D>().CubeStatus = 2;
        }

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
        GameData.Timer = Timer;
        _mouseControllObject.GetComponent<MouseInput>().CanMouseInput = false;
        _canMoveOtherPage = false;
        GameStatus = 3;
        _gameUIObject.GetComponent<GameUI>().MoveResult();
        _setCubeObject.GetComponent<SetCube>().ActiveLayer = -1;
        DiggedGridNum = _setCubeObject.GetComponent<SetCube>().SearchDiggedCube();
        _resultScoreDisplayObject.GetComponent<ResultScoreDisplay>().UpdateScore(DiggedGridNum, UsedHintNum, Timer, true);
        Invoke("CanMoveOtherPageTrue", 0.1f);
    }
    public void DefeatGame()
    {
        Debug.Log("Defeat");
        GameData.Timer = Timer;
        _mouseControllObject.GetComponent<MouseInput>().CanMouseInput = false;
        _canMoveOtherPage = false;
        GameStatus = 2;
        _gameUIObject.GetComponent<GameUI>().MoveResult();
        _setCubeObject.GetComponent<SetCube>().ActiveLayer = -1;
        DiggedGridNum = _setCubeObject.GetComponent<SetCube>().SearchDiggedCube();
        _resultScoreDisplayObject.GetComponent<ResultScoreDisplay>().UpdateScore(DiggedGridNum, UsedHintNum, Timer, false);
        _setCubeObject.GetComponent<SetCube>().OpenCubes();
        Invoke("CanMoveOtherPageTrue", 0.1f);
    }
    public void CanMoveOtherPageTrue()
    {
        _canMoveOtherPage = true;
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
