using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class HomeController : SceneController
{
    [SerializeField] private int _gameStatus;
    [SerializeField] private bool _isEnglish;
    [SerializeField] private GameObject _attentionObject;

    [SerializeField] private GameObject _enTextObject;
    [SerializeField] private GameObject _jpTextObject;

    // Start is called before the first frame update
    void Start()
    {
        GameStatus = GameData.GameStatus;
        IsEnglish = GameData.IsEnglish;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public int GameStatus
    {
        get { return _gameStatus; }
        set {
            _gameStatus = value;
            _attentionObject.GetComponent<Attention>().CheckStatus();
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
    override
    public void MoveScene(string sceneName)
    {
        GameData.BeforeSceneName = "Home";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
    public void UpdateLanguage()
    {
        if (IsEnglish)
        {
            _enTextObject.SetActive(true);
            _jpTextObject.SetActive(false);
        }
        else
        {
            _enTextObject.SetActive(false);
            _jpTextObject.SetActive(true);
        }
    }
}
