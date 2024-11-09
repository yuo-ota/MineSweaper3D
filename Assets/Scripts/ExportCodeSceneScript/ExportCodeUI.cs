using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExportCodeUI : MonoBehaviour
{
    [Header("data")]
    [SerializeField] private string _exportCode;
    [SerializeField] private bool _isCopyDone;
    [Header("gamaObject")]
    [SerializeField] private GameObject _exportCodeControllerObject;
    [SerializeReference] private GameObject _copyDoneText;
    [SerializeField] private TMP_InputField _exportCodeText;

    private void Start()
    {
        IsCopyDone = false;
    }
    public void goToPreScene()
    {
        _exportCodeControllerObject.GetComponent<ExportCodeController>().MoveScene(GameData.BeforeSceneName);
    }
    public bool IsCopyDone
    {
        get { return _isCopyDone; }
        set
        {
            _isCopyDone = value;
            _copyDoneText.SetActive(value);
        }
    }
    public string ExportCode
    {
        get { return _exportCode; }
        set
        {
            _exportCode = value;
            _exportCodeText.text = value;
            IsCopyDone = false;
        }
    }
    public void CopyCode()
    {
        GUIUtility.systemCopyBuffer = _exportCode;
        IsCopyDone = true;
    }
}
