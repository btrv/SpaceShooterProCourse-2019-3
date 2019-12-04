using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField]    private float _enemySpeed = 4.0f;
    private Player _player;
    private Animator _anim;
    private AudioSource _audioSourse;
    
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
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        if(transform.position.y <= -6f)
        {
            float randomX = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }


    
    private void OnTriggerEnter2D(Collider2D other) //Damage system
    {
        if(other.gameObject.tag == "Player") //damage player + null checking
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            player.Damage();
            
            _enemySpeed = -1f;
            _anim.SetTrigger("OnEnemyDeath");
            _audioSourse.Play();

            Destroy (this.gameObject, 1.5f);
        }

        if(other.gameObject.tag == "Laser") //Damage by Laser
        {
            Destroy(other.gameObject);

            //add 10 to the score
            if(_player != null)
            _player.AddTenToScore(10);
            
            // this.GetComponent<Rigidbody2D>().Equals(null);
            _enemySpeed = -3f;
            _anim.SetTrigger("OnEnemyDeath");
            _audioSourse.Play();

            Destroy(this.gameObject, 1.5f);
        }
    }

}
