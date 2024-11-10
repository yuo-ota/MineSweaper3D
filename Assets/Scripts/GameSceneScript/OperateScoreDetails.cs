using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateScoreDetails : MonoBehaviour
{
    [SerializeField] private GameObject _gameControllerObject;
    [SerializeField] private List<GameObject> _gameObjectList = new List<GameObject>();
    [SerializeField] private GameObject _prefubObject;
    [SerializeField] private int _scoreOfDig = 100;
    [SerializeField] private int _scoreOfHint = 200;

    private Vector3 _prefubPos = new Vector3(0f, 0f, 0f);
    public void DigAGrid()
    {
        SettingPrefub("+" + _scoreOfDig, "dig the grid");
        _gameControllerObject.GetComponent<GameController>().Score += _scoreOfDig;
    }
    public void UseHint()
    {
        SettingPrefub("-" + _scoreOfHint, "use a hint");
        _gameControllerObject.GetComponent<GameController>().Score -= _scoreOfHint;
    }
    public void SettingPrefub(string score, string reason)
    {
        GameObject newPrefub = Instantiate(_prefubObject, _prefubPos, Quaternion.identity);
        newPrefub.GetComponent<DisplayScoreDetail>().AddScoreObject = score;
        newPrefub.GetComponent<DisplayScoreDetail>().DetailScoreObject = reason;
        _gameObjectList.Add(newPrefub);
        newPrefub.transform.SetParent(transform, true);
        newPrefub.SetActive(false);
        if (_gameObjectList.Count == 1)
        {
            MoveNext(newPrefub);
        }
        CheckPreObject();
    }
    public void CheckPreObject()
    {
        if (_gameObjectList.Count > 1)
        {
            _gameObjectList[_gameObjectList.Count - 2].GetComponent<DisplayScoreDetail>().NextObject = _gameObjectList[_gameObjectList.Count - 1];
            _gameObjectList[_gameObjectList.Count - 2].GetComponent<DisplayScoreDetail>().HasNext = true;
        }
    }
    public void DestroyPrefub(GameObject g)
    {
        _gameObjectList.Remove(g);
        Destroy(g);
    }
    public void MoveNext(GameObject g)
    {
        g.SetActive(true);
        g.GetComponent<DisplayScoreDetail>().StartAnim();
    }
}
