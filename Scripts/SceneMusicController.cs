using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    public AudioClip sceneMusic;

    void Start()
    {
        if (AudioManager.instance != null && sceneMusic != null)
        {
            AudioManager.instance.ChangeMusic(sceneMusic);
        }
    }
}