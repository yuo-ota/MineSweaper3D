using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundScript : MonoBehaviour
{
    void Start()
    {
        if (GameData.CanMusicStart)
        {
            DontDestroyOnLoad(this);
            GameData.CanMusicStart = false;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
