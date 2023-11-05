using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New LevelData")]
public class levelData : ScriptableObject
{
    [SerializeField]
    private string[] _way;
    [SerializeField]
    private Sprite[] _groundSprites;
    [SerializeField]
    private uint _fieldHeight, _fieldWidth;
    //Я добавил поле с окном выбора башни, где будет храниться спрайт.
    [SerializeField]
    private Sprite _changer;

    public string[] Way => _way;
    public Sprite[] GroundSprites => _groundSprites;
    public uint FieldHeight => _fieldHeight;
    public uint FieldWidth => _fieldWidth;
    //Хз че сделал
    public Sprite Changer => _changer;
}

