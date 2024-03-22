using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Tilemaps;
using UnityEngine;

public class tileWithTower : MonoBehaviour
{
    [SerializeField]
    private storeData _storeData;
    [SerializeField]
    private LineRenderer _circleRadOfAttack;
    
    private GameObject _mainCamera;
    private bool _isOccupied;
    private Store _store;

    public bool IsOccupied => _isOccupied;

    private void Awake()
    {
        _mainCamera = GameObject.Find("Main Camera");
        _isOccupied = false;
        _store = _mainCamera.GetComponent<Store>();
        _circleRadOfAttack.positionCount = 600;
        _circleRadOfAttack.transform.position = transform.position;
        _circleRadOfAttack.startColor = Color.white;
        _circleRadOfAttack.endColor = Color.white;
        _circleRadOfAttack.enabled = false;
    }

    private void OnMouseDown()
    {
        if (_mainCamera.GetComponent<Store>().IsTriggered && !_isOccupied)
        {
            TowerType typeOfTower = _store.CurrentTypeOfTower;
            GameObject newTower = Instantiate(_storeData.TowerPrefs[(int)typeOfTower]);
            newTower.transform.position = new Vector2(transform.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                                      transform.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            _store.SpendMoney(_storeData.Prices[(int)typeOfTower]);
            _isOccupied = true;
            _store.SetIsTrieggeredToFalse();
            foreach (var obj in _mainCamera.GetComponent<levelCreator>().TilesWithTowers)
                obj.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private float getRangeByTowerType(TowerType tt)
    {
        if (tt == TowerType.Nothing)
            return 0;

        return _storeData.TowerPrefs[(int)tt].GetComponent<Tower>().range;
    }

    private void drawCicleOfAttack(float rad)
    {
        for (int i = 0; i < _circleRadOfAttack.positionCount; ++i)
        {
            float cirProg = (float)i / _circleRadOfAttack.positionCount;
            float radian = cirProg * 2 * Mathf.PI;
            float xScale = Mathf.Cos(radian);
            float yScale = Mathf.Sin(radian);

            float x = rad * xScale;
            float y = rad * yScale;

            Vector2 currPos = new Vector2(transform.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2 + x, 
                                          transform.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2 + y);
            _circleRadOfAttack.SetPosition(i, currPos);
        }
    }

    private void OnMouseEnter()
    {
        if (!_store.IsTriggered || IsOccupied)
            return;

        _circleRadOfAttack.enabled = true;
        drawCicleOfAttack(getRangeByTowerType(_store.CurrentTypeOfTower));
    }

    private void OnMouseExit()
    {
        _circleRadOfAttack.enabled = false;
    }
}
