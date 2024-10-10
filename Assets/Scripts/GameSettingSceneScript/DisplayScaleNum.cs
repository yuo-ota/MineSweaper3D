using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayScaleNum : MonoBehaviour
{
    [SerializeField] private int[] _mapSize = new int[3];   //0:x, 1:y, 2,z
    [SerializeField] private GameObject _gameSettingControllerObject;
    [Header("textObject")]
    [SerializeField] private TextMeshProUGUI _displayXScale;
    [SerializeField] private TextMeshProUGUI _displayYScale;
    [SerializeField] private TextMeshProUGUI _displayZScale;
    [Header("SliderObject")]
    [SerializeField] private Slider _sliderX;
    [SerializeField] private Slider _sliderY;
    [SerializeField] private Slider _sliderZ;
    public void UpdateScale()
    {
        _gameSettingControllerObject.GetComponent<GameSettingController>().MapSize = MapSize;
    }
    public void SetXScale()
    {
        _mapSize[0] = (int)_sliderX.value;
        UpdateScale();
    }
    public void SetYScale()
    {
        _mapSize[1] = (int)_sliderY.value;
        UpdateScale();
    }
    public void SetZScale()
    {
        _mapSize[2] = (int)_sliderZ.value;
        UpdateScale();
    }
    public void UpdateText(int[] i)
    {
        _displayXScale.text = i[0].ToString();
        _displayYScale.text = i[1].ToString();
        _displayZScale.text = i[2].ToString();
    }
    public void UpdateSlider(int[] i)
    {
        _sliderX.value = i[0];
        _sliderY.value = i[1];
        _sliderZ.value = i[2];
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set
        {
            UpdateText(value);
            UpdateSlider(value);
        }
    }
}
