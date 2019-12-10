using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField]    private float _enemySpeed = 4.0f;
                        private Player _player;
                        private Animator _anim;
                        private AudioSource _audioSourse;
    [SerializeField]    private GameObject _LaserPrefab;
                        private float _canFire;
                        private float _fireRate;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        Debug.LogError("Player is NULL");

        _anim = GetComponent<Animator>(); //Аниматор уже прикреплён к объекту Enemy, Find не нужен.
        if(_anim == null)
        Debug.LogError("Animator is NULL");

        _audioSourse = GetComponent<AudioSource>();
        if(_audioSourse == null)
        Debug.LogError("AudioSourse is NULL");
    }

    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire)
        EnemyFireLaser();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        if(transform.position.y <= -6f)
        {
            float randomX = UnityEngine.Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }

    private void EnemyFireLaser()
    {
        _fireRate = Random.Range(3f, 5f);
        _canFire = Time.time + _fireRate;

        GameObject enemyLaser = Instantiate(_LaserPrefab, transform.position + new Vector3(0f, -1.2f, 0), Quaternion.identity);
        
        //!!!!!Для варианта с префабом из двух лазеров!!!!!
        
        // Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        // //Неизвестно сколько дочерних объектов "Laser" будет в массиве "lasers" - поэтому форлуп по массиву.
        // for(int i = 0; i < lasers.Length; i++)
        // {
        //     lasers[i].AssignEnemyLaser();
        // }
        enemyLaser.GetComponent<Laser>().AssignEnemyLaser();
    }

    private void OnTriggerEnter2D(Collider2D other) //Damage system
    {
        if(other.gameObject.tag == "Player") //damage player + null checking
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                Destroy(GetComponent<Collider2D>());
            }
            
            _enemySpeed = 2f;
            _anim.SetTrigger("OnEnemyDeath");
            _audioSourse.Play();

            Destroy (this.gameObject, 1.5f);
        }

        if(other.gameObject.tag == "Laser") //Damage by Laser
        {
            Destroy(GetComponent<Collider2D>());
            Destroy(other.gameObject);

            //add 10 to the score
            if(_player != null)
            _player.AddTenToScore(10);
            
            // this.GetComponent<Rigidbody2D>().Equals(null);
            _enemySpeed = 2f;
            _anim.SetTrigger("OnEnemyDeath");
            _audioSourse.Play();

            Destroy(this.gameObject, 1.5f);
        }
    }
}
