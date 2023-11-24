using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    
    [SerializeField]
    private float _timeBtwSpawn;
    
    [SerializeField]
    private levelData _dataLevel;

    [SerializeField]
    private LevelWavesData _dataWaves;

    private GameObject[] _enemiesPrefs;

    private GameObject _enemyParent;

    private List<int>[] eachTypeOfEnemyCountLst;

    private int _currentWave;

    private void Start()
    {
        _enemyParent = GameObject.Find("enemies");

        _enemiesPrefs = _dataLevel.EnemyPrefs;

        eachTypeOfEnemyCountLst = new List<int>[_dataWaves.eachTypeOfEnemyCount.Length];//_dataWaves.eachTypeOfEnemyCount.Length);

        for (int i = 0; i < _dataWaves.eachTypeOfEnemyCount.Length; ++i)
        {
            eachTypeOfEnemyCountLst[i] = new List<int>();  //.Add(new List<int>());
            string oneWaweEnemyData = _dataWaves.eachTypeOfEnemyCount[i];
            string[] oneWaweEnemyDataUnits = oneWaweEnemyData.Split(" ");
            for (int j = 0; j < oneWaweEnemyDataUnits.Length; ++j)
            {               
                if (oneWaweEnemyDataUnits[j] != "")
                    eachTypeOfEnemyCountLst[i].Add(int.Parse(oneWaweEnemyDataUnits[j]));
            }
        }

        Debug.Log("��������� ��������� � ������� ����� �����������: " + eachTypeOfEnemyCountLst.Length);

        _currentWave = 1;

        StartCoroutine(wavesSpawn());

    }

    

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
                    throw new ArgumentOutOfRangeException("��� ������ �����");
            }
            newEnemy.transform.SetParent(_enemyParent.transform, false);
            newEnemy.transform.position = transform.position;
            
            //Debug.Log($"{typeOfEnemy.ToString()} has spawned");

            yield return new WaitForSeconds(2f);
        }

        yield break;
    }

    IEnumerator wavesSpawn()
    {
        for (int i = 0; i < eachTypeOfEnemyCountLst.Length; ++i)
        {
            for (int j = 1; j < eachTypeOfEnemyCountLst[i].Count; j += 2)
                StartCoroutine(spawnEnemy(eachTypeOfEnemyCountLst[i][j], (EnemyType)eachTypeOfEnemyCountLst[i][j - 1]));

            Debug.Log($"{i} wave is over");

            yield return new WaitForSeconds(10f);
        }

        yield break;
    }


    /*IEnumerator wavesSpawn(int k)
    {
        if (_currentWave < eachTypeOfEnemyCountLst.Count)
        {
            for (int j = 1; j < eachTypeOfEnemyCountLst[_currentWave].Count; j += 2)
                StartCoroutine(spawnEnemy(eachTypeOfEnemyCountLst[_currentWave][j], (EnemyType)eachTypeOfEnemyCountLst[_currentWave][j - 1]));

            Debug.Log($"{_currentWave} wave is over");

            yield return new WaitForSeconds(2f);

            ++_currentWave;
        }
            yield break;
    }*/

    /*public void wavesSpawn()
    {
        Debug.Log("��������� ��������� � ������� ����� �����������: " + eachTypeOfEnemyCountLst.Length);

        if (_currentWave < eachTypeOfEnemyCountLst.Length)
        {
            for (int j = 1; j < eachTypeOfEnemyCountLst[_currentWave].Count; j += 2)
                StartCoroutine(spawnEnemy(eachTypeOfEnemyCountLst[_currentWave][j], (EnemyType)eachTypeOfEnemyCountLst[_currentWave][j - 1]));

            Debug.Log($"{_currentWave} wave is over");

            ++_currentWave;
        }
    }*/

}
