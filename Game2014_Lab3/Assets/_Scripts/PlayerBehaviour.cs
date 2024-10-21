using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Boundry _horizontalBoundry;
    [SerializeField]
    private Boundry _verticalBoundry;
    [SerializeField]
    private bool _isTestMobile;
    [SerializeField]
    private BulletManager bulletManager;  // Reference to BulletManager for shooting

    [SerializeField]
    public int health = 3; // Player starts with 3 health points

    Camera _camera;
    Vector2 _destination;
    GameController _gameController;
    bool _isMobilePlatform = true;

    // Shooting settings
    private float shootCooldown = 0.5f; // How fast the player can shoot
    private float nextShotTime = 0f; // When the next shot can happen

    void Start()
    {
        _camera = Camera.main;
        _gameController = FindObjectOfType<GameController>(); // Get a reference to the GameController

        if (!_isTestMobile)
        {
            // Check if we're running on a mobile platform
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                                Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }

    void Update()
    {
        if (_isMobilePlatform)
        {
            GetTouchInput(); // Use touch input on mobile
        }
        else
        {
            GetTraditionalInput(); // Use keyboard/mouse input otherwise
        }

        Move(); // Handle movement
        CheckBoundaries(); // Make sure the player doesn't go out of bounds

        // Shooting logic
        if (Time.time > nextShotTime)
        {
            Shoot(); // Shoot a bullet
            nextShotTime = Time.time + shootCooldown; // Set the next time the player can shoot
        }
    }

    void Move()
    {
        transform.position = _destination; // Move the player to the new position
    }

    void GetTraditionalInput()
    {
        // Get movement input and apply to player's position
        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        _destination = new Vector3(axisX + transform.position.x, axisY + transform.position.y, 0);
    }

    void GetTouchInput()
    {
        // Handle touch movement for mobile
        foreach (Touch touch in Input.touches)
        {
            _destination = _camera.ScreenToWorldPoint(touch.position);
            _destination = Vector2.Lerp(transform.position, _destination, _speed * Time.deltaTime);
        }
    }

    void CheckBoundaries()
    {
        // Prevent the player from moving outside of the horizontal bounds
        if (transform.position.x > _horizontalBoundry.max)
        {
            transform.position = new Vector3(_horizontalBoundry.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundry.min)
        {
            transform.position = new Vector3(_horizontalBoundry.max, transform.position.y, 0);
        }

        if (transform.position.y > _verticalBoundry.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundry.max, 0);
        }
        else if (transform.position.y < _verticalBoundry.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundry.min, 0);
        }
    }

    private void Shoot()
    {
        GameObject bullet = bulletManager.GetBullet(true); // True means it's a player bullet
        bullet.transform.position = transform.position;
        bullet.SetActive(true); // Activate the bullet
    }

    public void TakeDamage()
    {
        health--; // Reduce player health by 1

        if (health <= 0)
        {
            Die(); // Call Die method if health reaches zero
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        gameObject.SetActive(false); // Deactivate the player when health is zero
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _gameController.ChangeScore(9);
            StartCoroutine(collision.GetComponent<EnemyBehaviour>().DyingRoutine());
        }
    }
}