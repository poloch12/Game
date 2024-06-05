using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapedSceneManager : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // This line is only needed to exit play mode in the Unity editor
#endif
    }
}
