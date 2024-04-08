using System.Collections;
using System.Collections.Generic;
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

    private GameObject _towerOnCeil;

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
        _towerOnCeil = null;
        _oldColor = Color.clear;
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
            _towerOnCeil = newTower;
            _store.SetIsTrieggeredToFalse();
            foreach (var obj in _mainCamera.GetComponent<levelCreator>().TilesWithTowers)
                obj.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (!_mainCamera.GetComponent<pauseAndReset>().isPaused && _isOccupied)
        {
            _store.SpendMoney(-(int)_storeData.Prices[(int)_store.CurrentTypeOfTower] / 10);
            deleteTower();
        }
    }

    private float getRangeByTowerType(TowerType tt)
    {
        if (tt == TowerType.Nothing)
            return 0;
        Debug.Log("Количество Tower Prefs: " + _storeData.TowerPrefs.Length);
        Debug.Log("(int)tt: " + (int)tt);
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

    //TODO:
    private void deleteTower()
    {
        if (!_isOccupied)
            return;

        Destroy(_towerOnCeil);
        _isOccupied = false;
        if (_oldColor != Color.clear)
        {
            this.GetComponent<SpriteRenderer>().color = _oldColor;
            _oldColor = Color.clear;
        }

    }
    private Color _oldColor;
    private void OnMouseEnter()
    {
        if (IsOccupied && !_mainCamera.GetComponent<pauseAndReset>().isPaused)
        {
            _oldColor = this.GetComponent<SpriteRenderer>().color;
            this.GetComponent<SpriteRenderer>().color = Color.red;
            return;
        }

        if (!_store.IsTriggered)
            return;

        _circleRadOfAttack.enabled = true;
        drawCicleOfAttack(getRangeByTowerType(_store.CurrentTypeOfTower));
    }

    private void OnMouseExit()
    {
        _circleRadOfAttack.enabled = false;
        if (_oldColor != Color.clear)
        {
            this.GetComponent<SpriteRenderer>().color = _oldColor;
            _oldColor = Color.clear;
        }
    }
}
