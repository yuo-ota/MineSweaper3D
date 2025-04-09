using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingController : SceneController
{
    [Header("data")]
    [SerializeField] private bool _isEnglish;
    [SerializeField] private int _volumeOfBgm;
    [SerializeField] private int _volumeOfSe;
    [SerializeField] private int _colorMode;
    [SerializeField] private AudioMixer _audioMixer;
    [Header("gameObject")]
    [SerializeField] private GameObject _langRadioBtnObject;
    [SerializeField] private GameObject _volumeDisplayUI;
    [SerializeField] private GameObject _colorRadioBtnObject;
    [SerializeField] private GameObject[] _enTextObject;
    [SerializeField] private GameObject[] _jpTextObject;

    [Header("Matrial")]
    [SerializeField] private Material[] _cubeMaterials;
    [Header("Color")]
    [SerializeField] private Color[,] _colorList = new Color[4, 5] {
                            { new Color(0.490566f, 0.002313996f, 0.01757187f), new Color(0.2156393f, 0.2294607f, 0.3292803f), new Color(1f, 0.206116f, 0.199f, 0.6313726f), new Color(0.4669811f, 0.8056048f, 1f, 0.6313726f), new Color(1f, 0.7160435f, 0.1372549f, 0.6313726f)},  //n
                            { new Color(0.4901961f, 0.4138939f, 0.003921577f), new Color(0.2156393f, 0.2294607f, 0.3292803f), new Color(0.4658573f, 0.2f, 0f, 0.6313726f), new Color(0.4669811f, 0.8056048f, 1f, 0.6313726f), new Color(1f, 0.7160435f, 0.1372549f, 0.6313726f)},   //p
                            { new Color(0.3852983f, 0.4901961f, 0.003921577f), new Color(0.2156393f, 0.2294607f, 0.3292803f), new Color(0f, 0.09611027f, 0.5660378f, 0.6313726f), new Color(0.4669811f, 0.8056048f, 1f, 0.6313726f), new Color(1f, 0.7160435f, 0.1372549f, 0.6313726f)},    //d
                            { new Color(0.490566f, 0.002313996f, 0.01757187f), new Color(0.2156393f, 0.2294607f, 0.3292803f), new Color(1f, 0.206116f, 0.199f, 0.6313726f), new Color(0.4669811f, 0.8056048f, 1f, 0.6313726f), new Color(0.2877739f, 0.1372549f, 1f, 0.6313726f)}
                        };
    // Start is called before the first frame update
    void Start()
    {
        IsEnglish = GameData.IsEnglish;
        VolumeOfBgm = GameData.VolumeOfBgm;
        VolumeOfSe = GameData.VolumeOfSe;
        ColorMode = GameData.ColorMode;
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
        AudioMixer.SetFloat("BGM_Volume", (float)VolumeOfBgm * 0.8f - 80);

    }
    public AudioMixer AudioMixer
    {
        get { return _audioMixer; }
        set { _audioMixer = value; }
    }
    public void UpdateSeVol()
    {
        _volumeDisplayUI.GetComponent<DisplayVolumeNum>().VolumeOfSe = VolumeOfSe;
        AudioMixer.SetFloat("SE_Volume", (float)VolumeOfSe * 0.8f - 60);
    }
    public void UpdateColorMode()
    {
        _colorRadioBtnObject.GetComponent<ActiveColorRadioBtn>().ColorMode = ColorMode;

        switch (ColorMode)
        {
            case 0:
                for (int i = 0; i < 5; i++)
                {
                    _cubeMaterials[i].SetColor("_Color", _colorList[0, i]);
                }
                break;
            case 1:
                for (int i = 0; i < 5; i++)
                {
                    _cubeMaterials[i].SetColor("_Color", _colorList[1, i]);
                }
                break;
            case 2:
                for (int i = 0; i < 5; i++)
                {
                    _cubeMaterials[i].SetColor("_Color", _colorList[2, i]);
                }
                break;
            case 3:
                for (int i = 0; i < 5; i++)
                {
                    _cubeMaterials[i].SetColor("_Color", _colorList[3, i]);
                }
                break;
        }
    }
}

