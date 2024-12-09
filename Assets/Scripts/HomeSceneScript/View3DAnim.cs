using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class View3DAnim : MonoBehaviour
{
    [SerializeField] private int _layer;
    [SerializeField] private int _cubeStatus;
    [SerializeField] private GameObject _camObject;
    [SerializeField] private int[] _index;
    [SerializeField] private int _aroundBombNum;
    // Start is called before the first frame update
    void Start()
    {
        CamObject = transform.parent.parent.GetComponent<CubeAnim>().CamObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraAnim.Direction + transform.position);
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
                case 2: //äJé¶çœÇ›
                    transform.GetChild(0).gameObject.SetActive(true);
                    _cubeStatus = value;
                    transform.parent.GetChild(1).GetComponent<MeshRenderer>().material = transform.parent.parent.GetComponent<CubeAnim>().DiggedCubeMaterial;
                    break;
                default:
                    break;
            }
        }
    }
}
