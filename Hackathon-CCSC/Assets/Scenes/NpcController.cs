using Unisave.Facets;
using UnityEngine;
using TMPro;
using Unisave;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    // references to UI components,
    // must be set up manually in the inspector window
    public TMP_InputField promptField;
    public Button respondButton;
    public TextMeshProUGUI conversationText;

    // conversation state necessary to generate coherent responses
    private NpcConversation conversation = new NpcConversation();

    void Start()
    {
        respondButton.onClick.AddListener(OnPlayerResponse);
        promptField.onSubmit.AddListener(_ => OnPlayerResponse());
    }
async void OnPlayerResponse()
{
    try 
    {
        // Define and await the call inside the try block
        var facetCall = this.CallFacet(
            (NpcFacet f) => f.RespondToPlayer(conversation, promptField.text)
        );
        
        conversation = await facetCall;
        
        // Display result
        conversationText.text += "\n\n NeoForge Assistant: " + conversation.lastModelResponseText;
    }
    catch (System.Exception e) 
    {
        // This will print the ACTUAL error message to your console
        Debug.LogError("The server-side call failed: " + e.Message);
        conversationText.text += "\n\n[SYSTEM]: Connection failed. Check Console.";
    }
    finally 
    {
        // Reset UI
        promptField.interactable = true;
        respondButton.interactable = true;
    }
}
}
