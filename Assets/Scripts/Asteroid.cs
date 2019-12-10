using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _rotateSpeed = 10f;
    [SerializeField]    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) //Damage system
    {
        if(other.tag == "Player") //Damage player + null checking
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            player.Damage();
            
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _spawnManager.StartSpawning();

            Destroy (this.gameObject, 0.1f);
        }

        if(other.tag == "Laser") //Damage by Laser
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            _spawnManager.StartSpawning();
            
            Destroy(this.gameObject, 0.1f);
        }
    }
}
