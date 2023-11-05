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
    private GameObject _parent;
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
        levelBuilding();
    }

    void levelBuilding()
    {
        Vector2 cellPosVector = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

        int cellTypeId = -1;
        
        for (int i = 0; i <  _dataLevel.FieldHeight ; ++i)
            for(int j = 0; j < _dataLevel.FieldWidth; ++j)
            {
                cellTypeId = int.Parse(_dataLevel.Way[i][j].ToString());

                cellInstantiate(j, i, cellPosVector, cellTypeId);
            }
    }

    void cellInstantiate(int x, int y, Vector2 cellPosVector, int cellTypeId)
    {
        GameObject newCell = Instantiate(_cellPref);
        newCell.transform.SetParent(_parent.transform, false);

        newCell.GetComponent<SpriteRenderer>().sprite = _dataLevel.GroundSprites[cellTypeId];

        float cellSizeX = newCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float cellSizeY = newCell.GetComponent<SpriteRenderer>().bounds.size.y;

        newCell.transform.position = new Vector2(cellPosVector.x + (cellSizeX * x),
                                                 cellPosVector.y + (cellSizeY * -y));
        //Здесь хочу добавить кликабельность
        if (cellTypeId == 1)
        {
            //newCell.AddComponent<BoxCollider2D>().transform.position = newCell.transform.position;
            //newCell.GetComponent<changerCreator>().OnMouseDown();
        }
    }
    
}
