using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour,IDamageableEnemy
{
    public Image healthBar;
    public float maxHealth = 100f;
    private float currentHealth;
    private Animator animator;
    AudioManager audioManager;
    GameOverScreen gameOverScreen;
    private WinConditionManager winConditionManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameOverScreen = FindObjectOfType<GameOverScreen>();
        winConditionManager = FindObjectOfType<WinConditionManager>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth/100f;
        animator.SetTrigger("hurt");
        Debug.Log(gameObject.name + " took " + damage + " damage!");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        // Add death animation or effect here
        GetComponent<Movement>().enabled = false;
        animator.SetBool("died", true);
        audioManager.playSfx(audioManager.death);
        
        //StartCoroutine(GoingRespawn());
        // Tambahkan logika kemenangan
        if (gameOverScreen != null)
        {
            gameOverScreen.TriggerGameOver();
        }
        if (winConditionManager != null)
        {
            winConditionManager.EnemyDefeated();
        }
    }
    
    // IEnumerator GoingRespawn(){
    //     yield return new WaitForSeconds(3);
    //     Application.LoadLevel(Application.loadedLevel);
        
    // }
}
