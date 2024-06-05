using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
 public GameObject projectilePrefab; // Prefab proyektil
    public Transform firePoint; // Titik asal proyektil
    public float projectileSpeed = 20f; // Kecepatan proyektil
    public float projectileLifetime = 2f; // Waktu hidup proyektil sebelum dihapus
    
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Klik kanan mouse
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate proyektil di posisi firePoint dengan rotasi firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
        // Mendapatkan Rigidbody dari proyektil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Mengatur kecepatan proyektil ke arah yang dihadapi oleh pemain
            rb.velocity = firePoint.forward * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("No Rigidbody found on the projectile prefab.");
        }

        // Hancurkan proyektil setelah waktu tertentu
        Destroy(projectile, projectileLifetime);
    }
}