using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SroreData", menuName = "Data/New StoreData")]
public class storeData : ScriptableObject
{
    [SerializeField]
    private float _startMoney;

    [SerializeField]
    private GameObject[] _towersPrefs;

    [SerializeField]
    private float[] _prices;

    public float StartMoney => _startMoney;
    public GameObject[] TowerPrefs => _towersPrefs;
    public float[] Prices => _prices;
}
