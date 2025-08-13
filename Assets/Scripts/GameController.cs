using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// States
public enum GameState { FreeRoam, Dialog, Interact };  // Enum- array that has diff values, can switch bet them

public class GameController : MonoBehaviour
{
    // Switches bet walking and dialogie/interacting?
    [SerializeField] PlayerMovement playerMovement;  // [S.F.]...- Exposes the playerMovement variable into the inspector
    GameState state;

    private void Start()  // Switches state?, set up event listener
    {
        DialogManager.Instance.OnShowDialog += () =>  // Lambda function, important
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    private void Update()  // Check/change state every frame
    {
        if (state == GameState.FreeRoam)
        {
            playerMovement.FixedUpdate();
        } else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        } else if (state == GameState.Interact)
        {
            
        }
    }
}
