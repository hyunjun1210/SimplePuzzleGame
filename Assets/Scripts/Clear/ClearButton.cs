using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearButton : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
