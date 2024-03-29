using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New LevelData")]
public class levelData : ScriptableObject
{
    [Header("References")]
    [SerializeField] private Sprite[] _groundSprites;
    [SerializeField] private GameObject[] _enemyPrefs;


    
    
    [Header("Attributes")]
    [SerializeField] private uint _fieldHeight, _fieldWidth;
    [SerializeField] private uint _xPosSpawner, _yPosSpawner;
    [SerializeField] private int _xPosLastWPoint, _yPosLastWPoint;
    [SerializeField] private int[] _numsOfWPoints;
    [SerializeField] private int[] _wavesEnemyCount;
    [SerializeField] private float _startMoney;
    [SerializeField] private string[] _way;
    [SerializeField] private int _currentLevel;

    public int CurrentLevel => _currentLevel;
    public string[] Way => _way;
    public Sprite[] GroundSprites => _groundSprites;
    public uint FieldHeight => _fieldHeight;
    public uint FieldWidth => _fieldWidth;
    public (uint, uint) xyPosSpawner => (_xPosSpawner, _yPosSpawner);
    public (int, int) xyPosLastWPoint => (_xPosLastWPoint, _yPosLastWPoint);
    public int[] numsOfWPoints => _numsOfWPoints;
    public GameObject[] EnemyPrefs => _enemyPrefs;
    public float StartMoney => _startMoney;


}

