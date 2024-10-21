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
    Camera _camera;
    Vector2 _destination;

    GameController _gameController;

    bool _isMobilePlatform = true;

    // Player health
    public int health = 3;

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
    }

    void Move()
    {
        transform.position = _destination;
    }

    void GetTraditionalInput()
    {
        // calculate movement amount
        float axisX = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float axisY = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;
        //apply movement amount to transform
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
        // for boundary
        if (transform.position.x > _horizontalBoundry.max)
        {
            transform.position = new Vector3(_horizontalBoundry.min, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalBoundry.min)
        {
            transform.position = new Vector3(_horizontalBoundry.max, transform.position.y, 0);
        }

        // player passes the boundary, stop it on the edge 
        if (transform.position.y > _verticalBoundry.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundry.max, 0);
        }
        else if (transform.position.y < _verticalBoundry.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundry.min, 0);
        }
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
