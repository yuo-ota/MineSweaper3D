using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameData : MonoBehaviour
{
    private static int _mapSeed;
    private static int[] _mapSize = {5, 5, 5};
    private static int[,,] _stage;
    private static int[,,] _stageStatus;
    private static int _score = 0;
    private static int _bombNum;
    private static int _diggedGridNum = 0;
    private static int _usedHintNum = 0;
    private static int _timer = 0;  //開始からの秒数
    private static bool _isEnglish = true;
    private static int _volumeOfBgm = 50;
    private static int _volumeOfSe = 50;
    private static int _colorMode = 0;  //0:正常色覚 1:1型2色覚 2:2型2色覚 3:3型2色覚
    private static string _beforeSceneName = null;
    private static int _gameStatus = 0;
    private static bool _isCleared = false;

    public static int MapSeed
    {
        get { return _mapSeed; }
        set { _mapSeed = value; }
    }
    public static int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public static int[,,] Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }
    public static int[,,] StageStatus
    { 
        get { return _stageStatus; }
        set { _stageStatus = value; }
    }
    public static int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    public static int BombNum
    {
        get { return _bombNum; }
        set { _bombNum = value; }
    }  
    public static int DiggedGridNum
    {
        get { return _diggedGridNum; }
        set { _diggedGridNum = value; }
    }
    public static int UsedHintNum
    {
        get { return _usedHintNum; }
        set { _usedHintNum = value; }
    }
    public static int Timer
    {
        get { return _timer; }
        set { 
            _timer = value;
        }
    }
    public static bool IsEnglish
    {
        get { return _isEnglish; }
        set { _isEnglish = value; }
    }
    public static int VolumeOfBgm
    {
        get { return _volumeOfBgm; }
        set { _volumeOfBgm = value; }
    }
    public static int VolumeOfSe
    {
        get { return _volumeOfSe; }
        set { _volumeOfSe = value; }
    }
    public static int ColorMode
    {
        get { return _colorMode; }
        set { _colorMode = value; }
    }
    public static string BeforeSceneName
    {
        get { return _beforeSceneName; }
        set { _beforeSceneName = value; }
    }
    public static int GameStatus
    {
        get { return _gameStatus; }
        set 
        {
            _gameStatus = value;
            Debug.Log(GameStatus);
        }
    }
    public static bool IsCleared
    {
        get { return _isCleared; }
        set { _isCleared = value; }
    }
}
