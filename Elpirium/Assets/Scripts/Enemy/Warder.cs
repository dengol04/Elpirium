using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Warder : Enemy
{
    [SerializeField]
    float _speed, _health;
    [SerializeField]
    int _damage;
    [SerializeField]
    float _award;
    [SerializeField]
    GameObject _spawnPointPref;

    private float _currentHealth;
    private int _nextWaypoint;
    private GameObject mainCamera;

    private void Awake()
    {
        _currentHealth = _health;
    }

    private void updateHealthBar()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Slider>().value = _currentHealth;
    }

    void Start()
    {
        _nextWaypoint = 1;
        mainCamera = GameObject.Find("Main Camera");
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Slider>().maxValue = _health;
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, mainCamera.GetComponent<levelCreator>().wayPoints[_nextWaypoint].transform.position, _speed * Time.deltaTime);
    }

    public void changeDirection()
    { 
        if (Vector2.Distance(transform.position, mainCamera.GetComponent<levelCreator>().wayPoints[_nextWaypoint].transform.position) < 0.6f)
        {
            if (_nextWaypoint + 1 < mainCamera.GetComponent<levelCreator>().wayPoints.Count)
                _nextWaypoint++;
        }
    }

    public override void getDamage(float damage)
    {
        if (damage >= _currentHealth)
            DieByTower();
        else
            _currentHealth -= damage;
    }

    public override void doDamage()
    {
        mainCamera.GetComponent<baseHealth>().getDamage(_damage);
    }

    public override void Die()
    {
        Destroy(gameObject);
        _spawnPointPref.GetComponent<SpawnPoint>().killEnemy();
    }

    public void DieByTower()
    {
        Die();
        mainCamera.GetComponent<Store>().GetMoney(_award);
    }

    public override float Health => _currentHealth;
    public override int Damage => _damage;

    void Update()
    {
        updateHealthBar();
        Move();
        changeDirection();
        Win();
    }

    public override void Win()
    {
        if (Vector2.Distance(transform.position, mainCamera.GetComponent<levelCreator>().wayPoints.Last().transform.position) < 0.1f)
        {
            doDamage();
            Die();
        }
    }
}
