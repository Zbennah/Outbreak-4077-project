using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 15f; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
        Vector3 spawnPos = bulletSpawnPoint.position + bulletSpawnPoint.forward * 0.5f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, bulletSpawnPoint.rotation);

        
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider playerCollider = GetComponent<Collider>();
        if (bulletCollider && playerCollider)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }

        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null && bulletScript.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}
