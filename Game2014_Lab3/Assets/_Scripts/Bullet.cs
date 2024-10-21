using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    public bool isPlayerBullet;

    private void Update()
    {
        transform.Translate((isPlayerBullet ? Vector3.up : Vector3.down) * speed * Time.deltaTime);

        if (transform.position.y > 10 || transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerBullet && collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                gameController.ChangeScore(10);
            }
        }
        else if (!isPlayerBullet && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().TakeDamage(); // should be called when an enemy bullet hits the player
            Destroy(gameObject); // Destroy the bullet after hitting the player
        }
    }
}
