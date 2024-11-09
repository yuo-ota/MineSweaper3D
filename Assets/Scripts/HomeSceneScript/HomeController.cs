using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class HomeController : SceneController
{
    [SerializeField] private int _gameStatus;
    [SerializeField] private GameObject _attentionObject;

    // Start is called before the first frame update
    void Start()
    {
        GameStatus = GameData.GameStatus;
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
    override
    public void MoveScene(string sceneName)
    {
        GameData.BeforeSceneName = "Home";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
}
