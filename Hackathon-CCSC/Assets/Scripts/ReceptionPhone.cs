using UnityEngine;

public class ReceptionPhone : MonoBehaviour
{
    public CutsceneDialogueManager dialogueManager;

    public void TriggerPhoneCall()
    {
        if (dialogueManager != null && !dialogueManager.IsDialogueActive())
        {
            dialogueManager.StartCustomerServicePhoneCall();
        }
    }
}