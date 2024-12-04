using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField] private GameObject _2dViewControlObject;
    [SerializeField] private GameObject _3dViewControlObject;
    [SerializeField] private GameObject _gridControlObject;
    [SerializeField] private GameObject _cubeControlObject;

    [SerializeField] private bool _isMousePointerInLeft;
    [SerializeField] private bool _isMousePointerInRight;
    [SerializeField] private Vector3 _pushButtonPrePosition;

    [SerializeField] private bool _canMouseInput = true;
    public bool CanMouseInput
    {
        set { _canMouseInput = value; }
    }
    public void MouseClickIn3D()
    {
        _pushButtonPrePosition = Input.mousePosition;
    }
    public void MouseDragIn3D()
    {
        if (Input.GetMouseButton(0))
        {
            _3dViewControlObject.GetComponent<CameraControl>().UpdateCamPosition(_pushButtonPrePosition - Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            _3dViewControlObject.GetComponent<CameraControl>().UpdateAnkerPosition(_pushButtonPrePosition - Input.mousePosition);
        }
        _pushButtonPrePosition = Input.mousePosition;
    }
    public void ScrollIn2D()
    {
        _2dViewControlObject.GetComponent<SetGrid>().ActiveLayer = (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
    }
    public void ScrollIn3D()
    {
        _3dViewControlObject.GetComponent<CameraControl>().UpdateCamLength(Input.GetAxis("Mouse ScrollWheel"));
    }
}
