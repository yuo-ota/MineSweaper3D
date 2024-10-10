using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameSettingControllerObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void goToHome()
    {
        _gameSettingControllerObject.GetComponent<GameSettingController>().MoveScene("Home");
    }
    public void goToGame()
    {
        _gameSettingControllerObject.GetComponent<GameSettingController>().MoveScene("Game");
    }
}
