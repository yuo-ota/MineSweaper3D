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

    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;

    [SerializeField] private GameObject _titleAnimatorObject;
    [SerializeField] private GameObject _cubeControllObject;

    [Header("Matrial")]
    [SerializeField] private Material[] _cubeMaterials;[SerializeField]
    private Color[] _colorList = new Color[5] {
                            new Color(0.490566f, 0.002313996f, 0.01757187f), 
                            new Color(0.2156393f, 0.2294607f, 0.3292803f), 
                            new Color(1f, 0.206116f, 0.199f, 0.6313726f), 
                            new Color(0.4669811f, 0.8056048f, 1f, 0.6313726f), 
                            new Color(1f, 0.7160435f, 0.1372549f, 0.6313726f)
                        };  //n

    // Start is called before the first frame update
    void Start()
    {


        GameStatus = GameData.GameStatus;
        IsEnglish = GameData.IsEnglish;
        if (GameData.BeforeSceneName != null) Destroy(_titleAnimatorObject);
        else
        {
            for (int i = 0; i < 5; i++)
            {
                _cubeMaterials[i].SetColor("_Color", _colorList[i]);
            }
            _cubeControllObject.GetComponent<CubeAnim>().MakeCube();
        }
        IsOpenEscPanel = false;
        
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
