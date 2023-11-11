using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class levelCreator : MonoBehaviour
{
    [SerializeField] 
    private levelData _dataLevel;
    [SerializeField]
    private GameObject _cellPref;
    [SerializeField]
    private GameObject _groundTilesParent;
    [SerializeField]
    private GameObject _enemySpawnerPref, _wayPointPref, _lastWayPointPref;
    [SerializeField]
    private GameObject _wayPointsParent;


    private List<GameObject> _wayPoints = new List<GameObject>(_);

    public List<GameObject> wayPoints => _wayPoints;

    private void wayPointsInitial()
    {
        for (int i = 0; i < _dataLevel.xWayPointsPos.Length; ++i)
        {
            wayPoints.Add(null);
        }
    }

    /*private enum TileTypes
    {
        Regular,
        Broken,
        Horizontal,
        Vertical,
        LeftUp,
        RightUp,
        RightDown,
        LeftDown
    }*/

    void Start()
    {
        levelBuilding();
        //setDirectionsToWayPoints();
    }

    void levelBuilding()
    {
        Vector2 cellPosVector = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        int cellTypeId;
        

        for (int i = 0; i <  _dataLevel.FieldHeight ; ++i)
            for(int j = 0; j < _dataLevel.FieldWidth; ++j)
            {
                cellTypeId = int.Parse(_dataLevel.Way[i][j].ToString());

                // ������ �� ���������
                if (j == _dataLevel.xyPosSpawner.Item1 && i == _dataLevel.xyPosSpawner.Item2)
                {
                    // ���� ��������� �����
                    if (j == _dataLevel.xyPosLastWPoint.Item1 && i == _dataLevel.xyPosLastWPoint.Item2)
                        cellInstantiate(j, i, cellPosVector, cellTypeId, true, false, true);
                    else
                        cellInstantiate(j, i, cellPosVector, cellTypeId, true, false, false);

                    continue;
                }
                // ���� �������
                if (cellTypeId > 3)
                {
                    cellInstantiate(j, i, cellPosVector, cellTypeId, false, true, false);

                    continue;
                }

                

                cellInstantiate(j, i, cellPosVector, cellTypeId, false, false, false);
            }
    }


    void cellInstantiate(int x, int y, Vector2 cellPosVector, int cellTypeId, bool isSpawnPoint, bool isWayPoint, bool isLastWayPoint)
    {
        GameObject newCell = Instantiate(_cellPref);
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

            spawnPoint.GetComponent<SpawnPoint>().setInitialDirection(_dataLevel.initialDirection);

            _wayPoints.Insert(0, spawnPoint);
            Debug.Log("Added spawn point");
        }

        if (isWayPoint)
        {
            GameObject wayPoint = isLastWayPoint ? Instantiate(_lastWayPointPref) : Instantiate(_wayPointPref);
            wayPoint.transform.SetParent(_wayPointsParent.transform, false);
            wayPoint.transform.position = new Vector3(newCell.transform.position.x + newCell.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                                      newCell.transform.position.y + newCell.GetComponent<SpriteRenderer>().bounds.size.y / 2,
                                                      newCell.transform.position.z);
            if (isLastWayPoint)
                _wayPoints.Insert(_wayPoints.Count, wayPoint);
        }
    }
    
    /*void setDirectionsToWayPoints()
    {
        for(int i = 1; i < _wayPoints.Count - 1; ++i)
        {
            _wayPoints[i].GetComponent<WayPoint>().setNewDirection(_wayPoints[i + 1].transform.position - _wayPoints[i].transform.position);
            Debug.Log($"Customize {i} way point");
        }

        if (!_wayPoints[_wayPoints.Count - 2].TryGetComponent<WayPoint>(out var t))
            _wayPoints[_wayPoints.Count - 1].GetComponent<WayPoint>().setNewDirection(_wayPoints[_wayPoints.Count - 2].GetComponent<SpawnPoint>().initialDirection);
        else
            _wayPoints[_wayPoints.Count - 1].GetComponent<WayPoint>().setNewDirection(_wayPoints[_wayPoints.Count - 2].GetComponent<WayPoint>().newDirection);
    }*/

}
