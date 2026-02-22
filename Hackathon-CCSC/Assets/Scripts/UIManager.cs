using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject hintsPanel;
    public GameObject pauseMenuUI;

    public void HintsOnClick()
    {
        // Freeze the game
        Time.timeScale = 0f;

        // Unlock and show mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenuUI.SetActive(false);
        // Show hints panel
        hintsPanel.SetActive(true);
    }

    public void CloseHints()
    {
        // Resume game
        Time.timeScale = 1f;

        // Hide panel
        hintsPanel.SetActive(false);

        // Optional: lock cursor again (if FPS game)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}