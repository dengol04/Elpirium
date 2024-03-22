using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField]
    private storeData _storeData;
    [SerializeField]
    private GameObject _textMoney;

    private TowerType _currentTypeOfTower;
    private bool _isTriggered;
    private float _currentAmountOfMoney;
    private GameObject _mainCamera;

    public TowerType CurrentTypeOfTower => _currentTypeOfTower;
    public float CurrentAmountOfMoney => _currentAmountOfMoney;
    public bool IsTriggered => _isTriggered;

    private void Awake()
    {
        _isTriggered = false;
        _currentTypeOfTower = TowerType.Nothing;
        _currentAmountOfMoney = _storeData.StartMoney;
        _mainCamera = GameObject.Find("Main Camera");
        _textMoney.GetComponent<Text>().text = _currentAmountOfMoney.ToString();
    }

    public void SetIsTrieggeredToFalse()
    {
        _isTriggered = false;
    }

    public void LightAllTilesWithTowers()
    {
        foreach (var obj in _mainCamera.GetComponent<levelCreator>().TilesWithTowers)
        {
            if (!obj.GetComponent<tileWithTower>().IsOccupied)
                obj.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    public void SetDefaultColorToTowersTiles()
    {
        foreach (var obj in _mainCamera.GetComponent<levelCreator>().TilesWithTowers)
            obj.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetCurrentTypeOfTower(int tt)
    {
        if (_currentAmountOfMoney >= _storeData.Prices[tt] && _mainCamera.GetComponent<levelCreator>().TilesWithTowers.Find(x => !x.GetComponent<tileWithTower>().IsOccupied) != null)
        {
            LightAllTilesWithTowers();
            _isTriggered = true;
            _currentTypeOfTower = (TowerType)tt;
            Debug.Log((int)_currentTypeOfTower);
        }
    }

    public void SpendMoney(float cost)
    {
        _currentAmountOfMoney -= cost;

        if (_currentAmountOfMoney < 0)
            _currentAmountOfMoney = 0;

        _textMoney.GetComponent<Text>().text = _currentAmountOfMoney.ToString();
    }

    public void GetMoney(float award)
    {
        _currentAmountOfMoney += award;

        _textMoney.GetComponent<Text>().text = _currentAmountOfMoney.ToString();
    }
}
