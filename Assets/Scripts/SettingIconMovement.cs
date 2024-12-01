using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingIconMovement : MonoBehaviour
{
    [SerializeField] private Animator _gearMovementAnimator;
    public void OnMouseHover()
    {
        Debug.Log("マウスがボタンの上に乗りました");
        _gearMovementAnimator.SetTrigger("Hover");
    }
    public void OnMouseExit()
    {
        Debug.Log("マウスがボタンの上から離れました");
        _gearMovementAnimator.SetTrigger("UnHover");
    }
}
    