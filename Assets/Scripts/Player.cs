using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    private float _speed = 6f;
    [SerializeField]    private GameObject _laserPrefab;
    [SerializeField]    private GameObject _tripleShotPrefab;
    [SerializeField]    private float _fireRate = 0.1f;
                        private float _canFire = -0.5f;
    [SerializeField]    private int _lives = 3;
                        private SpawnManager _spawnManager;
                        private bool _isTripleShotActive = false;
                        private bool _isSetSpeedUpActive = false;
                        private bool _isShieldsIsActive = false;
    [SerializeField]    private GameObject _shieldVis;
    [SerializeField]    private int _score;
                        private UIManager _uiManager;
                        private GameManager _gameManager;
    [SerializeField]    private GameObject _leftEnginePrefab;
    [SerializeField]    private GameObject _rightEnginePrefab;
    [SerializeField]    private AudioClip _laserShotAudio;
                        private AudioSource _audioSourse;
                        public bool _isPlayerOne = false;
                        public bool _isPlayerTwo = false;


    void Start()
    {
        _shieldVis.SetActive(false);
        if (_shieldVis == null)
        Debug.LogError("SieldVis is NULL");

        _audioSourse = GetComponent<AudioSource>();
        if(_audioSourse == null)
        Debug.LogError("Audio Sourse on the Player is NULL");
        else _audioSourse.clip = _laserShotAudio;
        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        Debug.LogError("Spawn Manager is NULL");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        Debug.LogError("UI manager is NULL");

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        Debug.LogError("GameManager is NULL");
        
        if(_gameManager.isSingleMode == true)
        transform.position = new Vector3(0, -2.5f, 0);
    }
    
    void Update()
    {
        PlayerBounds();
        SpeedBoosted();

        if(_isPlayerOne == true)
        {
            PlayerOneMovement();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            FireLaser();
        }

        if(_isPlayerTwo == true)
        {
            PlayerTwoMovement();
            if (Input.GetKeyDown(KeyCode.RightShift) && Time.time > _canFire)
            FireLaser();
        }
    }

    private void PlayerOneMovement()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");
    
        transform.Translate(new Vector3(horizontalinput, verticalinput, 0) * _speed * Time.deltaTime);
    }

    private void PlayerTwoMovement()
    {
        if(Input.GetKey(KeyCode.Keypad4))
        transform.Translate(Vector3.left * _speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Keypad6))
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Keypad8))
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Keypad5))
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    void PlayerBounds()
    {
        // Ограничения передвижения верх-низ
        if (transform.position.y >= 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }

        // Ограничения передвижения право-лево
        if (transform.position.x >= 10.85f)
        {
            transform.position = new Vector3(-10.85f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.85f)
        {
            transform.position = new Vector3(10.85f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        }

        _audioSourse.Play();
        
    }

    public void Damage()
    {
        //if shields is active - do nothing
        //deactivate shields
        //return;
        if(_isShieldsIsActive == true)
        {
            _isShieldsIsActive = false;
            _shieldVis.SetActive(false);
            return;
        }

        _lives --;
        _uiManager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _leftEnginePrefab.SetActive(true);
        }

        else if(_lives == 1)
        {
            _rightEnginePrefab.SetActive(true);
        }

        if(_lives < 1) 
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
            _gameManager.GameIsOver();
        }
    }

    private void SpeedBoosted()
    {
        if(_isSetSpeedUpActive == true)
        _speed = 10f;
        else
        _speed = 6f;
    }
    
    public void SetTripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        while(_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
    }

    public void SetSpeedUpActive()
    {
        _isSetSpeedUpActive = true;
        StartCoroutine(SpeedUpPowerDownRoutine());
    }
    
    IEnumerator SpeedUpPowerDownRoutine()
    {
        while(_isSetSpeedUpActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isSetSpeedUpActive = false;
        }
    }

    public void SetShieldsActive()
    {
        _isShieldsIsActive = true;
        _shieldVis.SetActive (true);
    }
   
    public void AddTenToScore(int points) //method to add 10 to the score
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}