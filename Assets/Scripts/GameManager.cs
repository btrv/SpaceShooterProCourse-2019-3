using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]    private bool _isGameOver;
                        public bool isSingleMode = true;
    [SerializeField]    private GameObject _pauseMenuPanel;
                        private Animator _pauseAnimator;

    private void Start()
    {
        _pauseMenuPanel.SetActive(true);
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            Debug.Log("R is pressed");
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape is pressed");
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            _pauseMenuPanel.SetActive(true);
            _pauseAnimator.SetBool("isPaused", true);
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
