using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private BulletManager bulletManager; // BulletManager reference for shooting
    [SerializeField]
    private float enemySpeed = 2f; // Speed at which the enemy moves

    // Shooting parameters
    private float shootCooldown = 2f; // Time between enemy shots
    private float nextShotTime = 0f; // Time of the next shot

    void Update()
    {
        // Move the enemy downward
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);

        // Continuously shoot at intervals
        if (Time.time > nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shootCooldown;
        }

        // Destroy enemy if it goes off-screen
        if (transform.position.y < -10f) // Adjust the boundary as needed for your game world
        {
            Destroy(gameObject); // Destroys the enemy when it leaves the screen
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
        GetComponent<Renderer>().enabled = false; // Hide the enemy
        GetComponent<Collider2D>().enabled = false; // Disable collisions

        yield return new WaitForSeconds(0.2f); // Wait for a short duration before destruction

        Destroy(gameObject); // Destroy the object at the end of the routine
    }
}
