using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WavesData", menuName = "Data/New WavesData")]
public class LevelWavesData : ScriptableObject
{
    //[SerializeField]
    //private int _wawesCount;
    //[SerializeField]
    //private GameObject[] _enemyPrefs;
    //[SerializeField]
    //private int[] _enemyOnEachWaveCount;
    [SerializeField]
    private string[] _eachTypeOfEnemyCount;

    public string[] eachTypeOfEnemyCount => _eachTypeOfEnemyCount;



}
