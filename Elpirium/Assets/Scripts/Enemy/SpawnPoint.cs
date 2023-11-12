using System;
using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private Vector2 _initialDirection;
    [SerializeField]
    private GameObject _warderPref;
    [SerializeField]
    private float _timeBtwSpawn;
    [SerializeField]
    private GameObject _enemyParent;

    public Vector2 initialDirection => _initialDirection;

    public void setInitialDirection(Vector2 initialDirection)
    {
        _initialDirection = initialDirection;
    }

    public void setDirection(GameObject somebody)
    {
        if (!somebody.TryGetComponent<IMovable>(out var t))
            throw new InvalidOperationException("Данный объект не может двигаться");


        somebody.GetComponent<IMovable>().Direction = _initialDirection;
    }

    private void Update()
    {
        if (_timeBtwSpawn <= 0)
        {
            StartCoroutine(spawnEnemy());
            _timeBtwSpawn = 4;
        }

        _timeBtwSpawn -= Time.deltaTime;
    }

    IEnumerator spawnEnemy()
    {
        GameObject newEnemy = Instantiate(_warderPref);
        newEnemy.transform.position = transform.position;
        //newEnemy.transform.SetParent(_enemyParent.transform, false);
        Debug.Log("Warder has swapned");
        setDirection(newEnemy);

        yield return new WaitForSeconds(0.5f);
    }

}
