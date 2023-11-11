using System;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private DirectionsEnum _initialDirection;

    public void setDirection(GameObject somebody)
    {
        if (!somebody.TryGetComponent<IMovable>(out var t))
            throw new InvalidOperationException("Данный объект не может двигаться");


        somebody.GetComponent<IMovable>().Direction = _initialDirection;
    }

    public void spawnEnemy(Enemy enemy, GameObject enemyPref)
    {
        GameObject newEnemy = Instantiate(enemyPref);
        newEnemy.transform.position = this.GetComponent<Transform>().position;
        setDirection(newEnemy);
    }
}
