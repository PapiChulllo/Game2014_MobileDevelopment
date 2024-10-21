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
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime; 

        if (transform.position.y < _boundry.min)
        {
            transform.position = _spawnPosition;
        }
        
    }
}
