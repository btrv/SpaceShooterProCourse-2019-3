using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]    private float _speed = 4f;
    [SerializeField]    private float _speedBoosted = 8;

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
    // [SerializeField]    private AudioClip _explosionSound;
    private AudioSource _audioSourse;


    void Start()
    {
        _shieldVis.SetActive(false);
        transform.position = new Vector3(0, -2.5f, 0);

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
    }
    
    void Update()
    {
        CalculateMovement();

        if (Input.GetButtonDown("Fire1") && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalinput = Input.GetAxis("Vertical");

        //x y controls
        //new Vector3(-1, 0, 0) * 1 * 3.5f real time
        //transform.Translate(Vector3.right * horizontalinput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalinput * _speed * Time.deltaTime);

        if(_isSetSpeedUpActive == false)
        transform.Translate(new Vector3(horizontalinput, verticalinput, 0) * _speed * Time.deltaTime);
        else
        transform.Translate(new Vector3(horizontalinput, verticalinput, 0) * _speedBoosted * Time.deltaTime);


        // Player bounds - Ограничения передвижения верх-низ

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }


        // Player bounds - Ограничения передвижения право-лево

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
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

        //play audio
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
            // _audioSourse.clip = _explosionSound;
            // _audioSourse.Play();
            
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
            _gameManager.GameIsOver();
        }
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

    //method to add 10 to the score
    public void AddTenToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
