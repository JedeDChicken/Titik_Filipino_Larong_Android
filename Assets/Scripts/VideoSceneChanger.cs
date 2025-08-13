using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneChanger : MonoBehaviour  // From scene w/ video to another scene as vid finishes
{
    public VideoPlayer videoPlayer;  // Reference to the Video Player component
    [SerializeField] string nextSceneName;  // The name of the scene to load after the video ends

    void Start()
    {
        // Ensure the VideoPlayer is assigned
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Register the event to call when the video finishes playing
        videoPlayer.loopPointReached += OnVideoFinished;  // +/-=- to subscribe/unsubscribe a method to an event (add/remove method on event's list of methods to call)
    }

    // Method to change the scene
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}