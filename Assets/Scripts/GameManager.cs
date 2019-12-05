using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]    private bool _isGameOver;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && _isGameOver == true)
        {
            Debug.Log("Fire1 is pressed");
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        Application.Quit(); 
    }


    public void GameIsOver()
    {
        _isGameOver = true;
        Debug.Log("Game over");
    }
}
