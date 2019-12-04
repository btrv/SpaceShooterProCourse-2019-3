using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _LaserSpeed = 8.0f;
    
    void Update()
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
}
