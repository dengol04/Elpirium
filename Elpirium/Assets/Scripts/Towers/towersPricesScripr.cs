using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towersPricesScripr : MonoBehaviour
{
    [SerializeField] private storeData _storeData;
    [SerializeField] private TowerType _typeOfTower;

    private void Awake()
    {
        this.GetComponent<Text>().text = ((int)_storeData.Prices[(int)_typeOfTower]).ToString();
    }

}
