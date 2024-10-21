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
    bool _isTestMobile;
    [SerializeField]
    private BulletManager bulletManager;  // BulletManager reference for shooting

    Camera _camera;
    Vector2 _destination;
    GameController _gameController;
    bool _isMobilePlatform = true;

    // Player health
    public int health = 3;

    // Shooting parameters
    private float shootCooldown = 0.2f; // Time between shots
    private float nextShotTime = 0f; // Time of the next shot

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _gameController = FindObjectOfType<GameController>();

        if (!_isTestMobile)
        {
            _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                                Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMobilePlatform)
        {
            GetTouchInput();
        }
        else
        {
            GetTraditionalInput();
        }

        Move();
        CheckBoundaries();

        // Continuously shoot
        if (Time.time > nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + shootCooldown;
        }
    }

    void Move()
    {
        transform.position = _destination;
    }

    void GetTraditionalInput()
    {
        // Calculate movement amount
        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        // Apply movement amount to transform
        _destination = new Vector3(axisX + transform.position.x, axisY + transform.position.y, 0);
    }

    void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            _destination = _camera.ScreenToWorldPoint(touch.position);
            _destination = Vector2.Lerp(transform.position, _destination, _speed * Time.deltaTime);
        }
    }

    void CheckBoundaries()
    {
        // Horizontal boundaries
        if (transform.position.x > _horizontalBoundry.max)
        {
            transform.position = new Vector3(_horizontalBoundry.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundry.min)
        {
            transform.position = new Vector3(_horizontalBoundry.max, transform.position.y, 0);
        }

        // Vertical boundaries
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
        // Get a bullet from BulletManager and set it at the player's position
        GameObject bullet = bulletManager.GetBullet(true); // True indicates it's a player bullet
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

    public void TakeDamage()
    {
        // Reduce player health
        health--;

        // Check if the player has died
        if (health <= 0)
        {
            Debug.Log("Player Died!");
            // You can add code here to handle game over (e.g., restarting the game, showing Game Over screen, etc.)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _gameController.ChangeScore(9);
            // Destroy or disable the enemy
            StartCoroutine(collision.GetComponent<EnemyBehaviour>().DyingRoutine());
        }
    }
}
