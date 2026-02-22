using UnityEngine;
using TMPro;

public class ComputerController : MonoBehaviour
{
    public TMP_InputField apiInputField;
    public GameObject successText;
    public GameObject failureText;

    private string correctKey = "us-A7k9L2QxP";

public void RunCode(string input)
{
    string cleanedInput = input.Trim();

    Debug.Log("---- DEBUG START ----");
    Debug.Log("Entered: [" + cleanedInput + "]");
    Debug.Log("Correct: [" + correctKey + "]");
    Debug.Log("Entered Length: " + cleanedInput.Length);
    Debug.Log("Correct Length: " + correctKey.Length);
    Debug.Log("Equal? " + (cleanedInput == correctKey));
    Debug.Log("---- DEBUG END ----");

    if (cleanedInput == correctKey)
    {
        successText.SetActive(true);
        failureText.SetActive(false);
    }
    else
    {
        failureText.SetActive(true);
        successText.SetActive(false);
    }
}

void Update()
{
    if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
    {
        CloseComputer();
    }

    if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
    {
        RunCode(apiInputField.text);
    }
}
public void ClearMessages()
{
    successText.SetActive(false);
    failureText.SetActive(false);
}
public MonoBehaviour playerController;

public void CloseComputer()
{
    gameObject.SetActive(false);

    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}
}