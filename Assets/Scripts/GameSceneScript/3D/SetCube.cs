using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCube : MonoBehaviour
{
    [Header("gameObject")]
    [SerializeField] private GameObject _gameControllerObject;
    [SerializeField] private GameObject _prefubObject;
    [SerializeField] private GameObject _prefubParentObject;
    [SerializeField] private GameObject _camObject;
    [Header("Material")]
    [SerializeField] private Material _nonActiveLayerMaterial;
    [SerializeField] private Material _activeLayerMaterial;
    [SerializeField] private Material _displayFlagMaterial;
    [SerializeField] private Material _nonDisplayFlagMaterial;
    [SerializeField] private Material _displayBombMaterial;
    [SerializeField] private Material _digedCubeMaterial;
    [SerializeField] private Material _missFlagMaterial;
    [Header("data")]
    [SerializeField] private bool _isExpand;

    [SerializeField] private int _activeLayer;
    [SerializeField] private int[] _mapSize;
    [SerializeField] private Vector3 _prefubPos = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _averagePos = new Vector3(0f, 0f, 0f);

    public void SettingPrefub(int[] mapSize, int[,,] stage, int[,,] stageStatus)
    {
        MapSize = mapSize;
        for (int i = 0; i < mapSize[2]; i++)
        {
            GameObject newGrandParentPrefub = Instantiate(_prefubParentObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
            newGrandParentPrefub.transform.SetParent(transform, false);
            for (int j = 0; j < mapSize[1]; j++)
            {
                GameObject newParentPrefub = Instantiate(_prefubParentObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
                newParentPrefub.transform.SetParent(newGrandParentPrefub.transform, false);
                for (int k = 0; k < mapSize[0]; k++)
                {
                    GameObject newPrefub = Instantiate(_prefubObject, _prefubPos, Quaternion.identity); 
                    newPrefub.transform.SetParent(newParentPrefub.transform, true);
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().Layer = i;
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().SetText = stage[k, j, i];
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().Index = new int[3] { k, j, i };
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().CubeStatus = stageStatus[k, j, i];
                    _averagePos += _prefubPos;
                    _prefubPos.x += 2f;
                }
                _prefubPos.x = 0f;
                _prefubPos.z += 2f;
            }
            _prefubPos.z = 0f;
            _prefubPos.y += 2f;
        }
        _averagePos /= (mapSize[0] * mapSize[1] * mapSize[2]);
        _camObject.GetComponent<CameraControl>().Anker = _averagePos;
        _camObject.GetComponent<CameraControl>().AveragePos = _averagePos;
        _camObject.GetComponent<CameraControl>().ChangePosition();
    }
    public void ExpandPosition(float f)
    {
        if (MapSize == null) return;
        for (int i = 0; i < MapSize[2]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[0]; k++)
                {
                    Vector3 position = (transform.GetChild(i).GetChild(j).GetChild(k).transform.position - _averagePos) * f + _averagePos;
                    transform.GetChild(i).GetChild(j).GetChild(k).transform.position = position;
                }
            }
        }
    }
    public GameObject CamObject
    {
        get { return _camObject; }
        set { _camObject = value; }
    }
    public bool IsExpand
    {
        get { return _isExpand; }
        set
        {
            _isExpand = value;
            if (IsExpand)
            {
                ExpandPosition(3f);
            }
            else
            {
                ExpandPosition(1f / 3);
            }
        }
    }
    public int ActiveLayer
    {
        set { UpdateActiveLayer(value); }
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public void UpdateActiveLayer(int activeLayerNum)
    {
        if (MapSize == null) return;
        if (MapSize.Length == 0) return;
        for (int i = 0; i < MapSize[2]; i++)
        {
            if (i == activeLayerNum)
            {
                for (int j = 0; j < MapSize[1]; j++)
                {
                    for (int k = 0; k < MapSize[0]; k++)
                    {
                        transform.GetChild(i).GetChild(j).GetChild(k).GetChild(0).GetComponent<MeshRenderer>().material = _activeLayerMaterial;
                    }
                }
                continue;
            }
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[0]; k++)
                {
                    transform.GetChild(i).GetChild(j).GetChild(k).GetChild(0).GetComponent<MeshRenderer>().material = _nonActiveLayerMaterial;
                }
            }
        }
    }
    public void ChangeCube(int[] index, int status)
    {
        transform.GetChild(index[2]).GetChild(index[1]).GetChild(index[0]).GetChild(2).GetComponent<View3D>().CubeStatus = status;
    }
    public void ChangeCube(int a, int b, int c, int status)
    {
        transform.GetChild(c).GetChild(b).GetChild(a).GetChild(2).GetComponent<View3D>().CubeStatus = status;
    }
    public Material DisplayFlagMaterial
    {
        get { return _displayFlagMaterial; }
    }
    public Material NonDisplayFlagMaterial
    {
        get { return _nonDisplayFlagMaterial; }
    }
    public Material DisplayBombMaterial
    {
        get { return _displayBombMaterial; }
    }
    public Material DigedCubeMaterial
    {
        get { return _digedCubeMaterial; }
    }
    public Material MissFlagMaterial
    {
        get { return _missFlagMaterial; }
    }
    public int SearchDiggedCube()
    {
        int count = 0;
        for (int i = 0; i < MapSize[2]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[0]; k++)
                {
                    if (transform.GetChild(i).GetChild(j).GetChild(k).GetChild(2).GetComponent<View3D>().SearchDiggedCube()) count++;
                }
            }
        }
        return count;
    }
    public void OpenCubes()
    {
        if (MapSize == null) return;
        if (MapSize.Length == 0) return;

        for (int i = 0; i < MapSize[2]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[0]; k++)
                {
                    transform.GetChild(i).GetChild(j).GetChild(k).GetChild(2).GetComponent<View3D>().OpenCube();
                }
            }
        }
    }
}
