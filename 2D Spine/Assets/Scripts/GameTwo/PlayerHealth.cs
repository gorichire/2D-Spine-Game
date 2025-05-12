using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth { get; private set; }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log($"체력 감소! 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("게임 오버!");
            // GameOver 처리 로직 추가
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }
}
