using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab proyektil
    public Transform firePoint; // Titik asal proyektil
    public float projectileSpeed = 20f; // Kecepatan proyektil

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
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed; // Menembakkan proyektil ke arah depan firePoint
        }
    }
}
