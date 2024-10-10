using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private GameObject _2dViewControlObject;
    [SerializeField] private GameObject _3dViewControlObject;
    [SerializeField] private GameObject _gridControlObject;
    [SerializeField] private GameObject _cubeControlObject;

    [SerializeField] private bool _pushLeftButtonIn3dView;
    [SerializeField] private Vector3 _pushLeftButtonPrePosition;
    [SerializeField] private bool _pushRightButtonIn3dView;
    [SerializeField] private Vector3 _pushRightButtonPrePosition;
    [SerializeField] private bool _canMouseInput = true;

    void Update()
    {
        //マウス左ボタン
        //マウスがどの位置で押されたかで2d, 3dを切り替える。
        if (Input.GetMouseButtonDown(0))
        {
            _pushLeftButtonPrePosition = Input.mousePosition;
            if (Input.mousePosition.x > 960) //y軸の制限を後々いれる。
            {
                _pushLeftButtonIn3dView = true;
            }
            else
            {
                _pushLeftButtonIn3dView = false;
                if (!_canMouseInput)
                {
                    return;
                }
                int[] index = _gridControlObject.GetComponent<SetGrid>().SearchIndex(Input.mousePosition.x, Input.mousePosition.y);
                if (index != null)
                {
                    _gridControlObject.GetComponent<SetGrid>().ChangeGrid(index, 2);
                    _cubeControlObject.GetComponent<SetCube>().ChangeCube(index, 2);
                }
            }
        }
        //3dで押されたかつ、押され続けている場合に入力を渡す。
        if (_pushLeftButtonIn3dView && Input.GetMouseButton(0))
        {
            _3dViewControlObject.GetComponent<CameraControl>().UpdateCamPosition(_pushLeftButtonPrePosition - Input.mousePosition);
            _pushLeftButtonPrePosition = Input.mousePosition;
        }

        //マウス右ボタン
        //マウスがどの位置で押されたかで2d, 3dを切り替える。
        if (Input.GetMouseButtonDown(1))
        {
            _pushRightButtonPrePosition = Input.mousePosition;
            if (Input.mousePosition.x > 960) //y軸の制限を後々いれる。
            {
                _pushRightButtonIn3dView = true;
            }
            else
            {
                _pushRightButtonIn3dView = false;
                if (!_canMouseInput)
                {
                    return;
                }
                int[] index = _gridControlObject.GetComponent<SetGrid>().SearchIndex(Input.mousePosition.x, Input.mousePosition.y);
                if (index != null)
                {
                    _gridControlObject.GetComponent<SetGrid>().ChangeGrid(index, 1);
                    _cubeControlObject.GetComponent<SetCube>().ChangeCube(index, 1);
                }
            }
        }
        //3dで押されたかつ、押され続けている場合に入力を渡す。
        if (_pushRightButtonIn3dView && Input.GetMouseButton(1))
        {
            _3dViewControlObject.GetComponent<CameraControl>().UpdateAnkerPosition(_pushRightButtonPrePosition - Input.mousePosition);
            _pushRightButtonPrePosition = Input.mousePosition;
        }
        //2dで押されたかつ、押され続けている場合に入力を渡す。
        else if (_pushRightButtonIn3dView && Input.GetMouseButton(1))
        {

        }

        //マウスホイール
        if (Input.mousePosition.x > 990 && Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            _3dViewControlObject.GetComponent<CameraControl>().UpdateCamLength(Input.GetAxis("Mouse ScrollWheel"));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0f && _canMouseInput)
        {
            _2dViewControlObject.GetComponent<SetGrid>().ActiveLayer = (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
        }
    }
    public bool CanMouseInput
    {
        set { _canMouseInput = value; }
    }
}
