using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public abstract class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _escapePanel;
    [SerializeField] private bool _isOpenEscPanel = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isOpenEscPanel) PanelOpen();
            else PanelClose();
        }
    }
    public void PanelOpen()
    {
        _isOpenEscPanel = true;
        _escapePanel.SetActive(true);
    }
    public void PanelClose()
    {
        _isOpenEscPanel = false;
        _escapePanel.SetActive(false);
    }
    public void EndGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        #else
            Application.Quit();//ゲームプレイ終了
        #endif
    }
    public bool IsOpenEscPanel
    {
        set { _isOpenEscPanel = value; }
        get { return _isOpenEscPanel; }
    }

    public abstract void MoveScene(string sceneName);
}
