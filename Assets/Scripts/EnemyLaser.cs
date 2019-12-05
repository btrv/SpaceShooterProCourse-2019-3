using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]    private float _enemyLaserSpeed = 8f;
                        private AudioSource _audioSourse;
    [SerializeField]    private AudioClip _explosion;

    void Start()
    {
       _audioSourse = GetComponent<AudioSource>();
       if(_audioSourse == null)
       Debug.LogError("AudioSourse is NULL");
    }
    
    void Update()
    {
        //move laser
        transform.Translate(Vector3.down * _enemyLaserSpeed * Time.deltaTime);

        //destroy lasers
        if(transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.Damage();
            _audioSourse.Play();
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
        }
    }
}
