using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayVolumeNum : MonoBehaviour
{
    [SerializeField] private int _volumeOfBgm;
    [SerializeField] private int _volumeOfSe;
    [SerializeField] private GameObject _settingControllerObject;
    [Header("textObject")]
    [SerializeField] private TextMeshProUGUI _displayBgmVol;
    [SerializeField] private TextMeshProUGUI _displaySeVol;
    [Header("SliderObject")]
    [SerializeField] private Slider _sliderBgm;
    [SerializeField] private Slider _sliderSe;
    public void UpdateBgmVolume()
    {
        _settingControllerObject.GetComponent<SettingController>().VolumeOfBgm = VolumeOfBgm;
    }
    public void UpdateSeVolume()
    {
        _settingControllerObject.GetComponent<SettingController>().VolumeOfSe = VolumeOfSe;
    }
    public void SetBgmVol()
    {
        VolumeOfBgm = (int)_sliderBgm.value;
        UpdateBgmVolume();
    }
    public void SetSeVol()
    {
        VolumeOfSe = (int)_sliderSe.value;
        UpdateSeVolume();
    }
    public int VolumeOfBgm
    {
        get { return _volumeOfBgm; }
        set
        {
            _volumeOfBgm = value;
            _displayBgmVol.text = value.ToString();
            _sliderBgm.value = value;
        }
    }
    public int VolumeOfSe
    {
        get { return _volumeOfSe; }
        set
        {
            _volumeOfSe = value;
            _displaySeVol.text = value.ToString();
            _sliderSe.value = value;
        }
    }
}
