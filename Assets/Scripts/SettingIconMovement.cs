using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingIconMovement : MonoBehaviour
{
    [SerializeField] private Animator _gearMovementAnimator;
    public void OnMouseHover()
    {
        _gearMovementAnimator.SetTrigger("Hover");
    }
    public void OnMouseExit()
    {
        _gearMovementAnimator.SetTrigger("UnHover");
    }
}
    