using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]    private Text _scoreText;
    [SerializeField]    private Text _recordScoreText;
    [SerializeField]    private Text _restartText;
    [SerializeField]    private Image _livesImg;
    [SerializeField]    private Sprite[] _LiveSprites;
    [SerializeField]    private GameObject _gameOver;
    [SerializeField]    private GameObject _pauseMenuPanel;
    int m_Score;

    void Start()
    {
        SetHiScore();
        //assign text component to handle
        _scoreText.text = "Счёт: " + 0;
        _gameOver.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    void SetHiScore()
    {
        m_Score = PlayerPrefs.GetInt("hiScore", 0);
        UpdateRecordText(m_Score);
    }
    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Счёт: " + playerScore;
    }

    public void UpdateRecordText(int pts)
    {
       _recordScoreText.text = "Рекорд: " + pts;
    }

    public void UpdateLives(int _currentlives)
    {
        //display img sprite
        //give it a new one based on the currentLives index
        _livesImg.sprite = _LiveSprites[_currentlives];

        if(_currentlives == 0)
        GameOverSequence();
    }
    
    public void GameOverSequence()
    {
        StartCoroutine (BlinkingText());
        _restartText.gameObject.SetActive(true);
    }
    
    IEnumerator BlinkingText()
    {
        while(true)
        {
            _gameOver.SetActive(true);
            yield return new WaitForSeconds (0.5f);
            _gameOver.SetActive(false);
            yield return new WaitForSeconds (0.5f);
        }
    }
    
    public void ResumePlay() //Call from GameManager
    {
        GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        gm.ResumePlay();
    }

    public void StartMainMenu()
    {
        GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        gm.StartMainMenu();
    }
}
