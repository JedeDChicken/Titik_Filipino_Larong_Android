using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // To access dialogues through the inspector, 
public class Dialog  // Collects the dialogs
{
    [SerializeField] List<string> lines;

    public List<string> Lines
    {
        get { return lines; }
    }
}