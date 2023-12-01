using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WavesData", menuName = "Data/New WavesData")]
public class LevelWavesData : ScriptableObject
{
    [SerializeField]
    private string[] _eachTypeOfEnemyCount;

    public string[] eachTypeOfEnemyCount => _eachTypeOfEnemyCount;
}
