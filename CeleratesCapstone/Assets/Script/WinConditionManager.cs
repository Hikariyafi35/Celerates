using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    public GameObject winPanel; // Referensi ke panel kemenangan
    private int totalEnemies;
    private int enemiesDefeated = 0;

    private void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false); // Nonaktifkan panel kemenangan di awal
        }
        else
        {
            Debug.LogError("WinPanel is not assigned in the WinConditionManager");
        }

        totalEnemies = FindObjectsOfType<HealthSystemEnemy>().Length; // Hitung total musuh di awal
        Debug.Log("Total Enemies: " + totalEnemies);
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        Debug.Log("Enemies Defeated: " + enemiesDefeated);
        if (enemiesDefeated >= totalEnemies)
        {
            Debug.Log("All enemies defeated.");
        }
    }

    public void TriggerWin()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);// Aktifkan panel kemenangan
            Time.timeScale = 0f; 
            Debug.Log("Win condition met! Displaying win panel.");
        }
        else
        {
            Debug.LogError("WinPanel is not assigned in the WinConditionManager");
        }
    }

    public bool AllEnemiesDefeated()
    {
        return enemiesDefeated >= totalEnemies;
    }
}