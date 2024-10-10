using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class HomeController : SceneController
{
    [SerializeField] private bool _isInProgress;
    [SerializeField] private GameObject _attentionObject;

    // Start is called before the first frame update
    void Start()
    {
        IsInProgress = GameStatus.IsInProgress;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public bool IsInProgress
    {
        get { return _isInProgress; }
        set {
            Debug.Log(value);
            _isInProgress = value;
            _attentionObject.GetComponent<Attention>().CheckStatus();
        }
    }
    override
    public void MoveScene(string sceneName)
    {
        GameStatus.BeforeSceneName = "Home";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
}
