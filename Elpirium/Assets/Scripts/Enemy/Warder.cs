using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Warder : Enemy, IMovable
{
    [SerializeField]
    float _speed, _health;
    [SerializeField]
    int _damage;
    //[SerializeField]
    //Sprite _sprite;


    private Vector2 _direction;

    private int _nextWaypoint;

    void Start()
    {
        _direction = GameObject.Find("spawnPointPref(Clone)").transform.position;//GetComponent<SpawnPoint>().initialDirection;
        _nextWaypoint = 1;
        mainCameraWPoints = GameObject.Find("Main Camera").GetComponent<levelCreator>().wayPoints;
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, mainCameraWPoints[_nextWaypoint].transform.position, _speed * Time.deltaTime);
    }

    List<GameObject> mainCameraWPoints;

    public void changeDirection()
    { 
        if (Vector2.Distance(transform.position, mainCameraWPoints[_nextWaypoint].transform.position) < 0.6f)
        {
            if (_nextWaypoint + 1 < mainCameraWPoints.Count)
                _nextWaypoint++;
            _direction = mainCameraWPoints[_nextWaypoint].GetComponent<WayPoint>().newDirection;
        }
    }


    public override void getDamage(float damage)
    {
        if (damage >= _health)
            Die();
        else
            _health -= damage;
    }

    public override void doDamage(int damage)
    {

    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public Vector2 Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }

    public override float Health => _health;

    public override int Damage => _damage;


    void Update()
    {
        Move();
        changeDirection();
        Win();
    }


    public override void Win()
    {
        if (Vector2.Distance(transform.position, mainCameraWPoints.Last().transform.position) < 0.1f)
            Die();
    }
}
