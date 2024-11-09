using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCube : MonoBehaviour
{
    [Header("gameObject")]
    [SerializeField] private GameObject _gameControllerObject;
    [SerializeField] private GameObject _prefubObject;
    [SerializeField] private GameObject _camObject;
    [SerializeField] private List<GameObject> _cubeObjects;
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
    [SerializeField] private Vector3 _prefubPos = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _averagePos = new Vector3(0f, 0f, 0f);

    public void SettingPrefub(int[] mapSize, int[,,] stage, int[,,] stageStatus)
    {
        for (int i = 0; i < mapSize[0]; i++)
        {
            for (int j = 0; j < mapSize[1]; j++)
            {
                for (int k = 0; k < mapSize[2]; k++)
                {
                    GameObject newPrefub = Instantiate(_prefubObject, _prefubPos, Quaternion.identity); 
                    newPrefub.transform.SetParent(transform, true);
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().Layer = k;
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().SetText = stage[i, j, k];
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().Index = new int[3] { i, j, k };
                    newPrefub.transform.GetChild(2).GetComponent<View3D>().CubeStatus = stageStatus[i, j, k];
                    _averagePos += _prefubPos;
                    _cubeObjects.Add(newPrefub);
                    _prefubPos.y += 2f;
                }
                _prefubPos.y = 0f;
                _prefubPos.z += 2f;
            }
            _prefubPos.z = 0f;
            _prefubPos.x += 2f;
        }
        _averagePos /= (mapSize[0] * mapSize[1] * mapSize[2]);
        _camObject.GetComponent<CameraControl>().Anker = _averagePos;
        _camObject.GetComponent<CameraControl>().ChangePosition();
    }
    public void ExpandPosition(float f)
    {
        foreach (GameObject g in _cubeObjects)
        {
            Vector3 position = (g.transform.position - _averagePos) * f + _averagePos;
            g.transform.position = position;
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
    public void UpdateActiveLayer(int activeLayerNum)
    {
        for (int i = 0; i < _cubeObjects.Count; i++)
        {
            if (_cubeObjects[i].transform.GetChild(2).GetComponent<View3D>().Layer == activeLayerNum)
            {
                _cubeObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = _activeLayerMaterial;
            }
            else
            {
                _cubeObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().material = _nonActiveLayerMaterial;
            }
        }
    }
    public void ChangeCube(int[] index, int status)
    {
        foreach (GameObject g in _cubeObjects)
        {
            if (g.transform.GetChild(2).GetComponent<View3D>().Layer != index[2] ||
                g.transform.GetChild(2).GetComponent<View3D>().Index[0] != index[0] ||
                g.transform.GetChild(2).GetComponent<View3D>().Index[1] != index[1]) continue;
            g.transform.GetChild(2).GetComponent<View3D>().CubeStatus = status;
        }
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
    public void OpenCubes()
    {
        foreach (GameObject g in _cubeObjects)
        {
            g.transform.GetChild(2).GetComponent<View3D>().OpenCube();
        }
    }
}
