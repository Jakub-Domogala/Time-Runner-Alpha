using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] float timeToShot;
    [SerializeField] GameObject projectile;
    public Transform firePoint;
    private float nextFireTime = 2;
    private void Update()
    {
        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            // Fire bullet
            Fire();
            // Set next fire time
            nextFireTime = Time.time + timeToShot / GameMaster.Instance.timeMultiplayer;
           
        }
    }

    void Fire()
    {
        // Instantiate bullet at the fire point
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        // Play firing sound or animation
    }
}
