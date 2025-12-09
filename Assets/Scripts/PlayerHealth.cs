using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Health Bar Settings (OnGUI)")]
    public int barWidth = 150;
    public int barHeight = 20;

    [Header("Respawn Settings")]
    public float respawnDelay = 2f;

    [Header("Audio Settings")]
    public AudioClip damageSound;  
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false; 
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Die()
    {
        Debug.Log("Player Died");

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void OnGUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;

        int x = Screen.width - barWidth - 10;
        int y = 10;

        GUI.color = Color.black;
        GUI.Box(new Rect(x, y, barWidth, barHeight), GUIContent.none);

        GUI.color = Color.green;
        GUI.Box(new Rect(x, y, barWidth * healthPercent, barHeight), GUIContent.none);

        GUI.color = Color.white;
    }
}
