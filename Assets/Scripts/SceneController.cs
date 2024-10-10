using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public abstract class SceneController : MonoBehaviour
{
    public abstract void MoveScene(string sceneName);
}
