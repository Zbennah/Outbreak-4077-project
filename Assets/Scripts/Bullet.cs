using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth target = other.GetComponent<EnemyHealth>(); 
        if (target)
        {
            target.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
