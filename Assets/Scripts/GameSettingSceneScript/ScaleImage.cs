using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleImage : MonoBehaviour
{
    [SerializeField] private GameObject _gameSettingControllerObject;
    private GameSettingController _gameSettingControllerScript;
    [SerializeField] private int[] _mapSize = new int[3];
    private int _scaleRate = 10;
    // Start is called before the first frame update
    void Start()
    {
        _gameSettingControllerScript = _gameSettingControllerObject.GetComponent<GameSettingController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int[] MapSize
    {
        set
        {
            _mapSize = value;
            ScaleChange();
        }
    }
    public void ScaleChange()
    {
        transform.localScale = new Vector3(_mapSize[0], _mapSize[2], _mapSize[1]) * _scaleRate;
    }
}
