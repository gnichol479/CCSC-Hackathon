using Unisave.Facets;
using UnityEngine;
using TMPro;
using Unisave;

public class NpcController : MonoBehaviour
{
    public TMP_InputField promptField;
    public TextMeshProUGUI conversationText;

    private NpcConversation conversation = new NpcConversation();

    void Start()
    {
        promptField.onEndEdit.AddListener(OnEndEdit);
    }

    void OnEndEdit(string text)
    {
        if (!Input.GetKeyDown(KeyCode.Return))
            return;

        OnPlayerResponse();
    }

    async void OnPlayerResponse()
    {
        if (string.IsNullOrWhiteSpace(promptField.text))
            return;

        // Disable input while waiting
        promptField.interactable = false;

        string playerMessage = promptField.text;

        // Show player message immediately
        conversationText.text += "\n\nYou: " + playerMessage;

        promptField.text = "";

        try
        {
            var facetCall = this.CallFacet(
                (NpcFacet f) => f.RespondToPlayer(conversation, playerMessage)
            );

            conversation = await facetCall;

            conversationText.text += "\n\nNeoForge Assistant: " + conversation.lastModelResponseText;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Server call failed: " + e.Message);
            conversationText.text += "\n\n[SYSTEM]: Connection failed.";
        }
        finally
        {
            promptField.interactable = true;
            promptField.ActivateInputField(); // refocus
        }
    }
}