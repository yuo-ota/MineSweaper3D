using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SettingController : SceneController
{
    [Header("data")]
    [SerializeField] private bool _isEnglish;
    [SerializeField] private int _volumeOfBgm;
    [SerializeField] private int _volumeOfSe;
    [SerializeField] private int _colorMode;
    [Header("gameObject")]
    [SerializeField] private GameObject _langRadioBtnObject;
    [SerializeField] private GameObject _volumeDisplayUI;
    [SerializeField] private GameObject _colorRadioBtnObject;
    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;
    // Start is called before the first frame update
    void Start()
    {
        IsEnglish = GameData.IsEnglish;
        VolumeOfBgm = GameData.VolumeOfBgm;
        VolumeOfSe = GameData.VolumeOfSe;
        ColorMode = GameData.ColorMode;
    }

    // Update is called once per frame
    void Update()
    {
    }
    override
    public void MoveScene(string sceneName)
    {
        GameData.BeforeSceneName = "Setting";
        GameData.IsEnglish = IsEnglish;
        GameData.VolumeOfBgm = VolumeOfBgm;
        GameData.VolumeOfSe = VolumeOfSe;
        GameData.ColorMode = ColorMode;
        //シーンのロード
        SceneManager.LoadScene(sceneName);
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
    public int VolumeOfBgm
    {
        get { return _volumeOfBgm; }
        set
        {
            _volumeOfBgm = value;
            UpdateBgmVol();
        }
    }
    public int VolumeOfSe
    {
        get { return _volumeOfSe; }
        set 
        {
            _volumeOfSe = value;
            UpdateSeVol();
        }
    }
    public int ColorMode
    {
        get { return _colorMode; }
        set 
        {
            _colorMode = value;
            UpdateColorMode();
        }
    }
    public void UpdateLanguage()
    {
        _langRadioBtnObject.GetComponent<ActiveLangRadioBtn>().IsEnglish = IsEnglish;
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
    public void UpdateBgmVol()
    {
        _volumeDisplayUI.GetComponent<DisplayVolumeNum>().VolumeOfBgm = VolumeOfBgm;
    }
    public void UpdateSeVol()
    {
        _volumeDisplayUI.GetComponent<DisplayVolumeNum>().VolumeOfSe = VolumeOfSe;
    }
    public void UpdateColorMode()
    {
        _colorRadioBtnObject.GetComponent<ActiveColorRadioBtn>().ColorMode = ColorMode;
    }
}

