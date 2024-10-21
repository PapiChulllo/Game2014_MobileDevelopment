using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private BulletManager bulletManager; // Reference to BulletManager for shooting
    [SerializeField]
    private float enemySpeed = 2f; // Speed at which the enemy moves

    // Shooting parameters
    private float shootCooldown = 2f; // Time between enemy shots
    private float nextShotTime = 0f; // Time of the next shot

    void Update()
    {
        // Move the enemy downward
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        // Shooting logic
        if (Time.time > nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shootCooldown;
        }

        // Destroy enemy if it goes off-screen
        if (transform.position.y < -10f) // Adjust as needed for your game world
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        // Get a bullet from BulletManager and set it at the enemy's position
        GameObject bullet = bulletManager.GetBullet(false); // False indicates it's an enemy bullet
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

    public IEnumerator DyingRoutine()
    {
        // Optional: Hide the enemy or play death animation
        yield return new WaitForSeconds(0.2f); // Wait before destroying
        Destroy(gameObject); // Destroy the enemy
    }
}
