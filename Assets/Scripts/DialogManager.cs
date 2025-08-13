using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For Text
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond;

    // [SerializeField] Button nextButton;  // Reference to the button

    public event Action OnShowDialog;  // Event & Action- 
    public event Action OnHideDialog;

    public PlayerMovement playerMovement;   // Reference to the PlayerMovement script
    private NPCController currentNPC;

    // Exposes DialogManager to the world, any class will be able to use this, super public, be careful here bec it may create crazy dependencies...
    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    // bool isTyping;  // Try, removed to allow spamming...
    int reference = 1;  // Prevents accidental skipping?
    // [SerializeField] GameObject audioDisabler;
    // [SerializeField] private string sceneName;

    // void Start()
    // {
    //     if (playerMovement != null)
    //     {
    //         // bool isButtonPressed = playerMovement.isButtonPressed;
    //     } else if (playerMovement == null)
    //     {
    //         Debug.LogError("PlayerMovement reference is not set!");
    //     }

    //     // Setup button click listener
    //     if (nextButton != null)
    //     {
    //         nextButton.onClick.AddListener(HandleUpdate);
    //     }
    //     else
    //     {
    //         Debug.LogError("NextButton reference is not set!");
    //     }
    // }

    public IEnumerator ShowDialog(Dialog dialog, NPCController npcController)
    {
        if (!playerMovement.isTalking)
        {
            yield return new WaitForEndOfFrame();
            OnShowDialog?.Invoke();

            this.currentNPC = npcController;
            this.dialog = dialog;
            dialogBox.SetActive(true);
            StartCoroutine(TypeDialog(dialog.Lines[0]));
            // Debug.Log("1st line");
            playerMovement.isTalking = true;  // reference=1, ButtonPress=0+1;
        }
    }

    public void HandleUpdate()  // 
    {
        // Event & Action?...
        if ((playerMovement.ButtonPress == reference))  // && !isTyping, reference=1, ButtonPress=0+1 (OnInteractDown...);
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                // Debug.Log("Next line");
                ++reference;
                Debug.Log("Reference: " + reference + ", Current Line: " + currentLine + ", Button Press: " + playerMovement.ButtonPress);
                // audioDisabler.SetActive(false);
            }
            else
            {
                currentLine = 0;
                playerMovement.ButtonPress = 0;
                playerMovement.isTalking = false;
                reference = 1;
                dialogBox.SetActive(false);
                OnHideDialog?.Invoke();
                // audioDisabler.SetActive(true);

                // Transition to next scene if NPC is a boss or special character
                if (currentNPC != null && currentNPC.isBoss)
                {
                    SceneManager.LoadScene("PopKyuTransition");
                }
            }
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        // isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        // isTyping = false;
    }
}