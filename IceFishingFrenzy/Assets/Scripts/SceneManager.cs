using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
