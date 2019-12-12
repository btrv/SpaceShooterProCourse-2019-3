using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]    private bool _isGameOver;
                        public bool isSingleMode = true;
    [SerializeField]    private GameObject _pauseMenuPanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            Debug.Log("R is pressed");
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        Application.Quit();

        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            _pauseMenuPanel.SetActive(true);
        }

    }

    public void ResumePlay()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene(0);
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void GameIsOver()
    {
        _isGameOver = true;
        Debug.Log("Game over");
    }
}
