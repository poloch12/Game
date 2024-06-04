using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject infoPanel;

    void Start()
    {
        // Ensure the info panel is hidden at the start
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    // Function to load the Forest scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Forest");
    }

    // Function to toggle the Info panel
    public void ShowInfo()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }

    // Function to hide the Info panel
    public void HideInfo()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    // Function to exit the game
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        // This line is just to ensure the exit works in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
