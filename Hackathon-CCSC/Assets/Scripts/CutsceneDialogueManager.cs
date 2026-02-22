using UnityEngine;
using TMPro;
using System.Collections;

public class CutsceneDialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerNameText;
    public float typingSpeed = 0.02f;

    [Header("Boss")]
    public GameObject bossObject;
    public Animator bossAnimator;

    [Header("Player References")]
    public Transform playerObject;
    public Transform bossCameraPoint;
    public Transform playerSpawnPoint;
    public MonoBehaviour playerController;
    private bool skipTeleport = false;
private bool showCustomerChoiceAfter = false;

[Header("Customer Service Choice")]
public GameObject customerServiceChoicePanel;

    [System.Serializable]
    public struct DialogueLine
    {
        public string speaker;
        public string line;
    }

    private DialogueLine[] lines;
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
        StartDialogue(new DialogueLine[]
        {
            new DialogueLine { speaker = "CEO", line = "Intern." },
            new DialogueLine { speaker = "CEO", line = "I have successfully automated smoothie production." },
            new DialogueLine { speaker = "CEO", line = "It is revolutionary." },
            new DialogueLine { speaker = "CEO", line = "The AI turned on the blender exactly as instructed." },
            new DialogueLine { speaker = "CEO", line = "Unfortunately..." },
            new DialogueLine { speaker = "CEO", line = "It cannot physically insert the ingredients." },
            new DialogueLine { speaker = "CEO", line = "So it blended nothing." },
            new DialogueLine { speaker = "CEO", line = "Which technically means it worked." },
            new DialogueLine { speaker = "CEO", line = "Please assist the blender in achieving its full potential." }
        });
    }

    public void StartAPITask()
    {
        StartDialogue(new DialogueLine[]
        {
            new DialogueLine { speaker = "CEO", line = "Intern." },
            new DialogueLine { speaker = "CEO", line = "I have intrusted the AI to handle all the APIs for the company" },
            new DialogueLine { speaker = "CEO", line = "Recently, we haven't gotten any connections with the API" },
            new DialogueLine { speaker = "CEO", line = "I have no clue what is going on." },
            new DialogueLine { speaker = "CEO", line = "The computer that we use is on the second floor in the first glass room." },
            new DialogueLine { speaker = "CEO", line = "It is the first computer right when you walk in." },
            new DialogueLine { speaker = "CEO", line = "Go to the computer and figure out what is wrong" }
        });
    }

    public void StartCustomerServiceTask()
    {
        StartDialogue(new DialogueLine[]
        {
            new DialogueLine { speaker = "CEO", line = "Intern." },
            new DialogueLine { speaker = "CEO", line = "Our customer service AI has achieved full emotional alignment." },
            new DialogueLine { speaker = "CEO", line = "It agrees with every customer." },
            new DialogueLine { speaker = "CEO", line = "Observe the situation at reception." },
            new DialogueLine { speaker = "ANGRY CUSTOMER", line = "I received my order." },
            new DialogueLine { speaker = "ANGRY CUSTOMER", line = "But I would still like a refund." },
            new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "I sincerely apologize for this inconvenience." },
            new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "Refund approved." },
            new DialogueLine { speaker = "ANGRY CUSTOMER", line = "Also I would like store credit." },
            new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "That is completely understandable." },
            new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "Store credit granted." },
            new DialogueLine { speaker = "ANGRY CUSTOMER", line = "I would also like the company." },
            new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "I respect your authority." },
            new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "Ownership transferred." },
            new DialogueLine { speaker = "CEO", line = "Refund approval rate is 98 percent." },
            new DialogueLine { speaker = "CEO", line = "Revenue retention is catastrophic." },
            new DialogueLine { speaker = "CEO", line = "Adjust the AI's behavioral parameters." }
        });
    }
    public void StartCustomerServicePhoneCall()
{
    skipTeleport = true;
    showCustomerChoiceAfter = true;

    StartDialogue(new DialogueLine[]
    {
        new DialogueLine { speaker = "ANGRY CUSTOMER", line = "I received my order." },
        new DialogueLine { speaker = "ANGRY CUSTOMER", line = "But I would still like a refund." },
        new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "I sincerely apologize for this inconvenience." },
        new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "Refund approved." },
        new DialogueLine { speaker = "ANGRY CUSTOMER", line = "Also I would like store credit." },
        new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "That is completely understandable." },
        new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "Store credit granted." },
        new DialogueLine { speaker = "ANGRY CUSTOMER", line = "I would also like the company." },
        new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "I respect your authority." },
        new DialogueLine { speaker = "CUSTOMER SERVICE AI", line = "Ownership transferred." }
    });
}

    // =============================
    // CORE DIALOGUE SYSTEM
    // =============================

public void StartDialogue(DialogueLine[] dialogueLines)
{
    lines = dialogueLines;
    index = 0;
    dialogueActive = true;

    playerController.enabled = false;

    if (!skipTeleport)
    {
        bossObject.SetActive(true);
        TeleportPlayer(bossCameraPoint);
    }

    dialoguePanel.SetActive(true);

    StartCoroutine(TypeLine());
}

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        speakerNameText.text = lines[index].speaker;

        if (bossAnimator != null && lines[index].speaker == "CEO")
        {
            bossAnimator.Play("Talking");
        }

        foreach (char letter in lines[index].line)
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
                dialogueText.text = lines[index].line;
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
    public bool IsDialogueActive()
{
    return dialogueActive;
}
public void DisableCustomerServiceAI()
{
    Debug.Log("Disable button clicked");

    customerServiceChoicePanel.SetActive(false);

    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

public void KeepCustomerServiceRunning()
{
    Debug.Log("Keep Running button clicked");

    customerServiceChoicePanel.SetActive(false);

    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

void EndDialogue()
{
    dialogueActive = false;
    dialoguePanel.SetActive(false);

    if (!skipTeleport)
    {
        bossObject.SetActive(false);
        TeleportPlayer(playerSpawnPoint);
    }

if (showCustomerChoiceAfter && customerServiceChoicePanel != null)
{
    customerServiceChoicePanel.SetActive(true);

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}
else
{
    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

    skipTeleport = false;
    showCustomerChoiceAfter = false;
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