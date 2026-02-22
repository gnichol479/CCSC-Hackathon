using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public AudioMixer masterMixer;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void SetVolume(float sliderValue)
    {
        // Convert 0�1 slider to decibel range
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}