using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject imageObject;

    public void Interact()
    {
        Debug.Log("You will interact");
        if (imageObject != null)
        {
            imageObject.SetActive(!imageObject.activeSelf); // Toggle the image visibility
        }
    }
}