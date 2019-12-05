using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _LaserSpeed = 8f;
    private bool _isEnemyLaser = false;
    
    void Update()
    {
        if(_isEnemyLaser == false)
        PlayerLaser();
        else EnemyLaser();
    }

    void PlayerLaser()
    {
        //move laser up
        transform.Translate(Vector3.up * _LaserSpeed * Time.deltaTime);

        //destroy lasers
        if(transform.position.y >= 6)
        {
            if(this.transform.parent != null)
            {
                 Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void EnemyLaser()
    {
        //move laser
        transform.Translate(Vector3.down * _LaserSpeed * Time.deltaTime);

        //destroy lasers
        if(transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
}
