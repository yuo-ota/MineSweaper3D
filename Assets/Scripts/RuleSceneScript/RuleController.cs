using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RuleController : SceneController
{
    [Header("data")]
    [SerializeField] private bool _isEnglish;
    [SerializeField] private GameObject[] _pages;

    [Header("GameObject")]
    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;
    [SerializeField] private TextMeshProUGUI pageIndex;

    [SerializeField] private int _pageNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        IsOpenEscPanel = false;
        IsEnglish = GameData.IsEnglish;
        MovePage(0);
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
    public void MovePage(int i)
    {
        if (i < 0 || i >= _pages.Length)
        {
            MoveScene("Home");
            return;
        }

        _pages[_pageNum].SetActive(false);
        _pages[i].SetActive(true);
        _pageNum = i;

        pageIndex.SetText(_pageNum + 1 + "/" + _pages.Length);
    }
    override
    public void MoveScene(string sceneName)
    {
        GameData.BeforeSceneName = "Rule";
        SceneManager.LoadScene(sceneName);
    }
    public int PageNum
    {
        set 
        {
            _pageNum = value;

        }
        get { return _pageNum;}
    }
}
