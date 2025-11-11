using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.Instance.AllStop();
        Screen.SetResolution(2560, 1440, true);
        SoundManager.Instance.Play(SoundType.BGM, "Game", true);
    }
}
