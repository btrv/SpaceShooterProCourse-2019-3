using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]    private bool _isGameOver;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            Debug.Log("R pressed");
            SceneManager.LoadScene(0);
        }
    }


    public void GameIsOver()
    {
        _isGameOver = true;
        Debug.Log("Game over");
    }
}
