using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetGrid : MonoBehaviour
{
    [SerializeField] private GameObject _gameControllerObject;
    [SerializeField] private GameObject _prefubObject;
    [SerializeField] private GameObject _prefubParentObject;
    [SerializeField] private GameObject _3dViewControlObject;

    [SerializeField] private List<GameObject> _parentObjects;

    [SerializeField] private Vector3 _prefubPos = new Vector3(0f, 0f, 0f);

    [SerializeField] private int _activeLayer;
    [SerializeField] private int[] _mapSize;

    public void SettingPrefub(int[] mapSize, int[,,] stage, int[,,] stageStatus)
    {
        MapSize = mapSize;
        for (int i = 0; i < mapSize[2]; i++)
        {
            GameObject newParentPrefub = Instantiate(_prefubParentObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
            newParentPrefub.transform.SetParent(transform, false);
            _parentObjects.Add(newParentPrefub);
            for (int j = 0; j < mapSize[1]; j++)
            {
                for (int k = mapSize[0] - 1; k >= 0 ; k--)
                {
                    GameObject newPrefub = Instantiate(_prefubObject, _prefubPos, Quaternion.identity);
                    newPrefub.transform.SetParent(newParentPrefub.transform, false);
                    newPrefub.GetComponent<RectTransform>().sizeDelta = new Vector2(700f / mapSize[1], 700f / mapSize[0]);
                    newPrefub.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = Mathf.Min((int)(700f / mapSize[0]), (int)(700f / mapSize[1])) / 2f;
                    newPrefub.GetComponent<View2D>().GameControllerObject = GameControllerObject;
                    newPrefub.GetComponent<View2D>().SetText = stage[k, j, i];
                    newPrefub.GetComponent<View2D>().Index = new int[3] { k, j, i };
                    newPrefub.GetComponent<View2D>().GridStatus = stageStatus[k, j, i];
                    _prefubPos.y += 700f / mapSize[0];
                }
                _prefubPos.y = 0f;
                _prefubPos.x += 700f / mapSize[1];
            }
            _prefubPos.x = 0f;
        }
        ActiveLayer = mapSize[2];
    }
    public GameObject GameControllerObject
    {
        get { return _gameControllerObject;  }
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public void UpdateActiveLayer()
    {
        _3dViewControlObject.GetComponent<SetCube>().ActiveLayer = ActiveLayer;
        foreach (GameObject g in _parentObjects)
        {
            g.SetActive(false);
        }
        _parentObjects[_activeLayer].SetActive(true);
    }
    public int ActiveLayer
    {
        get { return _activeLayer; }
        set
        {
            _activeLayer += value;
            _activeLayer = Mathf.Max(0, _activeLayer);
            _activeLayer = Mathf.Min(MapSize[2] - 1, _activeLayer);
            UpdateActiveLayer();
        }
    }
    public int[] SearchIndex(float x, float y)
    {
        Vector3 pivot = transform.position + transform.parent.position;
        Vector2 gridPosition = new Vector2(x - pivot.x, y - pivot.y);
        //Debug.Log(gridPosition);
        int[] index = new int[3];
        if (gridPosition.x >= 0 && gridPosition.x < 700 && gridPosition.y >= 0 && gridPosition.y <= 700)
        {
            index[0] = MapSize[1] - (int)(gridPosition.y * MapSize[1] / 700f) - 1;
            index[1] = (int)(gridPosition.x * MapSize[0] / 700f);
            index[2] = ActiveLayer;
            return index;
        }
        return null;
    }
    public void ChangeGrid(int[] index, int status)
    {
        for (int i = 0; i < transform.GetChild(index[2]).childCount; i++)
        {
            if (transform.GetChild(index[2]).GetChild(i).GetComponent<View2D>().Index[0] == index[0]
                && transform.GetChild(index[2]).GetChild(i).GetComponent<View2D>().Index[1] == index[1])
            {
                transform.GetChild(index[2]).GetChild(i).GetComponent<View2D>().GridStatus = status;
            }
        }
    }
    public void ChangeGridStatus(int[] index, int value)
    {
        int[,,] status = _gameControllerObject.GetComponent<GameController>().StageStatus;
        status[index[0], index[1], index[2]] = value;
        _gameControllerObject.GetComponent<GameController>().StageStatus = status;
    }
}
