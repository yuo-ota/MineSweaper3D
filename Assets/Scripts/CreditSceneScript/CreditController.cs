using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CreditController : SceneController
{
    [SerializeField] private bool _isEnglish;
    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;
    // Start is called before the first frame update
    void Start()
    {
        IsEnglish = GameData.IsEnglish;
    }

    // Update is called once per frame
    void Update()
    {
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
    override
    public void MoveScene(string sceneName)
    {
        GameData.BeforeSceneName = "Credit";
        //シーンのロード
        SceneManager.LoadScene(sceneName);
    }
}

