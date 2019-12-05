using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 2.5f);
    }
}