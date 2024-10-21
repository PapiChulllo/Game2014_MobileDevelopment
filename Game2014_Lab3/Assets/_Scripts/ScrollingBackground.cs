using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;

    [SerializeField]
    private Boundry _boundry;

    [SerializeField]
    private Vector3 _spawnPosition;

    private Vector3 _direction = Vector3.down;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime; // multiply by deltatime so it doesnt update it based on computer fps

        if (transform.position.y < _boundry.min)
        {
            transform.position = _spawnPosition;
        }
        
    }
}
