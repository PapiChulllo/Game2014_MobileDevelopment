using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    // Make this variable public or [SerializeField] so it appears in the Inspector
    [SerializeField]
    public bool isPlayerBullet; // Determines if this bullet is from the player

    private void Update()
    {
        // Move the bullet upwards if it's a player bullet, downwards if it's an enemy bullet
        transform.Translate((isPlayerBullet ? Vector3.up : Vector3.down) * speed * Time.deltaTime);

        // Destroy or deactivate the bullet if it moves off-screen
        if (transform.position.y > 10 || transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBehaviour>().StartCoroutine(collision.GetComponent<EnemyBehaviour>().DyingRoutine());
            gameObject.SetActive(false); // Deactivate bullet after hitting enemy
        }
        else if (!isPlayerBullet && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().TakeDamage();
            gameObject.SetActive(false); // Deactivate bullet after hitting player
        }
    }
}
