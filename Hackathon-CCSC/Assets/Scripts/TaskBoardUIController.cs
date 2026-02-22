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

    // ===============================
    // OPEN PREVIEW
    // ===============================
    public void OpenSmoothiePreview()
    {
        taskPreviewPanel.SetActive(true);
        smoothiePreviewImage.SetActive(true);
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

    // ===============================
    // CANCEL TASK
    // ===============================
    public void CancelPreview()
    {
        smoothiePreviewImage.SetActive(false);
        taskPreviewPanel.SetActive(false);
    }
}