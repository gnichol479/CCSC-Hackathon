using UnityEngine;

public class TaskBoardInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;

    public GameObject boardUIPanel;
    public MonoBehaviour playerController;

    private bool boardOpen = false;

    void Update()
    {
        if (!boardOpen)
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCamera.transform.position,
                                playerCamera.transform.forward,
                                out hit,
                                interactDistance))
            {
                if (hit.collider.CompareTag("TaskBoard"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OpenBoard();
                    }
                }
            }
        }

        if (boardOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBoard();
        }
    }

void OpenBoard()
{
    boardOpen = true;

    playerController.enabled = false;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    boardUIPanel.SetActive(true);
}

void CloseBoard()
{
    boardOpen = false;

    playerController.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    boardUIPanel.SetActive(false);
}
}