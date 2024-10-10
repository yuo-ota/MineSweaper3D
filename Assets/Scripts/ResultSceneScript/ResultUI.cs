using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject _resultControllerObject;
    public void goToExportCodeScene()
    {
        _resultControllerObject.GetComponent<ResultController>().MoveScene("ExportCode");
    }
    public void goToHomeScene()
    {
        _resultControllerObject.GetComponent<ResultController>().MoveScene("Home");
    }
    public void goToSettingScene()
    {
        _resultControllerObject.GetComponent<ResultController>().MoveScene("Setting");
    }
}
