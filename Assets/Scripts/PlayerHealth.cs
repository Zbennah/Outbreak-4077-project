using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Health Bar Settings (OnGUI)")]
    public int barWidth = 150;
    public int barHeight = 20;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // --- Health Functions ---
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

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
        // Your death logic goes here
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // --- OnGUI Health Bar ---
    void OnGUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;

        // Position in TOP RIGHT
        int x = Screen.width - barWidth - 10;
        int y = 10;

        // Background bar
        GUI.color = Color.black;
        GUI.Box(new Rect(x, y, barWidth, barHeight), GUIContent.none);

        // Green fill bar
        GUI.color = Color.green;
        GUI.Box(new Rect(x, y, barWidth * healthPercent, barHeight), GUIContent.none);

        GUI.color = Color.white; // reset
    }
}
