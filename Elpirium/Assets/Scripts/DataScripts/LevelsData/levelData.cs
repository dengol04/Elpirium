using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New LevelData")]
public class levelData : ScriptableObject
{
    [SerializeField]
    private string[] _way;
    [SerializeField]
    private Sprite[] _groundSprites;
    [SerializeField]
    private uint _fieldHeight, _fieldWidth;
    [SerializeField]
    private uint _xPosSpawner, _yPosSpawner;
    [SerializeField]
    private Vector2 _initialDirection;
    [SerializeField]
    private uint _xPosLastWPoint, _yPosLastWPoint;
    [SerializeField]
    private int[] _xWayPointsPos;
    [SerializeField]
    private int[] _yWayPointsPos;

    public string[] Way => _way;
    public Sprite[] GroundSprites => _groundSprites;
    public uint FieldHeight => _fieldHeight;
    public uint FieldWidth => _fieldWidth;
    public (uint, uint) xyPosSpawner => (_xPosSpawner, _yPosSpawner);
    public Vector2 initialDirection => _initialDirection;
    public (uint, uint) xyPosLastWPoint => (_xPosLastWPoint, _yPosLastWPoint);
    public int[] xWayPointsPos => _xWayPointsPos;
    public int[] yWayPointsPos => _yWayPointsPos;


}

