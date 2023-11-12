using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //[SerializeField]
    //private Vector2 _initialDirection;
    //[SerializeField]
    //private GameObject _warderPref;
    //[SerializeField]
    private GameObject[] _enemiesPrefs;
    [SerializeField]
    private float _timeBtwSpawn;
    //[SerializeField]
    private GameObject _enemyParent;

    private void Start()
    {
        _enemyParent = GameObject.Find("enemies");

        _enemiesPrefs = GameObject.Find("Main Camera").GetComponent<levelCreator>().DataLevel.EnemyPrefs;
    }

    /*public Vector2 initialDirection => _initialDirection;

    public void setInitialDirection(Vector2 initialDirection)
    {
        _initialDirection = initialDirection;
    }*/

    /*public void setDirection(GameObject somebody)
    {
        if (!somebody.TryGetComponent<IMovable>(out var t))
            throw new InvalidOperationException("Данный объект не может двигаться");


        somebody.GetComponent<IMovable>().Direction = _initialDirection;
    }*/

    private void Update()
    {
        if (_timeBtwSpawn <= 0)
        {
            StartCoroutine(spawnEnemy(1, EnemyType.Warder));
            _timeBtwSpawn = 4;
        }

        _timeBtwSpawn -= Time.deltaTime;
    }

    /*IEnumerator spawnEnemy()
    {
        GameObject newEnemy = Instantiate(_warderPref);
        newEnemy.transform.SetParent(_enemyParent.transform, false);
        newEnemy.transform.position = transform.position;//gameObject.transform.position;
        Debug.Log("Warder has swapned");
        //setDirection(newEnemy);

        yield return new WaitForSeconds(0.5f);
    }*/

    IEnumerator spawnEnemy(int count, EnemyType typeOfEnemy)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject newEnemy = new GameObject();
            switch (typeOfEnemy)
            {
                case EnemyType.Warder:
                    newEnemy = Instantiate(_enemiesPrefs.First(x => x.name == "warderPref"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Нет токого врага");
            }
            newEnemy.transform.SetParent(_enemyParent.transform, false);
            newEnemy.transform.position = transform.position;
            Debug.Log($"{typeOfEnemy.ToString()} has spawned");
        }


        yield return new WaitForSeconds(0.5f);
    }

}
