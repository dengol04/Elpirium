using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class levelCreator : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private levelData _dataLevel;
    [SerializeField] private GameObject _cellPref;
    [SerializeField] private GameObject _groundTilesParent;
    [SerializeField] private GameObject _enemySpawnerPref, _wayPointPref, _lastWayPointPref;
    [SerializeField] private GameObject _wayPointsParent;
    [SerializeField] private GameObject _tileWithTowerPref;

    private List<GameObject> _tilesWithTowers;
    private List<GameObject> _wayPoints = new List<GameObject>();

    public List<GameObject> TilesWithTowers => _tilesWithTowers;
    public levelData DataLevel => _dataLevel;
    public List<GameObject> wayPoints => _wayPoints;

    private void wayPointsInitial()
    {
        for (int i = 0; i < _dataLevel.numsOfWPoints.Length + 2; ++i)
        {
            _wayPoints.Add(null);
        }
    }

    private enum TileTypes
    {
        Regular,
        Broken,
        Horizontal,
        Vertical,
        LeftUp,
        RightUp,
        RightDown,
        LeftDown
    }

    void Start()
    {
        wayPointsInitial();
        Debug.Log(_wayPoints.Count);

        levelBuilding();

        if (wayPoints[wayPoints.Count - 1] == null)
            throw new ArgumentNullException("Ќет последнего поинта");

        if (wayPoints.Any(x => x == null))
            throw new ArgumentNullException("Ќедозаполнил список поинтов");
    }

    void levelBuilding()
    {
        _tilesWithTowers = new List<GameObject>();

        Vector2 cellPosVector = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        int cellTypeId;

        int count = 0;

        for (int i = 0; i <  _dataLevel.FieldHeight ; ++i)
            for(int j = 0; j < _dataLevel.FieldWidth; ++j)
            {
                ++count;

                cellTypeId = int.Parse(_dataLevel.Way[i][j].ToString());

                // €чейка со спавнером
                if (j == _dataLevel.xyPosSpawner.Item1 && i == _dataLevel.xyPosSpawner.Item2)
                {
                    cellInstantiate(j, i, cellPosVector, cellTypeId, true, false);
                    continue;
                }
                // если поворот
                if (cellTypeId > 3)
                {
                    cellInstantiate(j, i, cellPosVector, cellTypeId, false, true, false, count);
                    continue;
                }
                // если последний поинт
                if (j == _dataLevel.xyPosLastWPoint.Item1 && i == _dataLevel.xyPosLastWPoint.Item2)
                {
                    cellInstantiate(j, i, cellPosVector, cellTypeId, false, true, true);
                    continue;
                }

                cellInstantiate(j, i, cellPosVector, cellTypeId, false, false);
            }
    }


    void cellInstantiate(int x, int y, Vector2 cellPosVector, int cellTypeId, bool isSpawnPoint, bool isWayPoint, bool isLastWayPoint = false, int numOfCell = 0)
    {
        GameObject newCell = cellTypeId == 1 ? Instantiate(_tileWithTowerPref) : Instantiate(_cellPref);

        if (cellTypeId == 1)
            _tilesWithTowers.Add(newCell);

        newCell.transform.SetParent(_groundTilesParent.transform, false);

        newCell.GetComponent<SpriteRenderer>().sprite = _dataLevel.GroundSprites[cellTypeId];


        float cellSizeX = newCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float cellSizeY = newCell.GetComponent<SpriteRenderer>().bounds.size.y;

        newCell.transform.position = new Vector2(cellPosVector.x + (cellSizeX * x), cellPosVector.y + (cellSizeY * -y));

        if (isSpawnPoint)
        {
            GameObject spawnPoint = Instantiate(_enemySpawnerPref);
            spawnPoint.transform.SetParent(_wayPointsParent.transform, false);
            spawnPoint.transform.position = new Vector3(newCell.transform.position.x + newCell.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                                        newCell.transform.position.y + newCell.GetComponent<SpriteRenderer>().bounds.size.y / 2,
                                                        newCell.transform.position.z);

            _wayPoints[0] = spawnPoint;
            Debug.Log("Added spawn point");
        }

        if (isWayPoint)
        {
            GameObject wayPoint = new GameObject();

            if (isLastWayPoint)
                wayPoint = Instantiate(_lastWayPointPref);
            else
                wayPoint = Instantiate(_wayPointPref);

            wayPoint.transform.SetParent(_wayPointsParent.transform, false);
            wayPoint.transform.position = new Vector3(newCell.transform.position.x + newCell.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                                      newCell.transform.position.y + newCell.GetComponent<SpriteRenderer>().bounds.size.y / 2,
                                                      newCell.transform.position.z);

            if (isLastWayPoint)
                _wayPoints[_wayPoints.Count - 1] = wayPoint;
            else
                _wayPoints[_dataLevel.numsOfWPoints.ToList().FindIndex(x => x == 50) + 1] = wayPoint;
        }
    }
}
