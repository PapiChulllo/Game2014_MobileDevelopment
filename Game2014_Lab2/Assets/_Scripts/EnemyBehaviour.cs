using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    Boundry _verticalSpeedRange;
    [SerializeField]
    Boundry _horizontalSpeedRange;


    float _verticalSpeed;
    float _horizontalSpeed;

    [SerializeField]
    Boundry _verticalBoundry;
    [SerializeField]
    Boundry _horizontalBoundry;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalBoundry.max - _horizontalBoundry.min) + _horizontalBoundry.min,
 
                                  /* transform.position.x + _horizontalSpeed Time.deltaTime */
                                  transform.position.y + _verticalSpeed *Time.deltaTime);

        if (transform.position.y < _verticalBoundry.min)
        {
            Reset();
        }

        //if (transform.position.x > _horizontalBoundry.max || transform.position.x < _horizontalBoundry.min)
        //{
        //    _horizontalSpeed = -_horizontalSpeed;
        //}
    }
    private void Reset()
    {
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        _verticalSpeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalSpeed = Random.Range(_horizontalSpeedRange.min, _horizontalSpeedRange.max);
    }
}
