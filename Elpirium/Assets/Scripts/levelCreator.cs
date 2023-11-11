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
    private GameObject _enemySpawnerPref, _wayPointPref;
    [SerializeField]
    private GameObject _wayPointsParent;

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
    }

    void levelBuilding()
    {
        Vector2 cellPosVector = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        int cellTypeId;
        

        for (int i = 0; i <  _dataLevel.FieldHeight ; ++i)
            for(int j = 0; j < _dataLevel.FieldWidth; ++j)
            {
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
                    cellInstantiate(j, i, cellPosVector, cellTypeId, false, true);
                    continue;
                }

                

                cellInstantiate(j, i, cellPosVector, cellTypeId, false, false);
            }
    }


    void cellInstantiate(int x, int y, Vector2 cellPosVector, int cellTypeId, bool isSpawnPoint, bool isWayPoint)
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
        }

        if (isWayPoint)
        {
            GameObject wayPoint = Instantiate(_wayPointPref);
            wayPoint.transform.SetParent(_wayPointsParent.transform, false);
            wayPoint.transform.position = new Vector3(newCell.transform.position.x + newCell.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                            newCell.transform.position.y + newCell.GetComponent<SpriteRenderer>().bounds.size.y / 2,
                                            newCell.transform.position.z);

            wayPoint.GetComponent<WayPoint>().setNewDirection
        }
    }
    
}
