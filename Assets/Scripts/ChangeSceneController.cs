using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour, Interactable  // For objects that will change scene upon Interact (e.g. Tahanan)
{
    [SerializeField] private string sceneName;

    public void Interact()
    {
        Debug.Log("Change Scene?");
        if (!string.IsNullOrEmpty(sceneName))  // If sceneName != null/empty
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set for this interactable");
        }
    }
}