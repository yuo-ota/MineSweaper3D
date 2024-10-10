using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ResultController : SceneController
{
    [Header("data")]
    [SerializeField] private int _diggedGridNum;
    [SerializeField] private int _usedHintNum;
    [SerializeField] private int _timer;
    [SerializeField] private bool _isCleared;
    [Header("gameObject")]
    [SerializeField] private GameObject _displayScoreObject;

    void Start()
    {
        DiggedGridNum = GameStatus.DiggedGridNum;
        UsedHintNum = GameStatus.UsedHintNum;
        Timer = GameStatus.Timer;
        IsCleared = GameStatus.IsCleared;
        _displayScoreObject.GetComponent<ResultScoreDisplay>().UpdateScore(DiggedGridNum, UsedHintNum, Timer, IsCleared);
    }
    override
    public void MoveScene(string sceneName)
    {
        GameStatus.BeforeSceneName = "Result";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
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

    public bool IsCleared
    {
        get { return _isCleared; }
        set { _isCleared = value; }
    }
}
