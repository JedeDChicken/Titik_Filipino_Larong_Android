using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, Interactable  // For scrollables...
{
    [SerializeField] private GameObject imageObject;

    public void Interact()
    {
        Debug.Log("You will interact for img");
        if (imageObject != null)
        {
            imageObject.SetActive(!imageObject.activeSelf);  // Toggle the image visibility
        }
    }
}