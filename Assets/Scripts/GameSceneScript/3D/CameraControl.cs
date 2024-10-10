using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _thetaX = 0;
    [SerializeField] private float _thetaY = 0;
    [SerializeField] private float _length = 50;
    [SerializeField] private Vector3 _anker = new Vector3(0, 0, 0);
    [SerializeField] private static Vector3 _direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position;
        position.x = Mathf.Cos(Mathf.Deg2Rad * _thetaX) * Mathf.Cos(Mathf.Deg2Rad * _thetaY);
        position.y = Mathf.Sin(Mathf.Deg2Rad * _thetaY);
        position.z = Mathf.Sin(Mathf.Deg2Rad * _thetaX) * Mathf.Cos(Mathf.Deg2Rad * _thetaY);
        position *= _length;
        transform.position = position + Anker;
        transform.LookAt(_anker);
        _direction = transform.position - Anker;
    }
    public Vector3 Anker
    {
        get { return _anker; }
        set { _anker = value; }
    }
    public static Vector3 Direction
    {
        get { return _direction; }
    }
    public void UpdateCamPosition(Vector3 v)
    {
        _thetaX += v.x * 0.2f;
        _thetaX %= 360;
        _thetaY += v.y * 0.2f;
        _thetaY = Mathf.Max(-89.9f, _thetaY);
        _thetaY = Mathf.Min(89.9f, _thetaY);
    }
    public void UpdateCamLength(float f)
    {
        _length -= f * 20;
        _length = Mathf.Max(0.1f, _length);
    }
    public void UpdateAnkerPosition(Vector3 v)
    {
        _anker += new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_thetaX + 90)), 0, Mathf.Sin(Mathf.Deg2Rad * (_thetaX + 90))) * v.x / 25;
        _anker += new Vector3(Mathf.Cos(Mathf.Deg2Rad * (_thetaX + 180)) * Mathf.Sin(Mathf.Deg2Rad * (_thetaY)), Mathf.Cos(Mathf.Deg2Rad * (_thetaY)), Mathf.Sin(Mathf.Deg2Rad * (_thetaX + 180)) * Mathf.Sin(Mathf.Deg2Rad * (_thetaY))) * v.y / 25;
    }
}
