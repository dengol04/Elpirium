using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileWithTower : MonoBehaviour
{
    [SerializeField]
    private storeData _storeData;
    
    private GameObject _mainCamera;
    private bool _isOccupied;

    public bool IsOccupied => _isOccupied;

    private void Awake()
    {
        _mainCamera = GameObject.Find("Main Camera");
        _isOccupied = false;
    }

    private void OnMouseDown()
    {
        if (_mainCamera.GetComponent<Store>().IsTriggered && !_isOccupied)
        {
            Store store = _mainCamera.GetComponent<Store>();
            TowerType typeOfTower = store.CurrentTypeOfTower;
            GameObject newTower = Instantiate(_storeData.TowerPrefs[(int)typeOfTower]);
            newTower.transform.position = new Vector2(transform.position.x + gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                                      transform.position.y + gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2);
            store.SpendMoney(_storeData.Prices[(int)typeOfTower]);
            _isOccupied = true;
            store.SetIsTrieggeredToFalse();
            foreach (var obj in _mainCamera.GetComponent<levelCreator>().TilesWithTowers)
                obj.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
