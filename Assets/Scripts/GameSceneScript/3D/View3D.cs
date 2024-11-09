using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class View3D : MonoBehaviour
{
    [SerializeField] private int _layer;
    [SerializeField] private int _cubeStatus;
    [SerializeField] private GameObject _camObject;
    [SerializeField] private int[] _index;
    [SerializeField] private int _aroundBombNum;
    // Start is called before the first frame update
    void Start()
    {
        CamObject = transform.parent.parent.parent.parent.GetComponent<SetCube>().CamObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraControl.Direction + transform.position);
    }
    public GameObject CamObject
    {
        get { return _camObject; }
        set { _camObject = value; }
    }
    public int SetText
    {
        set 
        {
            transform.GetChild(0).GetComponent<TextMeshPro>().text = value.ToString();
            _aroundBombNum = value;
        }
    }
    public int Layer
    {
        get { return _layer; }
        set { _layer = value; }
    }
    public int CubeStatus
    {
        get { return _cubeStatus; }
        set 
        {
            switch (value)
            {
                case 0: //ñ¢íÖéË
                    if (_cubeStatus == 0)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        _cubeStatus = value;
                    }
                    break;
                case 1: //ä¯ÇÃê›íu
                    if (_cubeStatus == 0)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        if (_aroundBombNum != 27)
                        {
                            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().DisplayFlagMaterial;
                            _cubeStatus = 4;
                        }
                        else
                        {
                            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().DisplayFlagMaterial;
                            _cubeStatus = 1;
                        }
                    }
                    else if (_cubeStatus == 1 || _cubeStatus == 4)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().NonDisplayFlagMaterial;
                        _cubeStatus = 0;
                    }
                    break;
                case 2: //äJé¶çœÇ›
                    if (_cubeStatus == 0 || _cubeStatus == 3)
                    {
                        if (_aroundBombNum == 27)
                        {
                            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().DisplayBombMaterial;
                        }
                        else if (_aroundBombNum == 0)
                        {
                            _cubeStatus = value;
                            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().DigedCubeMaterial;
                        }
                        else
                        {
                            transform.GetChild(0).gameObject.SetActive(true);
                            _cubeStatus = value;
                            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().DigedCubeMaterial;
                        }
                    }
                    break;
                default: //ä¯ÇÃåÎê›íu
                    break;
            }
        }
    }
    public int[] Index
    {
        get { return _index; }
        set { _index = value; }
    }
    public void OpenCube()
    {
        if (_aroundBombNum == 27 && CubeStatus != 1)
        {
            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().DisplayBombMaterial;
        }
        else if (CubeStatus == 4)
        {
            transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.parent.parent.GetComponent<SetCube>().MissFlagMaterial;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
