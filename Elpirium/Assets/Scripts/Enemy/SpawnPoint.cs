using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelWavesData _dataWaves;
    [SerializeField] private levelData _dataLevel;

    [Header("Attributes")]
    [SerializeField] private float _timeBtwSpawn;
    
    private GameObject[] _enemiesPrefs;
    private GameObject _enemyParent;
    private List<List<int>> eachTypeOfEnemyCountLst;
    private GameObject _mainCamera;
    private static int _enemyAmount;

    public int enemyAmount => _enemyAmount;

    private void Start()
    {
        enemyListInitial();
        setEnemyAmount();
        Debug.Log("��������� ��������� � ������� ����� �����������: " + eachTypeOfEnemyCountLst.Count);
        StartCoroutine(wavesSpawn());
    }

    public void killEnemy()
    {
        if (--_enemyAmount <= 0)
        {
            Debug.Log(_enemyAmount);
            SceneManager.LoadScene("Win");
        }
    }

    private void Awake()
    {
        _enemyParent = GameObject.Find("enemies");
        _enemiesPrefs = _dataLevel.EnemyPrefs;
        eachTypeOfEnemyCountLst = new List<List<int>>(_dataWaves.eachTypeOfEnemyCount.Length);
        _isSpawnEnemyWorking = false;
        _mainCamera = GameObject.Find("Main Camera");
    }

    private void setEnemyAmount()
    {
        for (int i = 0; i < eachTypeOfEnemyCountLst.Count; ++i)
            for (int j = 1; j < eachTypeOfEnemyCountLst[i].Count; j += 2)
            {
                _enemyAmount += eachTypeOfEnemyCountLst[i][j];
            }
    }

    private void enemyListInitial()
    {
        for (int i = 0; i < _dataWaves.eachTypeOfEnemyCount.Length; ++i)
        {
            eachTypeOfEnemyCountLst.Add(new List<int>());
            string oneWaweEnemyData = _dataWaves.eachTypeOfEnemyCount[i];
            string[] oneWaweEnemyDataUnits = oneWaweEnemyData.Split(" ");
            for (int j = 0; j < oneWaweEnemyDataUnits.Length; ++j)
            {
                if (oneWaweEnemyDataUnits[j] != "")
                    eachTypeOfEnemyCountLst[i].Add(int.Parse(oneWaweEnemyDataUnits[j]));
            }
        }
    }

    private bool _isSpawnEnemyWorking;

    IEnumerator spawnEnemy(int count, EnemyType typeOfEnemy)
    {
        _isSpawnEnemyWorking = true;
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

            yield return new WaitForSeconds(1f);
        }
        _isSpawnEnemyWorking = false;
        yield break;
    }

    IEnumerator wavesSpawn()
    {
        for (int i = 0; i < eachTypeOfEnemyCountLst.Count; ++i)
        {
            yield return new WaitUntil(() => _mainCamera.GetComponent<waveControllerButton>().isPressed);

            for (int j = 1; j < eachTypeOfEnemyCountLst[i].Count; j += 2)
            {
                StartCoroutine(spawnEnemy(eachTypeOfEnemyCountLst[i][j], (EnemyType)eachTypeOfEnemyCountLst[i][j - 1]));
                yield return new WaitUntil(() => !_isSpawnEnemyWorking);
                yield return new WaitForSeconds(4f);
            }

            Debug.Log($"{i} wave is over");
            if (i < eachTypeOfEnemyCountLst.Count - 1)
                _mainCamera.GetComponent<waveControllerButton>().unpressButton();
        }

        SceneManager.LoadScene("Win");

        yield break;
    }
}
