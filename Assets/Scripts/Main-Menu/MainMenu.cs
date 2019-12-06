using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadCoopGame()
    {
        SceneManager.LoadScene(2);
    }
}
