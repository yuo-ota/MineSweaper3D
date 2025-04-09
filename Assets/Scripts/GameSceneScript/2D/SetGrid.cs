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

    [SerializeField] private Vector3 _prefubPos = new Vector3(0f, 0f, 0f);

    [SerializeField] private int _activeLayer;
    [SerializeField] private int[] _mapSize;

    public void SettingPrefub(int[] mapSize, int[,,] stage, int[,,] stageStatus)
    {
        MapSize = mapSize;
        float fontSize = Mathf.Min((int)(GetComponent<RectTransform>().rect.width / mapSize[1]), (int)(GetComponent<RectTransform>().rect.width / mapSize[0])) / 2f; 
        float dx = 1f / mapSize[1];
        float dy = 1f / mapSize[0];

        for (int i = 0; i < mapSize[2]; i++)
        {
            GameObject newGrandParentPrefub = Instantiate(_prefubParentObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
            newGrandParentPrefub.transform.SetParent(transform, false);
            for (int j = 0; j < mapSize[1]; j++)
            {
                GameObject newParentPrefub = Instantiate(_prefubParentObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
                newParentPrefub.transform.SetParent(newGrandParentPrefub.transform, false);

                // 配置の設定
                newParentPrefub.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
                newParentPrefub.GetComponent<RectTransform>().anchorMin = new Vector2(_prefubPos.x, 0);
                newParentPrefub.GetComponent<RectTransform>().anchorMax = new Vector2(_prefubPos.x + dx, 1);
                newParentPrefub.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                newParentPrefub.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);

                for (int k = mapSize[0] - 1; k >= 0 ; k--)
                {
                    GameObject newPrefub = Instantiate(_prefubObject, _prefubPos, Quaternion.identity);
                    newPrefub.transform.SetParent(newParentPrefub.transform, false);

                    // 配置の設定
                    newParentPrefub.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
                    newPrefub.GetComponent<RectTransform>().anchorMin = new Vector2(0, _prefubPos.y);
                    newPrefub.GetComponent<RectTransform>().anchorMax = new Vector2(1, _prefubPos.y + dy);
                    newPrefub.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                    newPrefub.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);

                    newPrefub.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = fontSize;
                    newPrefub.GetComponent<View2D>().GameControllerObject = GameControllerObject;
                    newPrefub.GetComponent<View2D>().SetText = stage[k, j, i];
                    newPrefub.GetComponent<View2D>().Index = new int[3] { k, j, i };
                    newPrefub.GetComponent<View2D>().MapSize = MapSize;
                    newPrefub.GetComponent<View2D>().GridStatus = stageStatus[k, j, i];
                    _prefubPos.y += dy;
                }
                _prefubPos.y = 0f;
                _prefubPos.x += dx;
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
        for (int i = 0; i < MapSize[2]; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(false);
        }
        transform.GetChild(ActiveLayer).transform.gameObject.SetActive(true);
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
        int[] index = new int[3];
        if (gridPosition.x >= 0 && gridPosition.x < 700 && gridPosition.y >= 0 && gridPosition.y <= 700)
        {
            index[0] = MapSize[0] - (int)(gridPosition.y * MapSize[0] / 700f) - 1;
            index[1] = (int)(gridPosition.x * MapSize[1] / 700f);
            index[2] = ActiveLayer;
            return index;
        }
        return null;
    }
    public void ChangeGrid(int[] index, int status)
    {
        transform.GetChild(index[2]).GetChild(index[1]).GetChild(MapSize[0] - index[0] - 1).GetComponent<View2D>().GridStatus = status;
        _3dViewControlObject.GetComponent<SetCube>().ChangeCube(index, status);
    }
    public void ChangeGridStatus(int[] index, int value)
    {
        int[,,] status = _gameControllerObject.GetComponent<GameController>().StageStatus;
        status[index[0], index[1], index[2]] = value;
        _gameControllerObject.GetComponent<GameController>().StageStatus = status;
    }
    public void AutoOpen(int[] index)
    {
        for (int i = -1; i <= 1; i++)
        {
            if (index[0] + i < 0 || index[0] + i >= MapSize[0]) continue;
            for (int j = -1; j <= 1; j++)
            {
                if (index[1] + j < 0 || index[1] + j >= MapSize[1]) continue;
                for (int k = -1; k <= 1; k++)
                {
                    if (index[2] + k < 0 || index[2] + k >= MapSize[2]) continue;
                    _3dViewControlObject.GetComponent<SetCube>().ChangeCube(index[0] + i, index[1] + j, index[2] + k, 2);
                    if (_gameControllerObject.GetComponent<GameController>().GameStatus != 1) 
                    {
                        break;
                    }
                    transform.GetChild(index[2] + k).GetChild(index[1] + j).GetChild(MapSize[0] - (index[0] + i) - 1).GetComponent<View2D>().GridStatus = 3;
                }
            }
        }
    }
    public void SearchBombNum(int[] index)
    {
        int flagNum = 0;
        for (int i = -1; i <= 1; i++)
        {
            if (index[0] + i < 0 || index[0] + i >= MapSize[0]) continue;
            for (int j = -1; j <= 1; j++)
            {
                if (index[1] + j < 0 || index[1] + j >= MapSize[1]) continue;
                for (int k = -1; k <= 1; k++)
                {
                    if (index[2] + k < 0 || index[2] + k >= MapSize[2]) continue;
                    if (transform.GetChild(index[2] + k).GetChild(index[1] + j).GetChild(MapSize[0] - (index[0] + i) - 1).GetComponent<View2D>().GridStatus == 1 ||
                        transform.GetChild(index[2] + k).GetChild(index[1] + j).GetChild(MapSize[0] - (index[0] + i) - 1).GetComponent<View2D>().GridStatus == 4)
                    {
                        flagNum++;
                    }
                }
            }
        }
        if (flagNum >= transform.GetChild(index[2]).GetChild(index[1]).GetChild(MapSize[0] - index[0] - 1).GetComponent<View2D>().AroundBombNum)
        {
            AutoOpen(index);
        }
    }
}
