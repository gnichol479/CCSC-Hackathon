using UnityEngine;

public class TaskBoardUIController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject taskPreviewPanel;

    [Header("Preview Images")]
    public GameObject smoothiePreviewImage;

    [Header("References")]
    public GameObject taskBoardUIPanel;
    public MonoBehaviour playerController;
    public CutsceneDialogueManager cutsceneManager;
public GameObject apiPreviewImage;
    // ===============================
    // OPEN PREVIEW
    // ===============================
    public void OpenSmoothiePreview()
    {
        taskPreviewPanel.SetActive(true);
        smoothiePreviewImage.SetActive(true);
    }
    public void OpenAPIPreview()
{
    taskPreviewPanel.SetActive(true);
    apiPreviewImage.SetActive(true);
}

    // ===============================
    // ACCEPT TASK
    // ===============================
    public void AcceptSmoothieTask()
    {
        smoothiePreviewImage.SetActive(false);
        taskPreviewPanel.SetActive(false);

        // Close entire board
        taskBoardUIPanel.SetActive(false);

        playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cutsceneManager.StartSmoothieTask();
    }
public void AcceptAPITask()
{
    apiPreviewImage.SetActive(false);
    taskPreviewPanel.SetActive(false);
    taskBoardUIPanel.SetActive(false);

    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    cutsceneManager.StartAPITask();
}




    // ===============================
    // CANCEL TASK
    // ===============================
public void CancelPreview()
{
    smoothiePreviewImage.SetActive(false);
    apiPreviewImage.SetActive(false);

    taskPreviewPanel.SetActive(false);
}
}