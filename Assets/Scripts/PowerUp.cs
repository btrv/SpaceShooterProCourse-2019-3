using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]    private float _powerUpSpeed = 3;

    [SerializeField]    private int powerupID; //ID for powerups: 0 = triple shot, 1 = speed, 2 = shields

    [SerializeField]    private AudioClip _clip;
   
    void Update()
    {
        transform.Translate(Vector2.down * _powerUpSpeed * Time.deltaTime);
        if (transform.position.y <= -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //other - это переменная 
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>(); //player - переменная куда складываем искомый объект Player
            if (player != null)
            // {
            //     if(powerupID == 0)
            //     player.SetTripleShotActive();

            //     else if(powerupID == 1)
            //     Debug.Log("Speed boost collected");
            //     //player.SetSpeedUpActive();

            //     else if(powerupID == 2)
            //     Debug.Log("Shields collected");
            //     //player.SetShieldsOnActive();
            // }
            switch(powerupID)
            {
                case 0: player.SetTripleShotActive(); break;
                case 1: player.SetSpeedUpActive(); break;
                case 2: player.SetShieldsActive(); break;
                default: Debug.Log("Default Value"); break;
            }
            
            //instantiate для аудио, удаляется после использования
            AudioSource.PlayClipAtPoint(_clip, transform.position); 

            Destroy(this.gameObject);
        }
    }
}
