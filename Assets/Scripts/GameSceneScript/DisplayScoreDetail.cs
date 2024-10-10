using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScoreDetail : MonoBehaviour
{
    [Header("data")]
    [SerializeField] private bool _hasNext;
    [SerializeField] private int _animStatus = 0;
    [Header("gameObject")]
    [SerializeField] private GameObject _nextObject;
    [SerializeField] private TextMeshProUGUI _addScoreObject;
    [SerializeField] private TextMeshProUGUI _detailScoreObject;
    [SerializeField] private GameObject _scoreDetailObject;
    [Header("animator")]
    [SerializeField] private Animator _scoreAnimator;
    public void StartAnim()
    {
        _scoreAnimator.SetBool("startAnim", true);
        _scoreDetailObject.GetComponent<Animator>().SetBool("startAnim", true);
    }
    public void PushAnim()
    {
        EndAnim();
    }
    public void EndAnim()
    {
        _scoreAnimator.SetBool("endAnim", true);
        _scoreDetailObject.GetComponent<Animator>().SetBool("endAnim", true);
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
        set { _detailScoreObject.text = value; }
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
        _scoreAnimator.SetBool("startAnim", false);
        _scoreDetailObject.GetComponent<Animator>().SetBool("startAnim", false);
        _animStatus = 1;
        if (HasNext)
        {
            PushAnim();
            transform.parent.GetComponent<OperateScoreDetails>().MoveNext(_nextObject);
        }
    }
}
