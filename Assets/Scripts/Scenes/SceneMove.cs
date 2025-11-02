using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [SerializeField] private SceneType sceneType;

    public void SceneMoveButton()
    {
        SceneManager.LoadScene((int)sceneType);
    }

}
