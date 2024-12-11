using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _thetaX = 45f;
    [SerializeField] private float _thetaY = 45f;
    [SerializeField] private float _length = 50f;
    [SerializeField] private Vector3 _anker = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _averagePos = new Vector3(0f, 0f, 0f);
    [SerializeField] private static Vector3 _direction;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public float ThetaX
    {
        get { return _thetaX; }
        set { _thetaX = value; }
    }
    public float ThetaY
    {
        get { return _thetaY; }
        set { _thetaY = value; }
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
    public Vector3 AveragePos
    {
        get { return _averagePos; }
        set { _averagePos = value; }
    }
    public static Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    public void UpdateCamPosition(Vector3 v)
    {
        ThetaX += v.x * 0.2f;
        ThetaX %= 360;
        ThetaY += v.y * 0.2f;
        ThetaY = Mathf.Max(-89.9f, ThetaY);
        ThetaY = Mathf.Min(89.9f, ThetaY);
        ChangePosition();
    }
    public void UpdateCamLength(float f)
    {
        Length -= f * 20;
        Length = Mathf.Max(0.1f, Length);
        ChangePosition();
    }
    public void UpdateAnkerPosition(Vector3 v)
    {
        Anker += new Vector3(Mathf.Cos(Mathf.Deg2Rad * (ThetaX + 90)), 0, Mathf.Sin(Mathf.Deg2Rad * (ThetaX + 90))) * v.x / 25;
        Anker += new Vector3(Mathf.Cos(Mathf.Deg2Rad * (ThetaX + 180)) * Mathf.Sin(Mathf.Deg2Rad * (ThetaY)), Mathf.Cos(Mathf.Deg2Rad * (ThetaY)), Mathf.Sin(Mathf.Deg2Rad * (ThetaX + 180)) * Mathf.Sin(Mathf.Deg2Rad * (ThetaY))) * v.y / 25;
        ChangePosition();
    }
    public void InitPosition()
    {
        ThetaX = 45;
        ThetaY = 45;
        Length = 20;
        Anker = AveragePos;
        ChangePosition();
    }
    public void ChangePosition()
    {
        Vector3 position;
        position.x = Mathf.Cos(Mathf.Deg2Rad * ThetaX) * Mathf.Cos(Mathf.Deg2Rad * ThetaY);
        position.y = Mathf.Sin(Mathf.Deg2Rad * ThetaY);
        position.z = Mathf.Sin(Mathf.Deg2Rad * ThetaX) * Mathf.Cos(Mathf.Deg2Rad * ThetaY);
        Direction = position;
        position *= Length;
        transform.position = position + Anker;
        transform.LookAt(Anker);
    }
}