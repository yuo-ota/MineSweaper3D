using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleUI : MonoBehaviour
{
    [SerializeField] private string _exportCode;
    [SerializeField] private GameObject _ruleControllerObject;
    [SerializeField] private bool _isCopyDone;
    [SerializeReference] private GameObject[] _copyDoneText;

    private void Start()
    {
        IsCopyDone = false;
    }
    public bool IsCopyDone
    {
        get { return _isCopyDone; }
        set
        {
            _isCopyDone = value;
            _copyDoneText[0].SetActive(value);
            _copyDoneText[1].SetActive(value);
        }
    }
    public void CopyCode()
    {
        GUIUtility.systemCopyBuffer = _exportCode;
        IsCopyDone = true;
    }
    public void GotoHome()
    {
        _ruleControllerObject.GetComponent<RuleController>().MoveScene("Home");
    }
    public void GotoSetting()
    {
        _ruleControllerObject.GetComponent<RuleController>().MoveScene("Setting");
    }
    public void GoToPreviosPage()
    {
        int nowPage = _ruleControllerObject.GetComponent<RuleController>().PageNum;
        _ruleControllerObject.GetComponent<RuleController>().MovePage(nowPage - 1);
    }
    public void GoToNextPage()
    {
        int nowPage = _ruleControllerObject.GetComponent<RuleController>().PageNum;
        _ruleControllerObject.GetComponent<RuleController>().MovePage(nowPage + 1);
    }
}
