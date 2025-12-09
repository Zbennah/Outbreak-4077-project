using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    [Header("Audio Settings")]
    public AudioClip deathSound;
    private AudioSource audioSource;

    private bool isDead = false; 

    void Start()
    {
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; 

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; 
        isDead = true;

        
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);

        Destroy(gameObject, deathSound != null ? deathSound.length : 0f);
    }
}
