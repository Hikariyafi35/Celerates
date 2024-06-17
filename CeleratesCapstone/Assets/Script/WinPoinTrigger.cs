using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoinTrigger : MonoBehaviour
{
    public WinConditionManager winConditionManager; // Referensi ke WinConditionManager

    private void OnTriggerEnter2D(Collider2D other) // Menggunakan OnTriggerEnter2D untuk Collider 2D
    {
        if (other.CompareTag("Player")) // Jika pemain mencapai titik kemenangan
        {
            if (winConditionManager != null)
            {
                if (winConditionManager.AllEnemiesDefeated()) // Periksa apakah semua musuh telah dikalahkan
                {
                    Debug.Log("Player reached win point and all enemies defeated.");
                    winConditionManager.TriggerWin(); // Panggil metode TriggerWin() dari WinConditionManager
                }
                else
                {
                    Debug.Log("Player reached win point but not all enemies are defeated.");
                }
            }
            else
            {
                Debug.LogError("WinConditionManager is not assigned in the WinTrigger");
            }
        }
    }
}