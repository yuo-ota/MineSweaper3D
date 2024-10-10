using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColorRadioBtn : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject _signOfNormal;
    [SerializeField] private GameObject _signOfProto;
    [SerializeField] private GameObject _signOfDeuter;
    [SerializeField] private GameObject _signOfTrita;
    
    public int ColorMode
    {
        set
        {
            UpdateRadioBtn(value);
        }
    }
    public void UpdateRadioBtn(int i)
    {
        switch (i)
        {
            case 1:
                _signOfNormal.SetActive(false);
                _signOfProto.SetActive(true);
                _signOfDeuter.SetActive(false);
                _signOfTrita.SetActive(false);
                break;
            case 2:
                _signOfNormal.SetActive(false);
                _signOfProto.SetActive(false);
                _signOfDeuter.SetActive(true);
                _signOfTrita.SetActive(false);
                break;
            case 3:
                _signOfNormal.SetActive(false);
                _signOfProto.SetActive(false);
                _signOfDeuter.SetActive(false);
                _signOfTrita.SetActive(true);
                break;
            default:
                _signOfNormal.SetActive(true);
                _signOfProto.SetActive(false);
                _signOfDeuter.SetActive(false);
                _signOfTrita.SetActive(false);
                break;

        }
    }
}
