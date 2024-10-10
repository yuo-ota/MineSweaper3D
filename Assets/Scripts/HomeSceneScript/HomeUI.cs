using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private GameObject _homeControllerObject;
    private HomeController _homeControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        _homeControllerScript = _homeControllerObject.GetComponent<HomeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToSetting()
    {
        _homeControllerScript.MoveScene("Setting");
    }
    public void goToRule()
    {
        _homeControllerScript.MoveScene("Rule");
    }
    public void goToExplain()
    {
        _homeControllerScript.MoveScene("Explain");
    }
    public void goToGameSetting()
    {
        _homeControllerScript.MoveScene("GameSetting");
    }
    public void goToCredit()
    {
        _homeControllerScript.MoveScene("Credit");
    }
    public void goToGame()
    {
        _homeControllerScript.MoveScene("Game");
    }
}
