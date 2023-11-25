using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private List<List<int>> eachTypeOfEnemyCountLst;

    private int _currentWave;

    [SerializeField]
    private bool _isWaveButtomPressed = false;

    private GameObject _wavesControllerPanel;

    private void Start()
    {
        enemyListInitial();
        Debug.Log("Количство элементов в массиве выолн противников: " + eachTypeOfEnemyCountLst.Count);
        GameObject.Find("textOnWavesControllerButton").GetComponent<Text>().text = "Start the wave";
        StartCoroutine(wavesSpawn());
    }

    private void Awake()
    {
        _enemyParent = GameObject.Find("enemies");
        _enemiesPrefs = _dataLevel.EnemyPrefs;
        eachTypeOfEnemyCountLst = new List<List<int>>(_dataWaves.eachTypeOfEnemyCount.Length);
        _currentWave = 1;
        _isSpawnEnemyWorking = false;
        _isWaveButtomPressed = false;
        _wavesControllerPanel = GameObject.Find("wavesControllerPanel");
    }

    public void pressedWaveButton()
    {
        setWaveButtonToPressed();
    }

    private void setWaveButtonToPressed()
    {
        _isWaveButtomPressed = true;
        //_wavesControllerPanel.SetActive(false);
        Debug.Log("Pressed wave button" + _isWaveButtomPressed);
    }

    private void enemyListInitial()
    {
        for (int i = 0; i < _dataWaves.eachTypeOfEnemyCount.Length; ++i)
        {
            eachTypeOfEnemyCountLst.Add(new List<int>());  //.Add(new List<int>());
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
                    throw new ArgumentOutOfRangeException("Нет токого врага");
            }
            newEnemy.transform.SetParent(_enemyParent.transform, false);
            newEnemy.transform.position = transform.position;
            
            //Debug.Log($"{typeOfEnemy.ToString()} has spawned");

            yield return new WaitForSeconds(2f);
        }
        _isSpawnEnemyWorking = false;
        yield break;
    }

    IEnumerator wavesSpawn()
    {
        Debug.Log("Starting Wave Spawning" + _isWaveButtomPressed);

        GameObject.Find("textOnWavesControllerButton").GetComponent<Text>().text = "Start the next wave";

        for (int i = 0; i < eachTypeOfEnemyCountLst.Count; ++i)
        {

            //yield return new WaitUntil(() => _isWaveButtomPressed);
            while (!_isWaveButtomPressed)
                yield return null;
            Debug.Log("Зашёл в цикл");
            _isWaveButtomPressed = false;

            for (int j = 1; j < eachTypeOfEnemyCountLst[i].Count; j += 2)
            {
                StartCoroutine(spawnEnemy(eachTypeOfEnemyCountLst[i][j], (EnemyType)eachTypeOfEnemyCountLst[i][j - 1]));
                yield return new WaitUntil(() => !_isSpawnEnemyWorking);
                yield return new WaitForSeconds(4f);
            }

            Debug.Log($"{i} wave is over");
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
        Debug.Log("Количство элементов в массиве выолн противников: " + eachTypeOfEnemyCountLst.Length);

        if (_currentWave < eachTypeOfEnemyCountLst.Length)
        {
            for (int j = 1; j < eachTypeOfEnemyCountLst[_currentWave].Count; j += 2)
                StartCoroutine(spawnEnemy(eachTypeOfEnemyCountLst[_currentWave][j], (EnemyType)eachTypeOfEnemyCountLst[_currentWave][j - 1]));

            Debug.Log($"{_currentWave} wave is over");

            ++_currentWave;
        }
    }*/

}
