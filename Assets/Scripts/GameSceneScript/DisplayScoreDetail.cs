using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScoreDetail : MonoBehaviour
{
    [SerializeField] private GameObject _gameControllerObject;
    [Header("data")]
    [SerializeField] private bool _hasNext;
    [SerializeField] private int _animStatus = 0;
    [Header("gameObject")]
    [SerializeField] private GameObject _nextObject;
    [SerializeField] private TextMeshProUGUI _addScoreObject;
    [SerializeField] private TextMeshProUGUI[] _detailScoreObject;
    [SerializeField] private GameObject[] _scoreDetailObject;
    [Header("animator")]
    [SerializeField] private Animator[] _scoreAnimator;

    public GameObject GameControllerObject
    {
        set { _gameControllerObject = value; } 
    }
    public void StartAnim()
    {
        GetComponent<Animator>().SetBool("startAnim", true);
        if (_gameControllerObject.GetComponent<GameController>().IsEnglish)
        {
            _scoreAnimator[0].SetBool("startAnim", true);
        }
        else
        {
            _scoreAnimator[1].SetBool("startAnim", true);
        }
    }
        public void PushAnim()
    {
        EndAnim();
    }
    public void EndAnim()
    {
        GetComponent<Animator>().SetBool("endAnim", true);
        if (_gameControllerObject.GetComponent<GameController>().IsEnglish)
        {
            _scoreAnimator[0].SetBool("endAnim", true);
        }
        else
        {
            _scoreAnimator[1].SetBool("endAnim", true);
        }
        _animStatus = 2;
    }
    public void Delete()
    {
        transform.parent.GetComponent<OperateScoreDetails>().DestroyPrefub(this.gameObject);
    }
    public string AddScoreObject
    {
        set{ _addScoreObject.text = value; }
    }
    public string DetailScoreObject
    {
        set 
        {
            if (_gameControllerObject.GetComponent<GameController>().IsEnglish)
            {
                _scoreDetailObject[0].SetActive(true);
                _detailScoreObject[0].text = value;
            }
            else
            {
                _scoreDetailObject[1].SetActive(true);
                _detailScoreObject[1].text = value;
            }
        }
    }
    public bool HasNext
    {
        get { return _hasNext; }
        set
        {
            _hasNext = value;
            if (_animStatus == 1)
            {
                PushAnim();
                transform.parent.GetComponent<OperateScoreDetails>().MoveNext(_nextObject);
            }
            else if (_animStatus == 2)
            {
                transform.parent.GetComponent<OperateScoreDetails>().MoveNext(_nextObject);
            }
        }
    }
    public GameObject NextObject
    {
        get { return _nextObject; }
        set { _nextObject = value; }
    }
    public void SetAnimIdle()
    {
        GetComponent<Animator>().SetBool("startAnim", false);
        if (_gameControllerObject.GetComponent<GameController>().IsEnglish)
        {
            _scoreAnimator[0].SetBool("startAnim", false);
        }
        else
        {
            _scoreAnimator[1].SetBool("startAnim", false);
        }
        _animStatus = 1;
        if (HasNext)
        {
            PushAnim();
            transform.parent.GetComponent<OperateScoreDetails>().MoveNext(_nextObject);
        }
    }
}
