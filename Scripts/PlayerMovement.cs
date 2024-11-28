using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    private Animator animator;

    private Rigidbody2D rb;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    // public LayerMask ChangeSceneLayer;
    public float checkRadius = 0.1f;    //Adjustable

    // public bool interact;
    public int ButtonPress = 0;
    public bool isTalking = false;
    // public bool isBoss = false;

    private void Awake()    //Called when the script instance is being loaded (as soon as player is loaded)
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        // transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Calculate the target position based on movement direction
        Vector2 targetPos = rb.position + moveDirection * moveSpeed * Time.deltaTime;

        // Debug.Log("This is moveDirection.x: " + moveDirection.x);
        // Debug.Log("This is moveDirection.y: " + moveDirection.y);

        // Update animator parameters based on movement direction
        if (moveDirection != Vector2.zero)  // Player is moving
        {
            if (IsWalkable(targetPos))
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

    public void OnMoveUp(BaseEventData data)
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
        Debug.Log(ButtonPress);
        Debug.Log(isTalking);

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
        return !Physics2D.OverlapCircle(targetPos, checkRadius, solidObjectsLayer | interactableLayer);
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, checkRadius, interactableLayer);
        if (collider != null)
        {
            // Debug.Log("there is an interactable here");

            collider.GetComponent<Interactable>()?.Interact();

        }
    }

    //Interface- to handle diff types of interactables (e.g. challenger, NPC, merchant)
}