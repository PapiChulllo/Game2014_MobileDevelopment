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
        if (transform.position.y > 10 || transform.position.y < -10) // Adjust boundaries according to your game screen
        {
            Destroy(gameObject); // Alternatively, you can set the bullet inactive with gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.CompareTag("Enemy"))
        {
            // Destroy the enemy plane
            Destroy(collision.gameObject);
            // Deactivate the bullet after collision
            Destroy(gameObject);

            // Update the score (make sure you have a reference to the GameController)
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                gameController.ChangeScore(10); // Adjust score increment as needed
            }
        }
        else if (!isPlayerBullet && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().TakeDamage();
            Destroy(gameObject); // Deactivate the bullet after hitting the player
        }
    }

}
