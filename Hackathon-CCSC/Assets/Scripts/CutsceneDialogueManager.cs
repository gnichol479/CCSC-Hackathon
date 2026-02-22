using UnityEngine;
using TMPro;
using System.Collections;

public class CutsceneDialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.02f;

    [Header("Boss")]
    public GameObject bossObject;
    public Animator bossAnimator;

    [Header("Player References")]
    public Transform playerObject;            
    public Transform bossCameraPoint;         
    public Transform playerSpawnPoint;        
    public MonoBehaviour playerController;    

    private string[] lines;
    private int index;
    private bool isTyping;
    private bool dialogueActive = false;

    private CharacterController characterController;

    void Start()
    {
        characterController = playerObject.GetComponent<CharacterController>();

        if (bossObject != null)
            bossObject.SetActive(false);

        dialoguePanel.SetActive(false);
    }

    // =============================
    // TASK METHODS
    // =============================

    public void StartSmoothieTask()
    {
        StartDialogue(new string[]
        {
            "Intern.",
            "I have successfully automated smoothie production.",
            "It is revolutionary.",
            "The AI turned on the blender exactly as instructed.",
            "Unfortunately...",
            "It cannot physically insert the ingredients.",
            "So it blended nothing.",
            "Which technically means it worked.",
            "Please assist the blender in achieving its full potential."
        });
    }

    // =============================
    // CORE DIALOGUE SYSTEM
    // =============================

    public void StartDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;
        dialogueActive = true;

        bossObject.SetActive(true);
        playerController.enabled = false;

        TeleportPlayer(bossCameraPoint);

        dialoguePanel.SetActive(true);

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        if (bossAnimator != null)
        {
            bossAnimator.Play("Talking");
        }

        foreach (char letter in lines[index])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void NextLine()
    {
        index++;

        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;

        dialoguePanel.SetActive(false);
        bossObject.SetActive(false);

        TeleportPlayer(playerSpawnPoint);

        playerController.enabled = true;
    }

    void TeleportPlayer(Transform targetPoint)
    {
        if (characterController != null)
            characterController.enabled = false;

        playerObject.position = targetPoint.position;
        playerObject.rotation = targetPoint.rotation;

        if (characterController != null)
            characterController.enabled = true;
    }
}