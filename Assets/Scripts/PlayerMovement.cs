using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
// MonoBehavior- base class from w/c every script component derives (inheritance?), allows script to be attachable to GameObjects, and have access to Unity's event methods (e.g. Start(), Update(), OnTriggerEnter...)
// Check AudioManager, BattleSystem, 
{
    public float moveSpeed = 5f;  // float 5
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;  // So player faces/retains direction when it stopped moving

    private Animator animator;

    private Rigidbody2D rb;
    public LayerMask solidObjectsLayer;  // Public so it can be reused w/ another object...
    public LayerMask interactableLayer;
    // public LayerMask ChangeSceneLayer;
    public float checkRadius = 0.1f;  // Adjustable

    // public bool interact;
    public int ButtonPress = 0;
    public bool isTalking = false;
    // public bool isBoss = false;

    private void Awake()  // Called when the script instance is being loaded (as soon as player is loaded), called once, before Start() (called once before Update() after all Awake() calls are done)
    {
        // Add these components to Player game object
        animator = GetComponent<Animator>();  // Animator Controller...
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()  // fixed intervals for p6 updates, Update()- every frame for gen logic
    {
        // transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Calculate the target position based on movement direction
        Vector2 targetPos = rb.position + moveDirection * moveSpeed * Time.deltaTime;  // deltaTime- interval in s from last frame to current, to match frame rate…

        // Debug.Log("This is moveDirection.x: " + moveDirection.x);
        // Debug.Log("This is moveDirection.y: " + moveDirection.y);

        // Update animator parameters based on movement direction
        if (moveDirection != Vector2.zero)  // Player is moving, !(0, 0)
        {
            if (IsWalkable(targetPos))  // If targetPos is walkable
            {
                rb.MovePosition(targetPos);
                lastMoveDirection = moveDirection;
                animator.SetFloat("moveX", moveDirection.x);
                animator.SetFloat("moveY", moveDirection.y);
                animator.SetBool("isMoving", true);
            }
        }
        else  // Player is idle
        {
            // Maintain the last direction values in the animator
            animator.SetFloat("moveX", lastMoveDirection.x);
            animator.SetFloat("moveY", lastMoveDirection.y);
            animator.SetBool("isMoving", false);
        }
    }

    // These are linked to buttons...
    public void OnMoveUp(BaseEventData data)  // Container for UI event info used by EventSystem...
    {
        moveDirection = Vector2.up;
    }

    public void OnMoveDown(BaseEventData data)
    {
        moveDirection = Vector2.down;
    }

    public void OnMoveLeft(BaseEventData data)
    {
        moveDirection = Vector2.left;
    }

    public void OnMoveRight(BaseEventData data)
    {
        moveDirection = Vector2.right;
    }

    public void OnStopMove(BaseEventData data)
    {
        moveDirection = Vector2.zero;
    }

    // public void OnInteract(BaseEventData data)
    // {
    //     // interact = true;
    //     isButtonPressed = true;
    //     Debug.Log("Button is pressed");
    //     Interact();
    //     isButtonPressed = false;
    //     Debug.Log("Button is unpressed");
    // }

    public void OnInteractDown(BaseEventData data)
    {
        // if (!isButtonPressed)
        // {
        // Debug.Log("Button is pressed");
        Interact();
            // isButtonPressed = false;
            // Debug.Log("Button is unpressed");
        // }
        if (isTalking)
        {
            ButtonPress += 1;
        }
        // Debug.Log(ButtonPress);
        // Debug.Log(isTalking);

    }

    // public void OnInteractUp(BaseEventData data)
    // {
    //     if (!isButtonPressed)
    //     {
    //         isButtonPressed = false;
    //         Debug.Log("Button is unpressed");
    //     }
    // }

    private bool IsWalkable(Vector2 targetPos)
    {
        // if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        // {
        //     return false;
        // }
        // return true;

        // Check if there are any colliders in the target position
        return !Physics2D.OverlapCircle(targetPos, checkRadius, solidObjectsLayer | interactableLayer);  // null- false, not null- true, !null- true
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));  // From animator moveX and moveY earlier
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);  // Can only be seen on Scene, when OnInteractDown...

        var collider = Physics2D.OverlapCircle(interactPos, checkRadius, interactableLayer);
        if (collider != null)  // If there's something there
        {
            // Debug.Log("there is an interactable here");

            // Look for a component on the object that implements the Interactable class/interface
            // ?. (Null-Conditional Operator)- only call next method if component exists
            // If collider has Interactable component (if collider is an interactable), then call Interact() 
            collider.GetComponent<Interactable>()?.Interact();

        } 
    }

    // Interface- to handle diff types of interactables (e.g. challenger, NPC, merchant)
}