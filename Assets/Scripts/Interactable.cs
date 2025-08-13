using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable  // Interface- to handle diff types of interactables (e.g. challenger, NPC, merchant)
{
    void Interact();  // To have diff scripts w/ diff Interact() behaviors
}