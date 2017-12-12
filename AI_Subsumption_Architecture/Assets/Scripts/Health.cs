using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public RectTransform healthBar;

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;

    private void Start()
    {
        
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        if (currentHealth <= 30)
        {
            Debug.Log("Low Health!");
        }
    }
}
