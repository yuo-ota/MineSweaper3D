using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnim : MonoBehaviour
{
    [SerializeField] private float _thetaX = 45f;
    [SerializeField] private float _thetaY = 45f;
    [SerializeField] private float _length = 50f;
    [SerializeField] private Vector3 _anker = new Vector3(0f, 0f, 0f);
    [SerializeField] private static Vector3 _direction;

    private void Update()
    {
        ChangePosition();
    }
    public float Length
    {
        get { return _length; }
        set { _length = value; }
    }
    public Vector3 Anker
    {
        get { return _anker; }
        set { _anker = value; }
    }
    public static Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    public void ChangePosition()
    {
        Vector3 position;
        position.x = Mathf.Cos(Mathf.Deg2Rad * _thetaX) * Mathf.Cos(Mathf.Deg2Rad * _thetaY);
        position.y = Mathf.Sin(Mathf.Deg2Rad * _thetaY);
        position.z = Mathf.Sin(Mathf.Deg2Rad * _thetaX) * Mathf.Cos(Mathf.Deg2Rad * _thetaY);
        Direction = position;
        position *= Length;
        transform.position = position + Anker;
        transform.LookAt(Anker);
    }
}

