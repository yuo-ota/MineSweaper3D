using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnim : MonoBehaviour
{
    [SerializeField] private int i = 0;
    [Header("gameObject")]
    [SerializeField] private GameObject _prefubObject;
    [SerializeField] private GameObject _camObject;
    [SerializeField] private GameObject _titleAnimatorObject;
    [SerializeField] private GameObject _cameraAnimatorObject;
    [Header("Material")]
    [SerializeField] private Material _displayBombMaterial;
    [SerializeField] private Material _diggedCubeMaterial;

    [SerializeField] private int[] _mapSize;
    [SerializeField] private Vector3 _prefubPos = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _averagePos = new Vector3(0f, 0f, 0f);

    private int[] order = new int[26] { 25, 3, 20, 12, 5, 17, 4, 1, 0, 18, 10, 7, 11, 15, 19, 21, 14, 22, 6, 9, 8, 24, 26, 16, 2, 23 };


    public void SettingPrefub(int[] mapSize, int[,,] stage, int[,,] stageStatus)
    {
        /*MapSize = mapSize;
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
        _camObject.GetComponent<CameraControl>().ChangePosition();*/
    }
    public void MakeCube()
    {
        for (int i = 0; i < MapSize[0]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[2]; k++)
                {
                    GameObject newPrefub = Instantiate(_prefubObject, _prefubPos, Quaternion.identity);
                    newPrefub.transform.SetParent(transform, true);
                    newPrefub.transform.GetChild(2).GetComponent<View3DAnim>().SetText = 0;
                    newPrefub.transform.GetChild(2).GetComponent<View3DAnim>().CubeStatus = 0;

                    _averagePos += _prefubPos;
                    _prefubPos.x += 2f;
                }
                _prefubPos.x = 0f;
                _prefubPos.z += 2f;
            }
            _prefubPos.z = 0f;
            _prefubPos.y += 2f;
        }
        _averagePos /= (MapSize[0] * MapSize[1] * MapSize[2]);
        _camObject.GetComponent<CameraAnim>().Anker = _averagePos;
        _camObject.GetComponent<CameraAnim>().ChangePosition();

        transform.GetChild(13).GetChild(2).GetComponent<View3DAnim>().CubeStatus = 2;
        _cameraAnimatorObject.GetComponent<Animator>().SetTrigger("startTrigger");
        ChangeEnv();
    }
    public void ChangeEnv()
    {
        if (i == 26)
        {
            _titleAnimatorObject.GetComponent<Animator>().SetBool("startAnim", true);
            Invoke("DestroyCam", 1.5f);
            return;
        }
        transform.GetChild(13).GetChild(2).GetComponent<View3DAnim>().SetText = i + 1;
        transform.GetChild(order[i]).GetChild(1).GetComponent<MeshRenderer>().material = DisplayBombMaterial;
        i++;
        Invoke("ChangeEnv", .1f);
    }
    public GameObject CamObject
    {
        get { return _camObject; }
        set { _camObject = value; }
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public void ChangeCube(int[] index, int status)
    {
        transform.GetChild(index[2]).GetChild(index[1]).GetChild(index[0]).GetChild(2).GetComponent<View3DAnim>().CubeStatus = status;
    }
    public Material DisplayBombMaterial
    {
        get { return _displayBombMaterial; }
    }
    public Material DiggedCubeMaterial
    {
        get { return _diggedCubeMaterial; }
    }
    public void DestroyCam()
    {
        Destroy(_cameraAnimatorObject);
    }
}
