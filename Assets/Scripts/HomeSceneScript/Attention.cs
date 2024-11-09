using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour
{
    [SerializeField] private GameObject _homeControllerObject;
    private HomeController _homeControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        _homeControllerScript = _homeControllerObject.GetComponent<HomeController>();
    }
    void Update()
    {
    }

    public void CheckStatus()
    {
        if (_homeControllerScript.GameStatus == 1)
        {
            OnEnable();
        }
        else
        {
            OnDisable();
        }
    }
    public void OnEnable()
    {
        this.gameObject.SetActive(true);
    }
    public void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
}
