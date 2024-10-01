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

    SpriteRenderer _spriteRenderer;

    Color[] _colors = {Color.green, Color.cyan, Color.white, Color.magenta, Color.yellow};

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    public IEnumerator DyingRoutine()
    {

        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.2f);
        _spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
 
    private void Reset()
    {

        _spriteRenderer.color = _colors[Random.Range(0,_colors.Length)];
        _spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
        transform.position = new Vector2(Random.Range(_horizontalBoundry.min, _horizontalBoundry.max), _verticalBoundry.max);
        transform.localScale = new Vector3(1 + Random.Range(-.3f, .3f), 1 + Random.Range(-.3f, .3f), 1f);
        _verticalSpeed = Random.Range(_verticalSpeedRange.min, _verticalSpeedRange.max);
        _horizontalSpeed = Random.Range(_horizontalSpeedRange.min, _horizontalSpeedRange.max);
    }
}
