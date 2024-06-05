using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Attach this script to a GameObject in the scene, such as an empty GameObject or the Canvas

    public Button loadSceneButton;
    public Button exitButton;

    void Start()
    {
        // Ensure the buttons are properly assigned in the Inspector
        if (loadSceneButton != null)
        {
            loadSceneButton.onClick.AddListener(LoadScene);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Forest");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ExitGame()
    {
        // For quitting the application
        Application.Quit();

        // If you are running the game in the Unity editor, use the following line instead
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
