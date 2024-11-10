using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameControllerObject;
    [SerializeField] private GameObject _resultViewObject;
    [SerializeField] private GameObject _gameViewObject;

    void Start()
    {
        _resultViewObject = this.transform.GetChild(0).gameObject;
        _gameViewObject = transform.GetChild(1).gameObject;
    }
    public void GoToExportCodeScene()
    {
        _gameControllerObject.GetComponent<GameController>().MoveScene("ExportCode");
    }
    public void GoToHomeScene()
    {
        _gameControllerObject.GetComponent<GameController>().GameStatus = 0;
        _gameControllerObject.GetComponent<GameController>().MoveScene("Home");
    }
    public void GoToSettingScene()
    {
        _gameControllerObject.GetComponent<GameController>().MoveScene("Setting");
    }
    public void UpdateEmphasizeStatus()
    {
        _gameControllerObject.GetComponent<GameController>().IsEmphasize3Dview = !_gameControllerObject.GetComponent<GameController>().IsEmphasize3Dview;
    }
    public void UpdateExpandStatus()
    {
        _gameControllerObject.GetComponent<GameController>().IsExpand3Dview = !_gameControllerObject.GetComponent<GameController>().IsExpand3Dview;
    }
    public void UseHint()
    {
        _gameControllerObject.GetComponent<GameController>().UseHint();

    }
    public void MoveResult()
    {
        _resultViewObject.SetActive(true);
        _gameViewObject.SetActive(false);
    }
}
