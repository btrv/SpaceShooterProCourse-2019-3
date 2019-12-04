using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]    private Text _scoreText;
    [SerializeField]    private Image _livesImg;
    [SerializeField]    private Sprite[] _LiveSprites;
    [SerializeField]    private GameObject _gameOver;
    [SerializeField]    private Text _restartText;



    void Start()
    {
        //assign text component to handle
        _scoreText.text = "Счёт: " + 0;
        _gameOver.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    // void Update()
    // {
    //     if(_LiveSprites[0] && Input.GetKeyDown(KeyCode.R))
    //     SceneManager.LoadScene("Game");
    // }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Счёт: " + playerScore;
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
}
